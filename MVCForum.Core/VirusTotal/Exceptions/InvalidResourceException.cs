using System;

namespace MvcForum.Core.VirusTotal.Exceptions
{
    public class InvalidResourceException : Exception
    {
        public InvalidResourceException(string message) : base(message) { }
    }
}