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
        Task<BaseResponse<Author>> RestoreAsync(Expression<Func<Author, bool>> expression);
        Task<BaseResponse<Author>> LoginAsync(AuthorForLoginDto loginDto);
        Task<BaseResponse<Author>> SetBackgroundImageAsync(Expression<Func<Author, bool>> expression, IFormFile image);
        Task<BaseResponse<Author>> SetProfileImageAsync(Expression<Func<Author, bool>> expression, IFormFile image);
        Task<BaseResponse<Author>> DeleteBackgroundImageAsync(Expression<Func<Author, bool>> expression);
        Task<BaseResponse<Author>> DeleteProfileImageAsync(Expression<Func<Author, bool>> expression);   
        Task<BaseResponse<Author>> VoteAsync(int vote, Expression<Func<Author, bool>> expression);
    }
}
