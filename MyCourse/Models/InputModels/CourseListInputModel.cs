using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyCourse.Customizations.ModelBinders;
using MyCourse.Models.InputModels;
using MyCourse.Models.Options;

namespace MyCourse.Models.InputModels
{
    [ModelBinder(BinderType = typeof(CourseListInputModelBinder))]
    public class CourseListInputModel
    {
        public CourseListInputModel(string search, int page, string orderBy, bool ascending, int limit, CoursesOrderOptions orderOptions)
        {
            //Sanitizzazione
            
            if (!orderOptions.Allow.Contains(orderBy))
            {
                orderBy = orderOptions.By;
                ascending = orderOptions.Ascending;
            }

            Search      = search ?? "";
            Page        = Math.Max(1, page) ;
            OrderBy     = orderBy;
            Ascending   = ascending;

            Limit = Math.Max(1, limit);
            Offset = (Page -1) * Limit;
        }
        public string Search {get;}
        public int Page {get;}
        public string OrderBy {get;}
        public bool Ascending {get;}
        public int Limit {get;}
        public int Offset {get;}
    }
}