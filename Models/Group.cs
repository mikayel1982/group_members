using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Students.Models
{
    public class Group
    {
        [Key]
        public int groupId { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Group Location")]
        public string location { get; set; }

        [Display(Name = "Group Description")]
        public string description { get; set; }

        public IEnumerable<Student> students { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public Group()
        {
            students = new List<Student>();
        }
    }
}

