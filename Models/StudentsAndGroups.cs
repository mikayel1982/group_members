using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Students.Models
{
    public class StudentsAndGroups
    {
        public List<Group> groups { get; set; }

        public List<Student> students { get; set; }

        public Group group { get; set; }

        public Student student { get; set; }

    }
}
