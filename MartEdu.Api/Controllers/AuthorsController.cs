using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Enums;
using MartEdu.Service.DTOs.Authors;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MartEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Author>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await authorService.GetAllAsync(@params);
            
            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> Get(Guid id)
        {
            var result = await authorService.GetAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Author>>> Create([FromForm] AuthorForCreationDto course)
        {
            var result = await authorService.CreateAsync(course);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> Update(Guid id, AuthorForCreationDto course)
        {
            var result = await authorService.UpdateAsync(id, course);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> Delete(Guid id)
        {
            var result = await authorService.DeleteAsync(p => p.Id == id && p.State != ItemState.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }

        [HttpPost("restore/{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> Restore(Guid id)
        {
            var result = await authorService.Restore(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);  
        }
    }
}
