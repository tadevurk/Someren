using SomerenDAL;
using SomerenModel;
using System.Collections.Generic;

namespace SomerenLogic
{
     public class TeacherService
    {
        TeachersDao teacherdb;

        public TeacherService()
        {
            teacherdb = new TeachersDao();
        }

        public List<Teacher> GetTeachers()
        {
            List<Teacher> teachers = teacherdb.GetAllTeachers();
            return teachers;
        }
    }
}
