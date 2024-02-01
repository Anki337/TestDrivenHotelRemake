using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrivenHotelRemake.BLL;

namespace TestDrivenHotelRemake.Tests
{
    public class ReservationServiceTests
    {
        private ReservationService reservationService;

        public ReservationServiceTests()
        {
            reservationService = new ReservationService();
            
        }

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldAddReservation()
        {
            // Given
            string room = "Room1";
            // When
            reservationService.AddReservation(room);
            // Then
            reservationService.GetReservedRooms().Should().Contain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldRemoveRoomFromHotelRooms()
        {
            // Given
            string room = "Room1";

            // When
            reservationService.AddReservation(room);

            // Then
            var actualHotelRooms = reservationService.GetHotelRooms();
            actualHotelRooms.Should().NotContain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsNotAvailable_ShouldNotAddReservation()
        {
            // Given
            string room = "Yellow";

            //When
            reservationService.AddReservation(room);
            // Then
            reservationService.GetReservedRooms().Should().NotContain(room);
        }
        [Fact]
        public void GetHotelRooms_ShouldReturnListOfHotelRooms()
        {
            // Given
            var expectedHotelRooms = new[] { "Room1", "Room2", "Room3" };

            // When
            var actualHotelRooms = reservationService.GetHotelRooms();

            // Then
            actualHotelRooms.Should().BeEquivalentTo(expectedHotelRooms);
        }
        [Fact]
        public void CancelReservation_WhenRoomIsReserved_ShouldCancelReservation()
        {
            // Given
            string room = "Room1";
            reservationService.AddReservation(room);

            // When
            reservationService.CancelReservation(room);

            // Then
            reservationService.GetReservedRooms().Should().NotContain(room);
        }
        [Fact]
        public void CancelReservation_WhenRoomIsReserved_ShouldAddRoomToHotelRooms()
        {
            // Given
            string room = "Room1";
            reservationService.AddReservation(room);

            // When
            reservationService.CancelReservation(room);

            // Then
            reservationService.GetHotelRooms().Should().Contain(room);
        }
        [Fact]
        public void CancelReservation_WhenRoomIsNotReserved_ShouldNotCancelReservation()
        {
            // Given
            string room = "Room1";

            // When
            reservationService.CancelReservation(room);

            // Then
            reservationService.GetReservedRooms().Should().NotContain(room);
        }
        [Fact]
        public void GetReservedRooms_ShouldReturnEmptyListInitially()
        {
            // Given
            var expectedReservedRooms = new List<string>();

            // When
            var actualReservedRooms = reservationService.GetReservedRooms();

            // Then
            actualReservedRooms.Should().BeEquivalentTo(expectedReservedRooms);
        }








    }
}
