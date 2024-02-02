using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotelRemake.BLL;

namespace TestDrivenHotelRemake.Pages
{
    public class IndexModel : PageModel
    {
        
        public  List<string> AllRooms { get; set; }
        public  List<string> ReservedRooms { get; set; }
        public void OnGet()
        {
            AllRooms = ReservationService.GetHotelRooms();
            ReservedRooms = ReservationService.GetReservedRooms();
        }
        public IActionResult OnPostBookRoom(string room)
        {
            ReservationService.AddReservation(room);
            return Page();
        }
        public IActionResult OnPostCancelRoom(string room)
        {
            ReservationService.CancelReservation(room);
            return Page();
        }
    }
}
