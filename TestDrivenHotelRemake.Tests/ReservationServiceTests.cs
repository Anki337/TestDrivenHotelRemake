using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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

        // Get Hotel Rooms, Get Reserved Rooms tests.

        [Fact]
        public void GetHotelRooms_ShouldReturnListOfHotelRooms()
        {
            // Given
            var expectedHotelRooms = new[] { "Room101", "Room102", "Room103" };

            // When
            var actualHotelRooms = reservationService.GetHotelRooms();

            // Then
            actualHotelRooms.Should().BeEquivalentTo(expectedHotelRooms);
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


        // Add Reservation tests.

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldAddReservation()
        {
            // Given
            string room = "Room101";
            // When
            reservationService.AddReservation(room);
            // Then
            reservationService.GetReservedRooms().Should().Contain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldRemoveRoomFromHotelRooms()
        {
            // Given
            string room = "Room101";

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
        public void AddReservation_WhenRoomIsNotAvailable_ShouldNotRemoveRoomFromHotelRooms()
        {
            // Given
            string room = "Yellow";

            // When
            reservationService.AddReservation(room);

            // Then
            var actualHotelRooms = reservationService.GetHotelRooms();
            actualHotelRooms.Should().NotContain(room);
        }
        [Fact]
        public void AddReservation_WhenHotelRoomsIsEmpty_ShouldNotModifyHotelRooms()
        {
            // Given
            string room = "Yellow";

            // When
            reservationService.AddReservation(room);

            // Then
            var actualReservedRooms = reservationService.GetReservedRooms();
            actualReservedRooms.Should().NotContain(room);
        }
        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldNotAddDuplicateReservation()
        {
            // Given
            string room = "Room101";

            // When
            reservationService.AddReservation(room);
            reservationService.AddReservation(room);

            // Then
            var actualReservedRooms = reservationService.GetReservedRooms();
            actualReservedRooms.Should().ContainSingle(room);
        }




        // Cancel Reservation tests.

        [Fact]
        public void CancelReservation_WhenRoomIsReserved_ShouldCancelReservation()
        {
            // Given
            string room = "Room101";
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
            string room = "Room101";
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
            string room = "Room101";

            // When
            reservationService.CancelReservation(room);

            // Then
            reservationService.GetReservedRooms().Should().NotContain(room);
        }
        [Fact]
        public void CancelReservation_WhenHotelRoomsIsEmpty_ShouldNotModifyLists()
        {
            // Given
            string room = "Room101";

            // When
            reservationService.CancelReservation(room);

            // Then
            var actualReservedRooms = reservationService.GetReservedRooms();
            var actualHotelRooms = reservationService.GetHotelRooms();
            actualReservedRooms.Should().BeEmpty();
            actualHotelRooms.Should().BeEquivalentTo(new[] { "Room101", "Room102", "Room103" });
        }










    }
}
