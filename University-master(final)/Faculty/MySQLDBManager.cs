using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Faculty
{
    public class MySQLDBManager: IDBManager
    {
        private string connString;

        public MySQLDBManager()
        {
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public IList<Student> RetrieveStudents()
        {
            IList<Student> studentList = new List<Student>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM student";
                
                MySqlCommand cmd = new MySqlCommand(statement,conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.ID = reader.GetInt32("ID");
                        student.Name = reader.GetString("Name");
                        student.BirthDate = reader.GetDateTime("BirthDate");
                        student.Address = reader.GetString("Address");
                        studentList.Add(student);
                    }
                }
            }

            return studentList;
        }
        public IList<Student> RetrieveStudents(int Courseid)
        {
            IList<Student> studentList = new List<Student>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "select * from student where id=(select IDStudent from register where IDCourse="+Convert.ToString(Courseid)+")";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.ID = reader.GetInt32("ID");
                        student.Name = reader.GetString("Name");
                        student.BirthDate = reader.GetDateTime("BirthDate");
                        student.Address = reader.GetString("Address");
                        studentList.Add(student);
                    }
                }
            }

            return studentList;
        }


        public IList<Course> RetrieveCourses()
        {
            IList<Course> coursList = new List<Course>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM courses";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Course course = new Course();
                        course.ID = reader.GetInt32("ID");
                        course.Name = reader.GetString("Name");
                        course.Teacher = reader.GetString("Teacher");
                        course.Year = reader.GetInt32("Year");
                        coursList.Add(course);
                    }
                }
            }

            return coursList;
        }
        public void AddStudent(Student student)
        {
            using(MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd=new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO student(id, name, birthdate, address) VALUES(@id, @name, @birthdate, @address)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", student.ID);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@birthdate", student.BirthDate);
                cmd.Parameters.AddWithValue("@address", student.Address);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddCourse(Course course)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO courses(id, name, teacher, year) VALUES(@id, @name, @teacher, @year)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", course.ID);
                cmd.Parameters.AddWithValue("@name", course.Name);
                cmd.Parameters.AddWithValue("@teacher", course.Teacher);
                cmd.Parameters.AddWithValue("@year", course.Year);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateStudent(Student student)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateStudent";

                cmd.Parameters.Add(new MySqlParameter("StudentID", student.ID));
                cmd.Parameters.Add(new MySqlParameter("StudentName", student.Name));
                cmd.Parameters.Add(new MySqlParameter("StudentBirthDate", student.BirthDate));
                cmd.Parameters.Add(new MySqlParameter("StudentAddress", student.Address));

                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateCourse(Course course)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update lab2.courses set name=@name,teacher=@teacher,year=@year where id=@id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", course.ID);
                cmd.Parameters.AddWithValue("@name", course.Name);
                cmd.Parameters.AddWithValue("@teacher", course.Teacher);
                cmd.Parameters.AddWithValue("@year", course.Year);
                cmd.ExecuteNonQuery();
            }
        }
        public void EnrollStudent(Enroll enroll)
        {

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO register(IDStudent,IDCourse) VALUES(@ids, @idc)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@ids", enroll.IDStudent);
                cmd.Parameters.AddWithValue("@idc", enroll.IDCourse);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteCourse(Course course)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM  courses WHERE ID=@id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", course.ID);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(Student student)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM  student WHERE ID=@id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", student.ID);
            
                cmd.ExecuteNonQuery();
            }
        }
    }
}
