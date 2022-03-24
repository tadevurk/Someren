using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;

namespace SomerenLogic
{
    public class RoomService
    {
        RoomDao roomdb;

        public RoomService()
        {
            roomdb = new RoomDao();
        }

        public List<Room> GetRooms()
        {
            List<Room> rooms = roomdb.GetAllRooms();
            return rooms;
        }
    }
}
