using System;
using System.Collections.Generic;
namespace MyCourse.Models.Entities
{
    public partial class Lesson
    {
        public Lesson(string title, long courseId)
        {
            ChangeTitle(title);
            CourseId = courseId;
            Order=1000;
            Duration = TimeSpan.FromSeconds(0);
        }
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; private set; }
        public TimeSpan Duration { get; set; }
        public string RowVersion { get; private set; }

        public virtual Course Course { get; set; }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("A lesson must have a title");
            }
            Title = title;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void ChangeDuration(TimeSpan duration)
        {
            Duration = duration;
        }

        public void ChangeOrder(int order)
        {
            Order = order;
        }        
    }
}
