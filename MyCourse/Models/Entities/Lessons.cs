using System;
using System.Collections.Generic;

namespace MyCourse.Models.Entities
{
    public partial class Lesson
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; private set; }
        public TimeSpan Duration { get; set; }

        public virtual Course Course { get; set; }

        public void ChangeOrder(int order)
        {
            Order = order;
        }        
    }
}
