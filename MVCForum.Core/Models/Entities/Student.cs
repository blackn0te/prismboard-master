namespace MvcForum.Core.Models.Entities
{
    using System;
    using Interfaces;
    using Utilities;

    public partial class Student
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string AdminNo { get; set; }
        public string PemGroup { get; set; }
        public string Pem { get; set; }
        public string StudentCourse { get; set; }
        public string StudentSchool { get; set; }
        public string AltEmail { get; set; }

    }
}
