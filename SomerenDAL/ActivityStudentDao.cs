using SomerenModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenDAL
{
    public class ActivityStudentDao : BaseDao
    {
        public List<Student> GetParticipators(int activityId)
        {
            StudentDao studentDao = new StudentDao();

            string query = "SELECT Activity_ID, studentNumber, studentName, birthDate " +
                "FROM students " +
                "JOIN ActivityStudent ON students.studentNumber = ActivityStudent.Student_ID " +
                "JOIN Activities ON Activities.ActivityId = Activity_ID " +
                "WHERE Activity_ID=@Activity_ID";

            SqlParameter[] dateParameters = { new SqlParameter("@Activity_ID", activityId) };
            return studentDao.ReadTables(ExecuteSelectQuery(query, dateParameters));
        }

        public List<Student> GetNotParticipators(int activityId)
        {
            StudentDao studentDao = new StudentDao();

            string query = "SELECT studentNumber, studentName, birthDate " +
                "FROM students " +
                "WHERE studentNumber NOT IN(SELECT ActivityStudent.Student_ID " +
                "FROM ActivityStudent " +
                "WHERE Activity_ID = @Activity_ID)";

            SqlParameter[] dateParameters = { new SqlParameter("@Activity_ID", activityId) };
            return studentDao.ReadTables(ExecuteSelectQuery(query, dateParameters));
        }

        public void AddParticipant(ActivityStudent student)
        {
            string query = "INSERT INTO [ActivityStudent] VALUES (@Student_ID, @Activity_ID) SELECT SCOPE_IDENTITY()";

            SqlParameter[] sqlParameters =
            {
            new SqlParameter("@Student_ID", student.StudentId),
            new SqlParameter("@Activity_ID", student.ActivityId)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteParticipant(ActivityStudent student)
        {
            string query = "DELETE FROM [ActivityStudent] WHERE Activity_ID=@Activity_ID AND Student_ID=@Student_ID";
            SqlParameter[] sqlParameters =
{
            new SqlParameter("@Student_ID", student.StudentId),
            new SqlParameter("@Activity_ID", student.ActivityId)
            };

            ExecuteEditQuery(query, sqlParameters);
        }
    }


}
