﻿using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Enums;
using MartEdu.Service.DTOs.Authors;
using MartEdu.Service.Extensions.Attributes;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MartEdu.Api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : GenericController<IAuthorService, Author, AuthorForCreationDto>
    {
        public AuthorsController(IAuthorService authorService) : base(authorService)
        {
        }

        [HttpPost("image/profile/{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> SetProfileImage(Guid id, [FormFileExtensions(".png", ".jpg"), MaxFileSize(5 * 1024 * 1024)] IFormFile image)
        {
            var result = await service.SetProfileImageAsync(p => p.Id == id, image);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost("image/background/{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> SetBackgroundImage(Guid id, [FormFileExtensions(".png", ".jpg"), MaxFileSize(5 * 1024 * 1024)] IFormFile image)
        {
            var result = await service.SetBackgroundImageAsync(p => p.Id == id, image);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("image/profile/{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> DeleteProfileImage(Guid id, [FormFileExtensions(".png", ".jpg"), MaxFileSize(5 * 1024 * 1024)] IFormFile image)
        {
            var result = await service.DeleteProfileImageAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("image/background/{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> DeleteBackgroundImage(Guid id, [FormFileExtensions(".png", ".jpg"), MaxFileSize(5 * 1024 * 1024)] IFormFile image)
        {
            var result = await service.DeleteBackgroundImageAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost("vote/{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> Vote(Guid id, [Required] int vote)
        {
            var result = await service.VoteAsync(vote, p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost]
        public override async Task<ActionResult<BaseResponse<Author>>> Create([FromBody]AuthorForCreationDto creationDto)
        {
            var result = await service.CreateAsync(creationDto, p => p.Email == creationDto.Email);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
