using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace TestDrivenHotelRemake.BLL
{
    public static class ReservationService
    {
        public static List<HotelRoom> reservedRooms { get; set; } = new List<HotelRoom>();
        public static List<HotelRoom> hotelRooms { get; set; } = new List<HotelRoom>()
        {
            new HotelRoom(101, 2, 300),
            new HotelRoom(102, 3, 400),
            new HotelRoom(103, 4, 500)
        };
        
        public static bool IsRoomAvailable(HotelRoom room, DateTime startDate, DateTime endDate)
        {
            string errorMessage = null;

            if (reservedRooms.Contains(room))
            {
                errorMessage = "Room is already reserved";
                return false;
            }
            return true;

        }


        public static void AddReservation(HotelRoom room, DateTime startDate, DateTime endDate)
        {
            if (hotelRooms.Contains(room) && !reservedRooms.Contains(room))
            {
                hotelRooms.Remove(room);
                room.ReserveRoom(startDate, endDate);
                reservedRooms.Add(room);
            }
          
        }

        public static void CancelReservation(HotelRoom room)
        {
            if (reservedRooms.Contains(room) && !hotelRooms.Contains(room))
            {
                reservedRooms.Remove(room);
                hotelRooms.Add(room);
            }

        }

        public static List<HotelRoom> GetReservedRooms()
        {
            return reservedRooms;
        }

        public static List<HotelRoom> GetHotelRooms()
        {
            return hotelRooms;
        }
    }
}
