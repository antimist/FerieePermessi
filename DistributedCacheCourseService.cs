using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using MyCourse.Models.InputModels;
using MyCourse.Models.ViewModels;
using Newtonsoft.Json;

namespace MyCourse.Models.Services.Application
{










    public class DistributedCacheCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IDistributedCache distributedCache;
        public DistributedCacheCourseService(ICourseService courseService, IDistributedCache distributedCache)
        {
            this.courseService = courseService;
            this.distributedCache = distributedCache;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
           // return MemoryCacheCourseService.GetOrCreateAsync($"Course{id}", cacheEntry => 
           // {
           //     cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
           //     return courseService.GetCourseAsync(id);
           // });
           return null;
        }
                                                
        public async Task<List<CourseViewModel>> GetCoursesAsync(CourseListInputModel model)
        {
            string key = $"Courses";
            string serializedObject = await distributedCache.GetStringAsync(key);
 
            if(serializedObject != null) {
                  return Deserialize<List<CourseViewModel>>(serializedObject);
            }

            ListViewModel<CourseViewModel> courses = await courseService.GetCoursesAsync(model);
            serializedObject=Serialize(courses);

            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
 
            await distributedCache.SetStringAsync(key, serializedObject, cacheOptions);
            return courses;
        }        
    
        private string Serialize (object obj)
        {
            //Convertiamo un oggetto in una stringa JSON
            return JsonConvert.SerializeObject(obj);
        }

        private T Deserialize<T>(string serializedObject)
        {
            //Riconvertiamo una stringa JSON in un oggetto
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }
    }
}