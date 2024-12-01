using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public GradesController(IGradeRepository gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var grades = await _gradeRepository.GetAllAsync();
            var gradeDtos = _mapper.Map<IEnumerable<GradeDto>>(grades);
            return Ok(BaseResponse<IEnumerable<GradeDto>>.SuccessResponse(gradeDtos, "Grades retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            if (grade == null)
            {
                return NotFound(BaseResponse<string>.FailureResponse("Grade not found."));
            }

            var gradeDto = _mapper.Map<GradeDto>(grade);
            return Ok(BaseResponse<GradeDto>.SuccessResponse(gradeDto, "Grade retrieved successfully."));
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetGradesByStudentId(int studentId)
        {
            var grades = await _gradeRepository.GetByStudentIdAsync(studentId);
            var gradeDtos = _mapper.Map<IEnumerable<GradeDto>>(grades);
            return Ok(BaseResponse<IEnumerable<GradeDto>>.SuccessResponse(gradeDtos, $"Grades for student {studentId} retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody] GradeDto gradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BaseResponse<string>.FailureResponse("Invalid data."));
            }

            var grade = _mapper.Map<Grade>(gradeDto);
            await _gradeRepository.AddAsync(grade);
            return CreatedAtAction(
                nameof(GetGrade),
                new { id = grade.Id },
                BaseResponse<GradeDto>.SuccessResponse(gradeDto, "Grade created successfully.")
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] GradeDto gradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BaseResponse<string>.FailureResponse("Invalid data."));
            }

            var grade = await _gradeRepository.GetByIdAsync(id);
            if (grade == null)
            {
                return NotFound(BaseResponse<string>.FailureResponse("Grade not found."));
            }

            _mapper.Map(gradeDto, grade);
            await _gradeRepository.UpdateAsync(grade);
            return Ok(BaseResponse<GradeDto>.SuccessResponse(gradeDto, "Grade updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            if (grade == null)
            {
                return NotFound(BaseResponse<string>.FailureResponse("Grade not found."));
            }

            await _gradeRepository.DeleteAsync(id);
            return Ok(BaseResponse<string>.SuccessResponse($"Grade with ID {id} deleted successfully."));
        }
    }
}
