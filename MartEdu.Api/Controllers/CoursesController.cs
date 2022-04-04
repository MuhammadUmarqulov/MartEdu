using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Enums;
using MartEdu.Service.DTOs.Courses;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MartEdu.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : GenericController<ICourseService, Course, CourseForCreationDto>
    {
        public CoursesController(ICourseService service) : base(service)
        {
        }

        [HttpPost]
        public async override Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseForCreationDto creationDto)
        {
            var result = await service.CreateAsync(creationDto, p => p.Name == creationDto.Name);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost("register/{userId}&{courseId}")]
        public async Task<ActionResult<BaseResponse<Course>>> Register(Guid userId, Guid courseId)
        {
            var result = await service.RegisterForCourseAsync(userId, courseId);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
