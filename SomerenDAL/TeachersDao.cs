using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SomerenModel;


namespace SomerenDAL
{
    public class TeachersDao : BaseDao
    {
        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT lecturerNumber, name, supervisorStatus FROM [Teachers]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Teacher> ReadTables(DataTable dataTable)
        {
            List<Teacher> teachers = new List<Teacher>();

            foreach (DataRow dr in dataTable.Rows)
            {
                //splitting the firstname and lastname from fullname:
                string name = (string)dr["name"];
                string[] fullName = name.Split(' ');

                Teacher teacher = new Teacher()
                {
                    Number = (int)dr["lecturerNumber"],
                    FirstName = fullName[0],
                    LastName = fullName[1],
                    Supervisor = (bool)dr["supervisorStatus"]
                };
                teachers.Add(teacher);
            }
            return teachers;
        }
    }
}
