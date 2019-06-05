using System;
using System.ComponentModel.DataAnnotations;

namespace Students.Models
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Student Level (1-10)")]
        public int level { get; set; }

        [Display(Name = "Optional Description")]
        public string description { get; set; }

        [Display(Name = "Assigned Group")]
        public int groups_groupId { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public Group group { get; set; }

    }
}
