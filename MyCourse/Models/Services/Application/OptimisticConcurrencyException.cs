using System;
using System.Runtime.Serialization;

namespace MyCourse.Models.Exceptions.Application
{
    public class OptimisticConcurrencyException : Exception
    {
        public OptimisticConcurrencyException() : base($"Couldn't update row because it was updated by another use")
        {
        }
    }
}