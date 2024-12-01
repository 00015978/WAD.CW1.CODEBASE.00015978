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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetAllAsync();
            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(BaseResponse<IEnumerable<StudentDto>>.SuccessResponse(studentDtos, "Students retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound(BaseResponse<string>.FailureResponse("Student not found."));
            }

            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(BaseResponse<StudentDto>.SuccessResponse(studentDto, "Student retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreateEditDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BaseResponse<string>.FailureResponse("Invalid data."));
            }

            var student = _mapper.Map<Student>(studentDto);
            await _studentRepository.AddAsync(student);
            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                BaseResponse<StudentCreateEditDto>.SuccessResponse(studentDto, "Student created successfully.")
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentCreateEditDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(BaseResponse<string>.FailureResponse("Invalid data."));
            }

            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound(BaseResponse<string>.FailureResponse("Student not found."));
            }

            _mapper.Map(studentDto, student);
            await _studentRepository.UpdateAsync(student);
            return Ok(BaseResponse<StudentCreateEditDto>.SuccessResponse(studentDto, "Student updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound(BaseResponse<string>.FailureResponse("Student not found."));
            }

            await _studentRepository.DeleteAsync(id);
            return Ok(BaseResponse<string>.SuccessResponse($"Student with ID {id} deleted successfully."));
        }
    }
}
