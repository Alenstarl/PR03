using System;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Sql.Entities
{
    public partial class Student
    {
        public string StudentId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Nickname { get; set; }
        public DateTime JoinedAt { get; set; }
        public int? SchoolId { get; set; }

        public virtual School? School { get; set; }
    }
}
