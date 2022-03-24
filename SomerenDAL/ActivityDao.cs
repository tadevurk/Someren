using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SomerenModel;
using System.Data;
using System.Configuration;

namespace SomerenDAL
{

    public class ActivityDao : BaseDao
    {
        public List<Activity> GetAllActivities()
        {
            string query = "SELECT ActivityId, ActivityName, ActivityDateTime FROM [Activities]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Activity> ReadTables(DataTable dataTable)
        {
            List<Activity> activities = new List<Activity>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Activity activity = new Activity()
                {
                    ActivityId = (int)dr["ActivityId"],
                    ActivityName = (string)dr["ActivityName"],
                    ActivityDateTime = (DateTime)dr["ActivityDateTime"]
                };
                activities.Add(activity);
            }
            return activities;
        }

        public void Add(Activity activity)
        {
            string query =
                "INSERT INTO [Activities] (ActivityId, ActivityName, ActivityDateTime)" +
                "VALUES (@ActivityId, @ActivityName, @ActivityDateTime) SELECT SCOPE_IDENTITY()";

            List<Activity> currentList = GetAllActivities();

            activity.ActivityId = 1;

            foreach (Activity dr in currentList)
            {
                while (activity.ActivityId == dr.ActivityId)
                {
                    activity.ActivityId++;
                }
            }

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@ActivityId", activity.ActivityId),
                new SqlParameter("@ActivityName", activity.ActivityName),
                new SqlParameter("@ActivityDateTime", activity.ActivityDateTime)                
            };

            ExecuteEditQuery(query, sqlParameters);
        }

        public void Remove(Activity activity)
        {
            string query = "DELETE FROM Activities WHERE ActivityId = @ActivityId";
            SqlParameter[] sqlParameters = { new SqlParameter("@ActivityId", activity.ActivityId) };

            ExecuteEditQuery(query, sqlParameters);
        }

        public void Change(Activity activity)
        {
            string query = "UPDATE [Activities] SET ActivityId = @ActivityId, ActivityName = @ActivityName, ActivityDateTime = @ActivityDateTime" +
                " WHERE ActivityId = @ActivityId";

            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@ActivityId", activity.ActivityId),
                new SqlParameter("@ActivityName", activity.ActivityName),
                new SqlParameter("@ActivityDateTime", activity.ActivityDateTime)
            };

            ExecuteEditQuery(query, sqlParameters);
        }
    }

}
