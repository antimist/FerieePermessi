
using Microsoft.AspNetCore.Hosting;
using Mycurse.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
namespace MyCourse.Models.Services.Infrastructure
{
    public class MagickNetImagePersister : IImagePersister
    {
        private readonly IWebHostEnvironment env;

        public MagickNetImagePersister(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public async Task<string> SaveCourseImageAsync(int courseId, IFormFile formFile)
        {
            //Salvare il file
            string  path = $"/Courses/{courseId}.jpg";
            string  physicalPath = Path.Combine(env.WebRootPath, "Courses", $"{courseId}.jpg" );

            using Stream inputStrim = formFile.OpenReadStream();
            using MagickImage image = new MagickImage(inputStrim);

            //Manipolare l'immagine
            int whidth = 300;
            int height = 300;
            MagickGeometry resizeGeometry = new MagickGeometry(whidth, height)
            {
                FillArea=true
            };
            
            image.Resize(resizeGeometry);
            image.Crop(whidth, whidth, Gravity.Northwest);
            image.Quality = 70;
            image.Write(physicalPath, MagickFormat.Jpg);

            //restituisce il percorso del file
            return path;
        }
    }
}