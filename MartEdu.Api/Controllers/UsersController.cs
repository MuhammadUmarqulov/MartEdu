﻿using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Users;
using MartEdu.Service.DTOs.Users;
using MartEdu.Service.Extensions.Attributes;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MartEdu.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : GenericController<IUserService, User, UserForCreationDto>
    {
        public UsersController(IUserService service) : base(service)
        {
        }

        [HttpPost]
        public async override Task<ActionResult<BaseResponse<User>>> Create([FromForm] UserForCreationDto creationDto)
        {
            var result = await service.CreateAsync(creationDto, p => p.Email == creationDto.Email || p.Username == creationDto.Username);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<User>>> Login(UserForLoginDto user)
        {
            var result = await service.LoginAsync(user);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost("image/{id}")]
        public async Task<ActionResult<BaseResponse<User>>> SetImage(Guid id, [FormFileExtensions(".png", ".jpg"), MaxFileSize(5 * 1024 * 1024)] IFormFile image)
        {
            var result = await service.SetImageAsync(p => p.Id == id, image);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

    }
}
