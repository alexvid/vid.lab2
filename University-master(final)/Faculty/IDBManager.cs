using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faculty
{
    public interface IDBManager
    {
        IList<Student> RetrieveStudents();
        IList<Course> RetrieveCourses();
        IList<Student> RetrieveStudents(int Id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        void EnrollStudent(Enroll enroll);
    }
}
