using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenHotelRemake.BLL
{
    public static class ReservationService
    {
        public static List<string> reservedRooms { get; set; } = new List<string>();
        public static List<string> hotelRooms { get; set; } = new List<string>() { "Room101", "Room102", "Room103" };
        

        public static void AddReservation(string room)
        {
            if (hotelRooms.Contains(room) && !reservedRooms.Contains(room))
            {
                hotelRooms.Remove(room);
                reservedRooms.Add(room);
            }
          
        }

        public static void CancelReservation(string room)
        {
            if (reservedRooms.Contains(room) && !hotelRooms.Contains(room))
            {
                reservedRooms.Remove(room);
                hotelRooms.Add(room);
            }

        }

        public static List<string> GetReservedRooms()
        {
            return reservedRooms;
        }

        public static List<string> GetHotelRooms()
        {
            return hotelRooms;
        }
    }
}
