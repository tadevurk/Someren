using System.Collections.Generic;
using System.Data.SqlClient;
using SomerenModel;
using System.Data;

namespace SomerenDAL
{
    public class RoomDao : BaseDao
    {
        public List<Room> GetAllRooms()
        {
            string query = "SELECT [roomNumber],[roomType],[capacity] FROM [rooms]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }        

        private List<Room> ReadTables(DataTable dataTable)
        {
            List<Room> rooms = new List<Room>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Room room = new Room()
                {
                    Number = (int)dr["roomNumber"],
                    Type = (bool)dr["roomType"],
                    Capacity = (int)dr["capacity"]
                };
                rooms.Add(room);
            }
            return rooms;
        }
    }
}
