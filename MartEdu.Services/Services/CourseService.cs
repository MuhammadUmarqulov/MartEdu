using AutoMapper;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Enums;
using MartEdu.Service.DTOs.Courses;
using MartEdu.Service.Extensions;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
            mappedCourse.Image = await FileStreamExtensions.SaveFileAsync(model.Image.OpenReadStream(), model.Image.FileName, env, config);

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

            unitOfWork.Courses.Update(existCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Course>>();

            var courses = unitOfWork.Courses.Where(expression);

            courses = courses.Where(p => p.State != ItemState.Deleted);

            response.Data = courses.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(expression);


            if (course is null || course.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<Course>> Restore(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();

            var course = await unitOfWork.Courses.GetAsync(expression);

            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            course.Update();

            unitOfWork.Courses.Update(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = course;

            return response;
        }

        public async Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseForCreationDto model)
        {
            var response = new BaseResponse<Course>();

            // check for exist course
            var course = await unitOfWork.Courses.GetAsync(p => p.Id == id && p.State == ItemState.Deleted);
            if (course is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            course.Name = model.Name;
            course.Teg = model.Teg;
            course.Level = model.Level;
            course.Section = model.Section;
            course.Description = model.Description;
            
            course.Image = await FileStreamExtensions.SaveFileAsync(model.Image.OpenReadStream(), model.Image.FileName, env, config);

            course.Update();

            var result = unitOfWork.Courses.Update(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<Course>> RegisterForCourse(Guid userId, Guid courseId)
        {
            var response = new BaseResponse<Course>();

            var user = await unitOfWork.Users.GetAsync(p => p.Id == userId && p.State != ItemState.Deleted);
            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == courseId && p.State != ItemState.Deleted);
            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            user.Courses.Add(course);
            user.Update();
            unitOfWork.Users.Update(user);

            course.Participants.Add(user);
            course.Update();
            unitOfWork.Courses.Update(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = course;
            return response;
        }
    }
}
