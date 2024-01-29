using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestDrivenHotelRemake.BLL;

namespace TestDrivenHotelRemake.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ReservationService reservationService;
        public IndexModel(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }
        public List<string> AllRooms { get; set; }
        public List<string> ReservedRooms { get; set; } 
        public void OnGet()
        {
            AllRooms = reservationService.GetHotelRooms();
            ReservedRooms = reservationService.GetReservedRooms();
        }
        public IActionResult OnPostBookRoom(string room)
        {
            reservationService.AddReservation(room);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostCancelRoom(string room)
        {
            reservationService.CancelReservation(room);
            return RedirectToPage("Index");
        }
    }
}
