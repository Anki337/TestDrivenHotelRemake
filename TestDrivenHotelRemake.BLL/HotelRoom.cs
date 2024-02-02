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

        public HotelRoom(int roomNumber, int numberOfBeds, int roomPrice)
        {
            RoomNumber = roomNumber;
            NumberOfBeds = numberOfBeds;
            RoomPrice = roomPrice;
        }
        public HotelRoom()
        {

        }
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
