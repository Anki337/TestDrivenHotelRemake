using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotelRemake.BLL;

namespace TestDrivenHotelRemake.Pages
{
    public class IndexModel : PageModel
    {
        
        public  List<HotelRoom> AllRooms { get; set; }
        public  List<HotelRoom> ReservedRooms { get; set; }
        public void OnGet()
        {
            AllRooms = ReservationService.GetHotelRooms();
            ReservedRooms = ReservationService.GetReservedRooms();
        }
        public IActionResult OnPostBookRoom(int RoomNumber, int NumberOfBeds, int RoomPrice)
        {
            var room = new HotelRoom(RoomNumber, NumberOfBeds, RoomPrice);
            ReservationService.AddReservation(room);
            return RedirectToPage();
        }
        public IActionResult OnPostCancelRoom(int RoomNumber, int NumberOfBeds, int RoomPrice)
        {
            var room = new HotelRoom(RoomNumber, NumberOfBeds, RoomPrice);
            ReservationService.CancelReservation(room);
            return RedirectToPage();
        }
    }
}
