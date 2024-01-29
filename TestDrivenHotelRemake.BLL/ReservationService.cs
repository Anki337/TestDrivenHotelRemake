using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenHotelRemake.BLL
{
    public class ReservationService
    {
        private List<string> reservedRooms { get; set; } = new List<string>();
        private List<string> hotelRooms { get; set; } = new List<string> { "Room1", "Room2", "Room3" };
        

        public void AddReservation(string room)
        {
            if (hotelRooms.Contains(room))
            {
                hotelRooms.Remove(room);
                reservedRooms.Add(room);
            }
        }

        public void CancelReservation(string room)
        {
            if (reservedRooms.Contains(room))
            {
                reservedRooms.Remove(room);
                hotelRooms.Add(room);
            }
        }
        public List<string> GetReservedRooms()
        {
            return reservedRooms;
        }

        public List<string> GetHotelRooms()
        {
            return hotelRooms;
        }
    }
}
