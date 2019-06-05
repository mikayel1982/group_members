using System;
using System.Collections.Generic;
using Students.Models;
using Microsoft.AspNetCore.Mvc;

namespace Students.Managers
{
    public class ValidationManager
    {
        public static bool hasValidName(string name, List<Student> students)
        {
            var hasValidName = true;

            foreach (var student in students)
            {
                if (student.name == name)
                    hasValidName = false;
            }

            return hasValidName;
        }

        public static bool hasValidGroup(string name, List<Group> groups)
        {
            var hasValidName = true;

            foreach (var group in groups)
            {
                if (group.name == name)
                    hasValidName = false;
            }

            return hasValidName;
        }
    }

}