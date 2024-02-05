using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDrivenHotelRemake.BLL
{
    public class HotelRoom
    {
        public readonly int RoomNumber;
        public readonly int NumberOfBeds;
        public readonly int RoomPrice;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        



        public HotelRoom(int roomNumber, int numberOfBeds, int roomPrice)
        {
            RoomNumber = roomNumber;
            NumberOfBeds = numberOfBeds;
            RoomPrice = roomPrice;
            
        }
        public void ReserveRoom(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        // Override Equals and GetHashCode to compare objects by value instead of reference.
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            HotelRoom otherRoom = (HotelRoom)obj;
            return RoomNumber == otherRoom.RoomNumber &&
                   NumberOfBeds == otherRoom.NumberOfBeds &&
                   RoomPrice == otherRoom.RoomPrice;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(RoomNumber, NumberOfBeds, RoomPrice);
        }

    }

    

}
