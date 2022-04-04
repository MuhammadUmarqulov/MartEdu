using AutoMapper;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Enums;
using MartEdu.Service.DTOs.Authors;
using MartEdu.Service.Extensions;
using MartEdu.Service.Helpers;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        private readonly HttpContextHelper httpContextHelper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
            this.httpContextHelper = new HttpContextHelper();
        }

        public async Task<BaseResponse<Author>> CreateAsync(AuthorForCreationDto model)
        {
            var response = new BaseResponse<Author>();

            var existAuthor = await unitOfWork.Authors.GetAsync(p => p.Email == model.Email);

            if (existAuthor is not null)
            {
                response.Error = new ErrorResponse(400, "This email already used");
                return response;
            }


            var mappedAuthor = mapper.Map<Author>(model);
            mappedAuthor.Password = model.Password.Encrypt();

            mappedAuthor.Create();

            var result = await unitOfWork.Authors.CreateAsync(mappedAuthor);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null || author.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.Delete();
            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<Author>> DeleteBackgroundImageAsync(Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.BackgroundImage = null;
            author.Update();

            unitOfWork.Authors.Update(author);

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> DeleteProfileImageAsync(Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.ProfileImage = null;

            author.Update();
            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Author>>> GetAllAsync(PaginationParams @params, Expression<Func<Author, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Author>>();

            var authors = unitOfWork.Authors.Where(expression);

            authors = authors.Where(p => p.State != ItemState.Deleted);

            response.Data = authors.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Author>> GetAsync(Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> LoginAsync(AuthorForLoginDto loginDto)
        {
            var response = new BaseResponse<Author>();

            var encryptedPassword = loginDto.Password.Encrypt();

            var author = await unitOfWork.Authors
                                .GetAsync(p => p.Email == loginDto.Email && p.Password == encryptedPassword);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "AUthor not found!");
                return response;
            }

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> RestoreAsync(Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.Update();

            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> SetBackgroundImageAsync(Expression<Func<Author, bool>> expression, IFormFile image)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.BackgroundImage = await FileStreamExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, env, config);

            author.Update();
            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> SetProfileImageAsync(Expression<Func<Author, bool>> expression, IFormFile image)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.ProfileImage = await FileStreamExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, env, config);

            author.Update();
            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> UpdateAsync(Guid id, AuthorForCreationDto model)
        {
            var response = new BaseResponse<Author>();

            // check for exist author
            var author = await unitOfWork.Authors.GetAsync(p => p.Id == id && p.State == ItemState.Deleted);
            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found");
                return response;
            }

            author.Name = model.Name;
            author.Email = model.Email;
            author.Password = model.Password.Encrypt();
            
            author.Update();

            var result = unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<Author>> VoteAsync(int vote, Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.Score += vote;
            author.CountOfVotes++;


            author.Update();
            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }
    }
}
