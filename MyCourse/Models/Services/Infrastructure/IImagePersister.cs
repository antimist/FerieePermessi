using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
namespace  Mycurse.Models.Services.Infrastructure
{
    public interface IImagePersister
    {
        /// <returns> Return: The image URL e.g. /Courses/1.jpg</returns>
        Task<string> SaveCourseImageAsync(int courseId, IFormFile formFile);
    }
}