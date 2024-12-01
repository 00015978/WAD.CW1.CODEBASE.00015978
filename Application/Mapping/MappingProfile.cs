using Application.DTOs;
using AutoMapper;
using Core.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to DTO
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Grade, GradeDto>().ReverseMap();
        }
    }
}
