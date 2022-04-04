using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Service.DTOs.Authors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Interfaces
{
    public interface IAuthorService
    {
        Task<BaseResponse<Author>> CreateAsync(AuthorForCreationDto model);
        Task<BaseResponse<Author>> UpdateAsync(Guid id, AuthorForCreationDto model);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Author, bool>> expression);
        Task<BaseResponse<Author>> GetAsync(Expression<Func<Author, bool>> expression);
        Task<BaseResponse<IEnumerable<Author>>> GetAllAsync(PaginationParams @params, Expression<Func<Author, bool>> expression = null);
        Task<BaseResponse<Author>> Restore(Expression<Func<Author, bool>> expression);
        Task<BaseResponse<Author>> Login(AuthorForLoginDto loginDto);
        Task<BaseResponse<Author>> SetImage(Expression<Func<Author, bool>> expression, IFormFile image);
    }
}
