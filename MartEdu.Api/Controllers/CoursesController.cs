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
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Course>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var courses = await courseService.GetAllAsync(@params);

            return StatusCode(200, courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Get(Guid id)
        {
            var course = await courseService.GetAsync(p => p.Id == id);

            return StatusCode(200, course);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseForCreationDto course)
        {
            var result = await courseService.CreateAsync(course);

            return StatusCode(200, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Update(Guid id, CourseForCreationDto course)
        {
            var result = await courseService.UpdateAsync(id, course);

            return StatusCode(200, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Delete(Guid id)
        {
            var result = await courseService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(200, result);
        }

        [HttpPost("restore/{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Restore(Guid id)
        {
            var result = await courseService.Restore(p => p.Id == id);

            return StatusCode(200, result);
        }

        [HttpPost("register/{userId}&{courseId}")]
        public async Task<ActionResult<BaseResponse<string>>> Register(Guid userId, Guid courseId)
        {
            var result = await courseService.RegisterForCourse(userId, courseId);

            return StatusCode(200, result);
        }
    }
}
