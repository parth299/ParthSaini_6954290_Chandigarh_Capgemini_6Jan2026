using AutoMapper;
using OnlineLearningPlatform.API.Models;
using OnlineLearningPlatform.API.DTOs;

namespace OnlineLearningPlatform.API.Profiles;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Course, CourseDto>().ReverseMap();

        CreateMap<Lesson, LessonDto>().ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();
    }
}