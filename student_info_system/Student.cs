using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace student_info_system
{
    public class Student
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private double _gpa;

        public string fullName
        {
            get { return _firstName + ' ' + _lastName; }
        }

        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value;  }
        }

        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public double gpa
        {
            get { return _gpa; }
            set { _gpa = value; }
        }

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public List<Student> getAll()
        {
            List<Student> students = new List<Student>();
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=admin;password=12345,database=student_info");
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM student", conn);
            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student stu = new Student();
                    stu.id = (string)reader["id"];
                    stu.firstName = (string)reader["firstName"];
                    stu.lastName = (string)reader["lastName"];
                    stu.gpa = (float)reader["gpa"];
                    students.Add(stu);
                }
            }
            return students;
        }

        public void updateGpa(string studentId, float newGpa)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=admin;password=12345,database=student_info");
            MySqlCommand cmd = new MySqlCommand("UPDATE student SET gpa = " + newGpa.ToString() + " WHERE student.id = " + studentId, conn);
            using(conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
            }
            return;
        }

        public Student createStudent(string fName, string lName, float gPA)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=admin;password=12345,database=student_info");
            MySqlCommand cmd = new MySqlCommand("INSERT INTO student (firstName, lastName, gpa) VALUES (" +  fName + ", " + lName + ", " + gPA.ToString() + ");", conn);
            Student createdStudent = new Student();
            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                createdStudent.firstName = fName;
                createdStudent.lastName = lName;
                createdStudent.gpa = gPA;
            }
            return createdStudent;
        }
    }
}
