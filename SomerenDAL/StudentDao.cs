using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;

namespace SomerenDAL
{
    public class StudentDao : BaseDao
    {      
        public List<Student> GetAllStudents()
        {
            string query = "SELECT studentNumber, studentName, birthDate FROM [students]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Student> ReadTables(DataTable dataTable)
        {
            List<Student> students = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
                string name = (string)dr["studentName"];
                string[] fullName = name.Split(' ');
                // devide the fullname into lastname and firstname.

                Student student = new Student()
                {
                    Number = (int)dr["studentNumber"],
                    FirstName = fullName[0],
                    LastName = fullName[1],
                    BirthDate = (DateTime)dr["birthDate"]
                };
                students.Add(student);
            }
            return students;
        }
    }
}
