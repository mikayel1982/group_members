using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Students.Models;
using Students.Managers;
using Students.Factories;

namespace Students.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentFactory studentFactory;

        public HomeController()
        {
            this.studentFactory = new StudentFactory();
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            var students = this.studentFactory.FindAllStudents();
            var groups = this.studentFactory.FindAllGroups();
            var studentsAndGroups = new StudentsAndGroups();

            studentsAndGroups.students = students.ToList();
            studentsAndGroups.groups = groups.ToList();

            return View("Index", studentsAndGroups);
        }

        [HttpGet("/groups")]
        public IActionResult AllGroups()
        {
            var groups = this.studentFactory.FindAllGroups();
            var studentsAndGroups = new StudentsAndGroups();
            studentsAndGroups.groups = groups.ToList();

            return View("groups", studentsAndGroups);
        }

        [HttpPost("/student_create")]
        public IActionResult StudentCreate(StudentsAndGroups modelData)
        {
            if (ModelState.IsValid)
            {
                var studentsAll = this.studentFactory.FindAllStudents();

                if (!ValidationManager.hasValidName(modelData.student.name, studentsAll.ToList()))
                {
                    ViewBag.ErrorMessage = "The student already exists!";
                }
                else
                {
                    this.studentFactory.CreateStudent(modelData.student);
                    modelData.student = null;
                    ModelState.Clear();
                }
            }

            var students = this.studentFactory.FindAllStudents();
            var groups = this.studentFactory.FindAllGroups();
            var studentsAndGroups = new StudentsAndGroups();

            studentsAndGroups.students = students.ToList();
            studentsAndGroups.groups = groups.ToList();

            return View("Index", studentsAndGroups);
        }

        [HttpPost("/group_create")]
        public IActionResult GroupCreate(StudentsAndGroups modelData)
        {
            if (ModelState.IsValid)
            {
                var groupsAll = this.studentFactory.FindAllGroups();

                if (!ValidationManager.hasValidGroup(modelData.group.name, groupsAll.ToList()))
                {
                    ViewBag.ErrorMessage = "The group already exists!";
                }
                else
                {
                    this.studentFactory.CreateGroup(modelData.group);
                    modelData.group = null;
                    ModelState.Clear();
                }
            }

            var groups = this.studentFactory.FindAllGroups();
            var studentsAndGroups = new StudentsAndGroups();

            studentsAndGroups.groups = groups.ToList();

            return View("groups", studentsAndGroups);
        }

        [HttpGet("/student/{id}")]
        public IActionResult Student(int id)
        {

            var studentsAndGroups = new StudentsAndGroups();

            var student = this.studentFactory.GetStudentById(id);

            var group = this.studentFactory.GetGroupById(student.groups_groupId);

            if (student == null)
                return RedirectToAction("Index");

            studentsAndGroups.student = student;
            studentsAndGroups.group = group;

            return View("student", studentsAndGroups);
        }

        [HttpGet("/group/{id}")]
        public IActionResult Group(int id)
        {
            var studentsAndGroups = new StudentsAndGroups();

            var students = this.studentFactory.FindAllStudents();

            var group = this.studentFactory.GetGroupById(id);

            if (group == null)
                return RedirectToAction("groups");

            studentsAndGroups.students = students.ToList();
            studentsAndGroups.group = group;

            return View("group", studentsAndGroups);
        }

        [HttpGet("/drop/{id}/{groupId}")]
        public IActionResult Drop(int id, int groupId)
        {
            var studentsAndGroups = new StudentsAndGroups();

            this.studentFactory.DropStudent(id);

            var students = this.studentFactory.FindAllStudents();

            var group = this.studentFactory.GetGroupById(groupId);

            if (group == null)
                return RedirectToAction("groups");

            studentsAndGroups.students = students.ToList();
            studentsAndGroups.group = group;

            return View("group", studentsAndGroups);
        }

        [HttpGet("/enroll/{id}/{groupId}")]
        public IActionResult Enroll(int id, int groupId)
        {
            var studentsAndGroups = new StudentsAndGroups();

            this.studentFactory.EnrollStudent(id, groupId);

            var students = this.studentFactory.FindAllStudents();

            var group = this.studentFactory.GetGroupById(groupId);

            if (group == null)
                return RedirectToAction("groups");

            studentsAndGroups.students = students.ToList();
            studentsAndGroups.group = group;

            return View("group", studentsAndGroups);
        }
    }
}
