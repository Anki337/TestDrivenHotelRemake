using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotelRemake.BLL;

namespace TestDrivenHotelRemake.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ReservationService _reservationService;
        public IndexModel(ReservationService reservationService)
        {
            _reservationService = reservationService;
            AllRooms = _reservationService.GetHotelRooms();
            ReservedRooms = _reservationService.GetReservedRooms();
        }
        public  List<string> AllRooms { get; set; }
        public  List<string> ReservedRooms { get; set; }
        public void OnGet()
        {
            
        }
        public IActionResult OnPostBookRoom(string room)
        {
            _reservationService.AddReservation(room);
            return Page();
        }
        public IActionResult OnPostCancelRoom(string room)
        {
            _reservationService.CancelReservation(room);
            return Page();
        }
    }
}
