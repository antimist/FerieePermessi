using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace  Mycurse.Models.Services.Infrastructure
{
    public class InsecureImagePersister: IImagePersister 
    {
        private readonly IWebHostEnvironment env;

        public InsecureImagePersister(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<string> SaveCourseImageAsync(int courseID, IFormFile formFile)
        {
            // Come sanitizzare i nomi dei file https://bit.ly/sanitizzare-nome-file

            // TODO: Salvare il file
            string path = $"/Courses/{courseID}.jpg";
            string physicalPath = Path.Combine(env.WebRootPath, "Courses" ,$"{courseID}.jpg");
            using FileStream fileStream = File.OpenWrite(physicalPath);
            await formFile.CopyToAsync(fileStream);
            // TODO: Restituire il percorso del file

            return path;
        }
    }
}