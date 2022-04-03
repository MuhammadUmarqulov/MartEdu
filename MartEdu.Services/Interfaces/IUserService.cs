using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Users;
using MartEdu.Services.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> CreateAsync(UserForCreationDto model);
        Task<BaseResponse<User>> UpdateAsync(Guid id, UserForCreationDto model);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression);
        Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        Task<BaseResponse<User>> Restore(Guid id);
        Task<BaseResponse<User>> Login(UserForLoginDto loginDto);
    }
}
