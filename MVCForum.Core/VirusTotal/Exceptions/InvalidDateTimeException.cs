using System;

namespace MvcForum.Core.VirusTotal.Exceptions
{
    public class InvalidDateTimeException : Exception
    {
        public InvalidDateTimeException(string message) : base(message) { }
    }
}