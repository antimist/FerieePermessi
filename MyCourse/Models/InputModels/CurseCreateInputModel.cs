using System.ComponentModel.DataAnnotations;

namespace MyCourse.Models.InputModels
{
    public class CourseCreateInputModel
    {
        [Required (ErrorMessage ="Il titolo è obbligatorio"), 
        MinLength(10, ErrorMessage="Il titolo deve essere di almeno {1} caratteri"), 
        MaxLength(100, ErrorMessage="Il titolo deve essere di massimo {1} caratteri"), 
        RegularExpression(@"^[\w\s\.]+$", ErrorMessage="Titolo non valido")]
        public string Title {get; set;}
        
    }
}