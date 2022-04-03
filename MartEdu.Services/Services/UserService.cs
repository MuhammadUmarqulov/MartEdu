using AutoMapper;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Users;
using MartEdu.Domain.Enums;
using MartEdu.Service.DTOs.Users;
using MartEdu.Service.Extensions;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<User>> CreateAsync(UserForCreationDto model)
        {
            var response = new BaseResponse<User>();

            var existUser = await unitOfWork.Users.GetAsync(p => p.Email == model.Email || p.Username == model.Username);

            if (existUser is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist");
                return response;
            }


            var mappedUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password.Encrypt(),
            };

            mappedUser.Create();

            var result = await unitOfWork.Users.CreateAsync(mappedUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var user = await unitOfWork.Users.GetAsync(expression);

            if (user is null || user.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            user.Delete();
            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<User>();

            var user = await unitOfWork.Users.GetAsync(expression);

            if (user.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<User>>();

            var users = await unitOfWork.Users.WhereAsync(expression);

            users = users.Where(p => p.State != ItemState.Deleted);

            response.Data = users.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<User>> UpdateAsync(Guid id, UserForCreationDto model)
        {
            var response = new BaseResponse<User>();

            // check for exist user
            var user = await unitOfWork.Users.GetAsync(p => p.Id == id && p.State == ItemState.Deleted);
            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Update();

            var result = await unitOfWork.Users.UpdateAsync(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<User>> Restore(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<User>();

            var user = await unitOfWork.Users.GetAsync(expression);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            user.Update();

            await unitOfWork.Users.UpdateAsync(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<User>> Login(UserForLoginDto loginDto)
        {
            var response = new BaseResponse<User>();

            var encryptedPassword = loginDto.Password.Encrypt();

            var user = await unitOfWork.Users
                                .GetAsync(p => (p.Username == loginDto.EmailOrUsername || p.Email == loginDto.EmailOrUsername)
                                            && p.Password == encryptedPassword);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<User>> SetImage(Expression<Func<User, bool>> expression, IFormFile image)
        {
            var response = new BaseResponse<User>();

            var user = await unitOfWork.Users.GetAsync(expression);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            user.Image = await FileStreamExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, env, config);

            user.Update();
            await unitOfWork.Users.UpdateAsync(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = user;

            return response;

        }
    }
}
