using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenLogic
{
    public class ActivityStudentService
    {
        ActivityStudentDao activityStudentDao;

        public ActivityStudentService()
        {
            activityStudentDao = new ActivityStudentDao();
        }

        public List<Student> GetParticipants(int activityId)
        {
            return activityStudentDao.GetParticipators(activityId);
        }

        public List<Student> GetNotParticipators(int activityId)
        {
            return activityStudentDao.GetNotParticipators(activityId);
        }

        public void DeleteParticipant(ActivityStudent student)
        {
            activityStudentDao.DeleteParticipant(student);
        }

        public void AddParticipant(ActivityStudent student)
        {
            activityStudentDao.AddParticipant(student);
        }
    }
}
