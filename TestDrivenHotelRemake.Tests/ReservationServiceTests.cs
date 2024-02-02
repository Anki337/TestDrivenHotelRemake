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


        // Get Hotel Rooms, Get Reserved Rooms tests.
        //[Fact]  // This test will fail if all tests are run at once as it is tested at the end and thus not valid. Re-run alone to see the result.
        //public void GetHotelRooms_ShouldReturnListOfHotelRooms()
        //{
        //    // Given
        //    var expectedHotelRooms = new List<string> { "Room101", "Room102", "Room103" };

        //    // When
        //    var actualHotelRooms = ReservationService.GetHotelRooms();

        //    // Then
        //    actualHotelRooms.Should().BeEquivalentTo(expectedHotelRooms);
        //}

        //[Fact] // This test will fail if all tests are run at once as it is tested at the end and thus not valid. Re-run alone to see the result.
        //public void GetReservedRooms_ShouldReturnEmptyListInitially()
        //{
        //    // Given
        //    var expectedReservedRooms = new List<string>();

        //    // When

        //    var actualReservedRooms = ReservationService.GetReservedRooms();

        //    // Then
        //    actualReservedRooms.Should().BeEquivalentTo(expectedReservedRooms);
        //}


        [Fact] 
        public void GetHotelRooms_ShouldReturnEmptyListAfterAllReservations()
        {
            // Given
            var expectedHotelRooms = new List<string>();

            // When
            ReservationService.AddReservation("Room101");
            ReservationService.AddReservation("Room102");
            ReservationService.AddReservation("Room103");
            var actualHotelRooms = ReservationService.GetHotelRooms();

            // Then
            actualHotelRooms.Should().BeEquivalentTo(expectedHotelRooms);
        }


        // Add Reservation tests.

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldAddReservation()
        {
            // Given
            string room = "Room101";
            // When
            ReservationService.AddReservation(room);
            // Then
            ReservationService.GetReservedRooms().Should().Contain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldRemoveRoomFromHotelRooms()
        {
            // Given
            string room = "Room101";

            // When
            ReservationService.AddReservation(room);

            // Then
            var actualHotelRooms = ReservationService.GetHotelRooms();
            actualHotelRooms.Should().NotContain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsReserved_ShouldNotModifyLists()
        {
            // Given
            string room = "Room101";
            ReservationService.AddReservation(room);

            // When
            ReservationService.AddReservation(room);

            // Then
            var actualReservedRooms = ReservationService.GetReservedRooms();
            var actualHotelRooms = ReservationService.GetHotelRooms();
            actualReservedRooms.Should().BeEquivalentTo(new[] { "Room101" });
            actualHotelRooms.Should().BeEquivalentTo(new[] { "Room102", "Room103" });
        }

        [Fact]
        public void AddReservation_WhenRoomIsNotAvailable_ShouldNotAddReservation()
        {
            // Given
            string room = "Yellow";

            //When
            ReservationService.AddReservation(room);
            // Then
            ReservationService.GetReservedRooms().Should().NotContain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsNotAvailable_ShouldNotRemoveRoomFromHotelRooms()
        {
            // Given
            string room = "Yellow";

            // When
            ReservationService.AddReservation(room);

            // Then
            var actualHotelRooms = ReservationService.GetHotelRooms();
            actualHotelRooms.Should().NotContain(room);
        }

        [Fact]
        public void AddReservation_WhenHotelRoomsIsEmpty_ShouldNotModifyHotelRooms()
        {
            // Given
            string room = "Yellow";

            // When
            ReservationService.AddReservation(room);

            // Then
            var actualReservedRooms = ReservationService.GetReservedRooms();
            actualReservedRooms.Should().NotContain(room);
        }
        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldNotAddDuplicateReservation()
        {
            // Given
            string room = "Room101";

            // When
            ReservationService.AddReservation(room);
            ReservationService.AddReservation(room);

            // Then
            var actualReservedRooms = ReservationService.GetReservedRooms();
            actualReservedRooms.Should().ContainEquivalentOf(room);
        }


        // Cancel Reservation tests.

        [Fact]
        public void CancelReservation_WhenRoomIsReserved_ShouldCancelReservation()
        {
            // Given
            string room = "Room101";
            ReservationService.AddReservation(room);

            // When
            ReservationService.CancelReservation(room);

            // Then
            ReservationService.GetReservedRooms().Should().NotContain(room);
        }

        [Fact]
        public void CancelReservation_WhenRoomIsReserved_ShouldAddRoomToHotelRooms()
        {
            // Given
            string room = "Room101";
            ReservationService.AddReservation(room);

            // When
            ReservationService.CancelReservation(room);

            // Then
            ReservationService.GetHotelRooms().Should().Contain(room);
        }

        [Fact]
        public void CancelReservation_WhenRoomIsNotReserved_ShouldNotCancelReservation()
        {
            // Given
            string room = "Room101";

            // When
            ReservationService.CancelReservation(room);

            // Then
            ReservationService.GetReservedRooms().Should().NotContain(room);
        }
        [Fact]
        public void CancelReservation_WhenReservedRoomsIsEmpty_ShouldNotModifyLists()
        {
            // Given
            string room = "Room101";

            // When
            ReservationService.CancelReservation(room);

            // Then
            var actualReservedRooms = ReservationService.GetReservedRooms();
            var actualHotelRooms = ReservationService.GetHotelRooms();
            actualReservedRooms.Should().BeEmpty();
            actualHotelRooms.Should().BeEquivalentTo(new[] { "Room101", "Room102", "Room103" });
        }
        










    }
}
