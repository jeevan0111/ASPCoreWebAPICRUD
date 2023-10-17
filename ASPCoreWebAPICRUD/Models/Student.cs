using System;
using System.Collections.Generic;

namespace ASPCoreWebAPICRUD.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string? Studentname { get; set; }
        public string? Studentgender { get; set; }
        public int? Age { get; set; }
        public string? Standard { get; set; }
        public string? Fathername { get; set; }
    }
}
