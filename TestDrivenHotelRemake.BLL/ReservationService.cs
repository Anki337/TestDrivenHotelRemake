using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        

        public static void AddReservation(HotelRoom room)
        {
            if (hotelRooms.Contains(room) && !reservedRooms.Contains(room))
            {
                hotelRooms.Remove(room);
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
