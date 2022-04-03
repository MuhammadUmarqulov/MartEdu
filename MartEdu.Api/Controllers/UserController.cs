using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Users;
using MartEdu.Services.DTOs.Users;
using MartEdu.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MartEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<User>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await userService.GetAllAsync(@params);

            return StatusCode(200, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Get(Guid id)
        {
            var result = await userService.GetAsync(p => p.Id == id);

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<User>>> Create(UserForCreationDto user)
        {
            var result = await userService.CreateAsync(user);

            return StatusCode(200, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Update(Guid id, UserForCreationDto user)
        {
            var result = await userService.UpdateAsync(id, user);

            return StatusCode(200, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Delete(Guid id)
        {
            var result = await userService.DeleteAsync(id);

            return StatusCode(200, result);
        }

        [HttpPost("restore/{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Restore(Guid id)
        {
            var result = await userService.Restore(id);

            return StatusCode(200, result);
        }

        //[HttpPost("set-image")]
        //public async Task<Action<BaseResponse<User>>> SetImage(Guid id, IFormFile image)
        //{
        //    var result = await userService.SetImage(id);

        //    return StatusCode(200, result);
        //}
    }
}
