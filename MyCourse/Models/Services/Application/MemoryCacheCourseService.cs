using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MyCourse.Models.Exceptions;
using MyCourse.Models.InputModels;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Application;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
namespace MyCourse.Models.Services.Application
{
    public class MemoryCacheCourseService : ICachedCourseService   
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;
        public MemoryCacheCourseService(ICourseService courseService, IMemoryCache memoryCache)
        {
            this.courseService = courseService;
            this.memoryCache = memoryCache;
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                //cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.GetCourseAsync(id);
            });
        }
        
        public  Task<List<CourseViewModel>> GetBestRatingCoursesAsync()
        {
            return memoryCache.GetOrCreateAsync($"BestRatingCourses", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.GetBestRatingCoursesAsync();
            });
        }

        public  Task<List<CourseViewModel>> GetMostRecentCoursesAsync()
        {
            return memoryCache.GetOrCreateAsync($"MostRecentCourses", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.GetMostRecentCoursesAsync();
            });
        }
        public Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model)
        {   // Metto in cache i risultati solo per le prime 5 pagine del catalogo, che reputo essere
            // le più visitate dagli utenti, e che perchiò mi prmettono di avere il maggior beneficio dalla cache.
            // E inoltre, metto in cache i risultati  solo se l'utente non ha cercato nulla.
            // In questo modo riduco drasticamente il consumo di memoria RAM
            bool canCache = model.Page <=5 && string.IsNullOrEmpty(model.Search);
            
            //Se canCache è true, sfrutto il meccanismo di caching
            if(canCache)
            {
                return memoryCache.GetOrCreateAsync($"Courses{model.Search}-{model.Page}-{model.OrderBy}-{model.Ascending}", cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                    return courseService.GetCoursesAsync(model);
                });
            }
            //altrimenti uso il servizio applicativo sottostante, che recupererà sempre i valori dal database
            return courseService.GetCoursesAsync(model);
        }

        public Task<CourseDetailViewModel> CreateCurseAsync(CourseCreateInputModel inputModel)
        {
            return courseService.CreateCurseAsync(inputModel);
        }

        public Task<bool> IsTitleAviableAsync(string title, int id)
        {
            return courseService.IsTitleAviableAsync(title, id);
        }

        public Task<CourseEditInputModel> GetCourseForEditingAsync(int id)
        {
            return courseService.GetCourseForEditingAsync(id);
        }
                
        public async Task<CourseDetailViewModel> EditCourseAsync(CourseEditInputModel inputModel)
        {
            CourseDetailViewModel viewModel = await courseService.EditCourseAsync(inputModel);
            memoryCache.Remove($"Course{inputModel.Id}");
            return viewModel;
        }

    }
}