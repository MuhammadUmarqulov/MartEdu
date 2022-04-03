using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Users;
using MartEdu.Service.DTOs.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> CreateAsync(UserForCreationDto model);
        Task<BaseResponse<User>> UpdateAsync(Guid id, UserForCreationDto model);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        Task<BaseResponse<User>> Restore(Expression<Func<User, bool>> expression);
        Task<BaseResponse<User>> Login(UserForLoginDto loginDto);
        Task<BaseResponse<User>> SetImage(Expression<Func<User, bool>> expression, IFormFile image);
    }
}
