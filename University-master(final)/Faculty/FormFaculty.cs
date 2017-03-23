using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Faculty
{
    public partial class FormFaculty : Form
    {
        public FormFaculty()
        {
            InitializeComponent();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = RetrieveStudentInformation();

                IDBManager db = new MySQLDBManager();
                db.AddStudent(student);
                EmptyControlsStudent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = RetrieveStudentInformation();

                IDBManager db = new MySQLDBManager();
                db.UpdateStudent(student);
                EmptyControlsStudent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                IDBManager dbManager = new MySQLDBManager();

                gridStudents.DataSource = dbManager.RetrieveStudents();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmptyControlsStudent()
        {
            txtStudentID.Text = string.Empty;
            txtStudentName.Text = string.Empty;
            dtpStudentBirthDate.Value = DateTime.Now;
            txtStudentAddress.Text = string.Empty;
            gridStudents.SelectedRows[0].Selected = false;
        }
        private void EmptyControlsCours()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            dataGridView1.SelectedRows[0].Selected = false;
        }
        private void EmptyControlsEnroll()
        {
            textBox6.Text = string.Empty;
            textBox5.Text = string.Empty;
        }
        private Student RetrieveStudentInformation()
        {
            Student student = new Student();
            student.ID = Convert.ToInt32(txtStudentID.Text);
            student.Name = txtStudentName.Text;
            student.BirthDate = dtpStudentBirthDate.Value;
            student.Address = txtStudentAddress.Text;
            return student;
        }

        private void gridStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (gridStudents.SelectedRows.Count > 0)
            {
                Student student = gridStudents.SelectedRows[0].DataBoundItem as Student;
                if (student != null)
                {
                    txtStudentName.Text = student.Name;
                    txtStudentID.Text = student.ID.ToString();
                    dtpStudentBirthDate.Value = student.BirthDate;
                    txtStudentAddress.Text = student.Address;
                }
            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = RetrieveStudentInformation();

                IDBManager db = new MySQLDBManager();
                db.DeleteStudent(student);
                EmptyControlsStudent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Course RetrieveCourseInformation()
        {
            Course course = new Course();
            course.ID = Convert.ToInt32(textBox3.Text);
            course.Name = textBox2.Text;
            course.Teacher = textBox4.Text;
            course.Year = Convert.ToInt32(textBox1.Text);
            return course;
        }

        private Enroll RetrieveEnrollInformation()
        {
            Enroll course = new Enroll();
            course.IDStudent = Convert.ToInt32(textBox6.Text);
            course.IDCourse= Convert.ToInt32(textBox5.Text);
            return course;
        }
        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            try
            {
                Course course = RetrieveCourseInformation();

                IDBManager db = new MySQLDBManager();
                db.AddCourse(course);
                EmptyControlsCours();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditCourse_Click(object sender, EventArgs e)
        {
            try
            {
                Course course = RetrieveCourseInformation();

                IDBManager db = new MySQLDBManager();
                db.UpdateCourse(course);
                EmptyControlsCours();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            try
            {
                Course course = RetrieveCourseInformation();

                IDBManager db = new MySQLDBManager();
                db.DeleteCourse(course);
                EmptyControlsCours();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRetrieve2_Click(object sender, EventArgs e)
        {
            try
            {
                IDBManager dbManager = new MySQLDBManager();

                dataGridView1.DataSource = dbManager.RetrieveCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
         }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            try
            {

                IDBManager db = new MySQLDBManager();
                Enroll enroll = RetrieveEnrollInformation();
                db.EnrollStudent(enroll);
                EmptyControlsEnroll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnListStudents_Click(object sender, EventArgs e)
        {
            try
            {
                IDBManager dbManager = new MySQLDBManager();
                int CourseId = Convert.ToInt32(textBox7.Text);
                gridStudents.DataSource = dbManager.RetrieveStudents(CourseId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
