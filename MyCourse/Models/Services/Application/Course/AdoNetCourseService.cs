using System;
using System.Collections.Generic;
using System.Data;  // <-- sono rimasto qui
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCourse.Models.Exceptions;
using MyCourse.Models.Exceptions.Application;
using MyCourse.Models.Exceptions.Infrastructure;
using MyCourse.Models.InputModels;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ValueTypes;
using MyCourse.Models.ViewModels;
using Mycurse.Models.Services.Infrastructure;

/*using System.Linq;
  using MyCourse.Controllers;
  using MyCourse.Models.Services.Application;
 using ImageMagick; */

namespace MyCourse.Models.Services.Application.Course
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;
        private readonly IImagePersister imagePersister;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        private readonly ILogger<AdoNetCourseService> logger;


        public AdoNetCourseService(ILogger<AdoNetCourseService> logger, IDatabaseAccessor db, IImagePersister imagePersister, IOptionsMonitor<CoursesOptions> coursesOptions)
        {
            this.imagePersister = imagePersister;
            this.coursesOptions = coursesOptions;
            this.logger = logger;
            this.db = db;
        }
        public async Task<CourseDetailViewModel> GetCourseAsync(long id)
        {
            logger.LogInformation("Course {id} requested", id);

            FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id}
                           ; SELECT Id, Title, Description, Duration FROM Lessons WHERE CourseId={id}";

            DataSet dataSet = await db.QueryAsync(query);

            //Course
            var courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count != 1)
            {
                logger.LogWarning("Course {id} NOTTE FONDA!!", id);
                throw new CourseNotFoundException(id);// da vedere
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            //Curse lessons
            var lessonDataTable = dataSet.Tables[1];

            foreach (DataRow lessonsRow in lessonDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonsRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }

            return courseDetailViewModel;
        }

        public async Task<List<CourseViewModel>> GetMostRecentCoursesAsync()
        {
            CourseListInputModel inputModel = new CourseListInputModel(
                search: "",
                page: 1,
                orderBy: "Id",
                ascending: false,
                limit: coursesOptions.CurrentValue.InHome,
                orderOptions: coursesOptions.CurrentValue.Order);

            ListViewModel<CourseViewModel> result = await GetCoursesAsync(inputModel);
            return result.Results;
        }
        public async Task<List<CourseViewModel>> GetBestRatingCoursesAsync()
        {
            CourseListInputModel inputModel = new CourseListInputModel(
                search: "",
                page: 1,
                orderBy: "Rating",
                ascending: false,
                limit: coursesOptions.CurrentValue.InHome,
                orderOptions: coursesOptions.CurrentValue.Order);

            ListViewModel<CourseViewModel> result = await GetCoursesAsync(inputModel);
            return result.Results;
        }

        public async Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model)
        {
            string orderby = model.OrderBy == "CurrentPrice" ? "CurrentPrice_Amount" : model.OrderBy;
            string direction = model.Ascending ? "ASC" : "DESC";

            FormattableString query = $@"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Title LIKE {"%" + model.Search + "%"} ORDER BY {(Sql)orderby} {(Sql)direction} LIMIT {model.Limit} OFFSET {model.Offset};
            SELECT COUNT(*) FROM Courses WHERE Title LIKE {"%" + model.Search + "%"}";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var curseList = new List<CourseViewModel>();
            foreach (DataRow courseRow in dataTable.Rows)
            {
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                curseList.Add(course);
            }

            ListViewModel<CourseViewModel> result = new ListViewModel<CourseViewModel>
            {
                Results = curseList,
                TotalCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0])
            };

            return result;
        }

        public async Task<CourseDetailViewModel> CreateCurseAsync(CourseCreateInputModel inputModel)
        {
            string title = inputModel.Title;
            string author = "Mario Rossi";
            //string sSQL   = $@"INSERT INTO Courses (Title, Author, ImagePath, CurrentPrice_Currency, CurrentPrice_Ammount, FullPrice_Currency, FullPrice_Ammount) VALUES ('{title}, {author}, '/Courses/default.png', 'EUR', 0, 'EUR', 0); SELECT last_insert_rowid();";
            try
            {/*
                var dataSet = await db.QueryAsync($@"INSERT INTO Courses (Title, Author, ImagePath, CurrentPrice_Amount, 
                                                            CurrentPrice_Currency, FullPrice_Currency, FullPrice_Amount) 
                                                     VALUES ({title}, {author}, '/Courses/default.png',  0, 'EUR', 'EUR', 0); 
                                                     SELECT last_insert_rowid();"); 
                */
                // modifica come da sezione 17 lezione 130
                // ------------------------- INIZIO --------------------------
                var courseId = await db.QueryScalarAsync<int>($@"INSERT INTO Courses (Title, Author, ImagePath, CurrentPrice_Amount, 
                                                            CurrentPrice_Currency, FullPrice_Currency, FullPrice_Amount) 
                                                     VALUES ({title}, {author}, '/Courses/default.png',  0, 'EUR', 'EUR', 0); 
                                                     SELECT last_insert_rowid();");

                //int courseId = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                // ------------------------- FINE --------------------------
                CourseDetailViewModel course = await GetCourseAsync(courseId);
                return course;
            }
            catch (SqliteException exc) when (exc.SqliteErrorCode == 19)
            {
                throw new CourseTitleUnaviableException(title, exc);
            }
        }
        public async Task<bool> IsTitleAviableAsync(string title, long id)
        {
            bool titleExist = await db.QueryScalarAsync<bool>($"SELECT COUNT(*) FROM Courses WHERE Title LIKE {title} AND ID<>{id}");
            //bool titleAviable = Convert.ToInt32(result.Tables[0].Rows[0][0]) == 0;
            bool titleAviable = !titleExist;
            return titleAviable;
        }

        //public async Task<CourseEditInputModel> GetCourseEditInputModelAsync(long id)
        public async Task<CourseEditInputModel> GetCourseForEditingAsync(long id)
        {
            FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Email, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency, RowVersion FROM Courses WHERE Id={id}";

            DataSet dataSet = await db.QueryAsync(query);

            var courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count != 1)
            {
                logger.LogWarning("Course {id} not found", id);
                throw new CourseNotFoundException(id);
            }
            var courseRow = courseTable.Rows[0];
            var courseEditInputModel = CourseEditInputModel.FromDataRow(courseRow);
            return courseEditInputModel;
        }

        public async Task<CourseDetailViewModel> EditCourseAsync(CourseEditInputModel inputModel)
        {
            try
            {
                string ImagePath = null;
                if (inputModel.Image != null)
                {
                    ImagePath = await imagePersister.SaveCourseImageAsync(inputModel.Id, inputModel.Image);
                }

                int affectedRow = await db.CommandAsync($"UPDATE Courses SET ImagePath=COALESCE({ImagePath}, ImagePath), Title={inputModel.Title}, Description={inputModel.Description}, Email={inputModel.Email}, CurrentPrice_Currency={inputModel.CurrentPrice.Currency.ToString()}, CurrentPrice_Amount={inputModel.CurrentPrice.Amount}, FullPrice_Currency={inputModel.FullPrice.Currency.ToString()}, FullPrice_Amount={inputModel.FullPrice.Amount} WHERE Id={inputModel.Id} AND RowVersion = {inputModel.RowVersion}");
                if (affectedRow == 0)
                {
                    bool courseExist = await db.QueryScalarAsync<bool>($"SELEC COUNT(*) FROM Courses WHERE Id={inputModel.Id}");
                    if (courseExist)
                    {
                        throw new OptimisticConcurrencyException();
                    }
                    else
                    {
                        throw new CourseNotFoundException(inputModel.Id);
                    }
                }
            }
            catch (ConstraintViolationException exc)
            {
                throw new CourseTitleUnaviableException(inputModel.Title, exc);
            }
            // modifica come da sezione 17 lezione 130
            // ------------------------- INIZIO --------------------------
            catch (ImagePersistenceException exc)
            {
                throw new CourseImageInvalidException(inputModel.Id, exc);
            }
            //------------------------- Finito --------------------------

            CourseDetailViewModel course = await GetCourseAsync(inputModel.Id);
            return course;
        }

        public async Task<bool> IsTitleAvliableAsync(string title, long id)
        {
            // modifica come da sezione 17 lezione 130
            // ------------------------- INIZIO --------------------------
            //DataSet result = await db.QueryAsync($"SELECT COUNT(*) FROM Courses WHERE Title LIKE {title} AND id<>{id}");
            bool titleExists = await db.QueryScalarAsync<bool>($"SELECT COUNT(*) FROM Courses WHERE Title LIKE {title} AND id<>{id}");
            //bool titleAvailable = Convert.ToInt32(result.Tables[0].Rows[0][0]) == 0;
            bool titleAvailable = !titleExists;
            // ------------------------- FINE --------------------------
            return titleAvailable;
        }
    }
}