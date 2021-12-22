using System.Collections.Generic;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
//using MyCourse.Controllers;
using System.Data;
using System;
using System.Threading.Tasks;
//using MyCourse.Models.Services.Application;
//using MyCourse.Models.ViewModels.CourseDetailViewModel;

namespace MyCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;
        public AdoNetCourseService(IDatabaseAccessor db)
        {
            this.db = db;
        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id}
                           ; SELECT Id, Title, Description, Duration FROM Lessons WHERE CourseId={id}";

            DataSet dataSet = await db.QueryAsync(query);

            //Course
            var courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count !=1)
            {
                throw new InvalidOperationException($"Did not return exactly 1 row for Curse {id}");// da vedere
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            //Curse lessons
            var lessonDataTable = dataSet.Tables[1];

            foreach (DataRow lessonsRow in lessonDataTable.Rows) {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonsRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }

            return courseDetailViewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses";
            DataSet dataSet = await db.QueryAsync(query);

            var dataTable = dataSet.Tables[0];
            var curseList = new List<CourseViewModel>();

            foreach (DataRow courseRow in dataTable.Rows)
            {
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                curseList.Add(course);
            }
            // Id, Title, ImagePath, Author, Rating, FullPrice_Amount,  FullPrice_Currency
            return curseList;
        }
    }
}