using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using Students.Models;

namespace Students.Factories	//you can leave as is, or change to your own namespace
{
    public class StudentFactory
    {
        static string server = "localhost";
        static string db = "mydb"; //Change to your schema name
        static string port = "3306"; //Potentially 8889
        static string user = "root";
        static string pass = "root";
        internal static IDbConnection Connection
        {
            get
            {
                return new MySqlConnection($"Server={server};Port={port};Database={db};UserID={user};Password={pass};SslMode=None");
            }
        }

        //This method runs a query and stores the response in a list of dictionary records
        public IEnumerable<Student> FindAllStudents()
        {
            using (IDbConnection dbConnection = Connection)
            {
                var query = $"SELECT * FROM students LEFT JOIN groups ON students.groups_groupId=groups.groupId OR students.groups_groupId=NULL";
                dbConnection.Open();

                IEnumerable<Student> singleStudent = dbConnection.Query<Student, Group, Student>(query, (student, group) => { student.group = group; return student; }, splitOn: "groups_groupId,groupId");

                return singleStudent;
            }
        }

        public IEnumerable<Group> FindAllGroups()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Group>("SELECT * FROM groups").ToList();
            }
        }

        public void CreateStudent(Student newStudent)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO students (name, level, description, createdAt, updatedAt, groups_groupId) VALUES (@name, @level, @description, Now(), Now(), @groups_groupId)";

                if (newStudent.groups_groupId == 0)
                    query = "INSERT INTO students (name, level, description, createdAt, updatedAt, groups_groupId) VALUES (@name, @level, @description, Now(), Now(), NULL)";

                dbConnection.Open();
                dbConnection.Execute(query, newStudent);
            }
        }

        public void CreateGroup(Group newGroup)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO groups (name, location, description, createdAt, updatedAt) VALUES (@name, @location, @description, Now(), Now())";
                dbConnection.Open();
                dbConnection.Execute(query, newGroup);
            }
        }

        public Student GetStudentById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM students WHERE studentId = @Id";
                dbConnection.Open();
                return dbConnection.Query<Student>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public Group GetGroupById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM groups WHERE groupId = @Id";
                dbConnection.Open();
                return dbConnection.Query<Group>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public Group DropStudent(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "UPDATE students SET groups_groupId = Null WHERE studentId = @Id";
                dbConnection.Open();
                return dbConnection.Query<Group>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public Group EnrollStudent(int id, int groupId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "UPDATE students SET groups_groupId = @groupId WHERE studentId = @Id";
                dbConnection.Open();
                return dbConnection.Query<Group>(query, new { Id = id, groupId = groupId }).FirstOrDefault();
            }
        }
    }
}
