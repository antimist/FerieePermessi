using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Mycurse.Models.Services.Infrastructure;

namespace MyCourse.Models.Services.Infrastructure
{
    public class MagickNetImagePersister : IImagePersister
    {
        private readonly IWebHostEnvironment env;

        private readonly SemaphoreSlim semaphore;

        public MagickNetImagePersister(IWebHostEnvironment env)
        {
            ResourceLimits.Height = 4000;
            ResourceLimits.Width = 4000;
            semaphore = new SemaphoreSlim(2);
            this.env = env;
        }

        public async Task<string> SaveCourseImageAsync(int courseId, IFormFile formFile)
        {
            //Il metodo WaitAsync ha anche un overload che permette di passare un timeout
            //Ad esempio, se vogliamo aspettare al massimo 1 secondo:
            //await semaphore.AwaitAsync(TimeSpan.FromSeconds(1));
            //Se il timeout scade, il SemaphoreSlim solleverà un'eccezione (così almeno non resta in attesa all'infinito)            
            await semaphore.WaitAsync();
            try 
            {
                //Salvare il fil
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
            catch (Exception exc)
            {
                throw new ImagePersistenceException(exc);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}