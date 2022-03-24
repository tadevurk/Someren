using System.Collections.Generic;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class ActivityService
    {
        ActivityDao activitydb;

        public ActivityService()
        {
            activitydb = new ActivityDao();
        }

        public List<Activity> GetActivities()
        {
            List<Activity> activities = activitydb.GetAllActivities();
            return activities;
        }

        public void AddActivity(Activity activity)
        {
            activitydb.Add(activity);
        }

        public void RemoveActivity(Activity activity)
        {
            activitydb.Remove(activity);
        }

        public void ChangeActivity(Activity activity)
        {
            activitydb.Change(activity);
        }
    }
}
