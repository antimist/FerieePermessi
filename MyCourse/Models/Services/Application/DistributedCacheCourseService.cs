using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCourse.Models.ViewModels;
using Newtonsoft.Json;

namespace MyCourse.Models.Services.Application
{
    public class DistributedCacheCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IDistribuitedCache distribuitedCache;
        public DistribuitedCacheCourseService(ICourseService courseService, IDistribuitedCache distribuitedCache)
        {
            this.courseService = courseService;
            this.distribuitedCache = distribuitedCache;
        }

        public async Task<CourseDetailViewModel> GetTaskAsync(int id)
        {

        }

        public async Task<List<CourseDetailViewModel>> GetTaskAsync()
        {
            string key =$"Courses";
            string serializedObject = await distribuitedCache.GetStringAsync(key);
 
            if(serializedObject != null)
            {
                  return Deserialize<List<CourseDetailViewModel>>(serializedObject);
            }

            List<CourseDetailViewModel> courses = await courseService.GetCourseAsync();
            serializedObject=Serialize(courses);

            var cacheOptions = new DistributedCacheEntryOption();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
 
            await distribuitedCache.SetStringAsync(key, serializedObject, cacheOptions);
            return courses;
        }        
    }

    private string Serialize (object obj)
    {
        //Convertiamo un oggetto in una stringa JSON
        return JsonConvert.serializedObject(obj);
    }

    private T Deserialize<T>(string serializedObject)
    {
        //Riconvertiamo una stringa JSON in un oggetto
        return JsonConvert.DeserializeObject<T>(serializedObject);
    }
}