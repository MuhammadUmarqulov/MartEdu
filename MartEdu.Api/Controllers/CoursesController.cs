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
            var result = await courseService.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Get(Guid id)
        {
            var result = await courseService.GetAsync(p => p.Id == id);
                
            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseForCreationDto course)
        {
            var result = await courseService.CreateAsync(course);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Update(Guid id, CourseForCreationDto course)
        {
            var result = await courseService.UpdateAsync(id, course);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Delete(Guid id)
        {
            var result = await courseService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpPost("Restore/{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Restore(Guid id)
        {
            var result = await courseService.Restore(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpPost("Register/{userId}&{courseId}")]
        public async Task<ActionResult<BaseResponse<Course>>> Register(Guid userId, Guid courseId)
        {
            var result = await courseService.RegisterForCourse(userId, courseId);
            
            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }
    }
}
