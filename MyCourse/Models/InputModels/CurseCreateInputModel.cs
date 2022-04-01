using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Controllers;

namespace MyCourse.Models.InputModels
{
    public class CourseCreateInputModel
    {
        [Required (ErrorMessage ="Il titolo è obbligatorio"), 
        MinLength(10, ErrorMessage="Il titolo deve essere di almeno {1} caratteri"), 
        MaxLength(100, ErrorMessage="Il titolo deve essere di massimo {1} caratteri"), 
        RegularExpression(@"^[\w\s\.]+$", ErrorMessage="Titolo non valido"),
        Remote(action: nameof(CourserController.IsTitleAviable), controller: "Courser", ErrorMessage ="Il titolo esiste già")]
        public string Title {get; set;}
     }
}