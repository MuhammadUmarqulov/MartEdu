using AutoMapper;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Service.DTOs.Courses;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CourseForCreationDto model)
        {
            var response = new BaseResponse<Course>();

            var existCourse = await unitOfWork.Courses.GetAsync(p => p.Name == model.Name);

            if (existCourse is not null)
            {
                response.Error = new ErrorResponse(400, "Course with this name exist");
                return response;
            }


            var mappedCourse = mapper.Map<Course>(model);

            mappedCourse.Create();

            var result = await unitOfWork.Courses.CreateAsync(mappedCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existCourse = await unitOfWork.Courses.GetAsync(expression);

            if (existCourse is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }

            existCourse.Delete();

            await unitOfWork.Courses.UpdateAsync(existCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Course>> Restore(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseForCreationDto model)
        {
            throw new NotImplementedException();
        }
    }
}
