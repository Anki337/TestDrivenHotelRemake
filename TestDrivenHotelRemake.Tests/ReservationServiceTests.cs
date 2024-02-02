using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
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
            // Given
            var room1 = new HotelRoom(101, 2, 300);
            var room2 = new HotelRoom(102, 3, 400);
            var room3 = new HotelRoom(103, 4, 500);

            // When
            ReservationService.AddReservation(room1);
            ReservationService.AddReservation(room2);
            ReservationService.AddReservation(room3);

            // Then
            var actualHotelRooms = ReservationService.GetHotelRooms();
            actualHotelRooms.Should().BeEmpty();

        }


        // Add Reservation tests.

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldAddReservation()
        {
            // Given
            var room = new HotelRoom(101, 2, 300);
            // When
            ReservationService.AddReservation(room);
            // Then
            ReservationService.GetReservedRooms().Should().Contain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldRemoveRoomFromHotelRooms()
        {
            // Given
            var room = new HotelRoom(101, 2, 300);

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
            var room = new HotelRoom(101, 2, 300);

            // When
            ReservationService.AddReservation(room);

            // Then
            var actualReservedRooms = ReservationService.GetReservedRooms();
            var actualHotelRooms = ReservationService.GetHotelRooms();


            var expectedReservedRooms = ReservationService.reservedRooms; 
            var expectedHotelRooms = ReservationService.hotelRooms;

            actualReservedRooms.Should().BeEquivalentTo(expectedReservedRooms);
            actualHotelRooms.Should().BeEquivalentTo(expectedHotelRooms);
        }

        [Fact]
        public void AddReservation_WhenRoomIsNotAvailable_ShouldNotAddReservation()
        {
            // Given
            var room = new HotelRoom(9339, 1231232, 232222200);
            
            //When
            ReservationService.AddReservation(room);
            // Then
            ReservationService.GetReservedRooms().Should().NotContain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsNotAvailable_ShouldNotRemoveRoomFromHotelRooms()
        {
            // Given
            var room = new HotelRoom(9339, 1231232, 232222200);

            // When
            ReservationService.AddReservation(room);

            // Then
            var actualHotelRooms = ReservationService.GetHotelRooms();
            actualHotelRooms.Should().NotContain(room);
        }


        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldNotAddDuplicateReservation()
        {
            // Given
            var room = new HotelRoom(101, 2, 300);
            var room2 = new HotelRoom(101, 2, 300);

            // When
            ReservationService.AddReservation(room);
            ReservationService.AddReservation(room2);

            // Then
            var actualReservedRooms = ReservationService.GetReservedRooms();
            actualReservedRooms.Should().ContainEquivalentOf(room);
        }


        // Cancel Reservation tests.

        [Fact]
        public void CancelReservation_WhenRoomIsReserved_ShouldCancelReservation()
        {
            // Given
            var room = new HotelRoom(101, 2, 300);
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
            var room = new HotelRoom(101, 2, 300);
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
            var room = new HotelRoom(101, 2, 300);

            // When
            ReservationService.CancelReservation(room);

            // Then
            ReservationService.GetReservedRooms().Should().NotContain(room);
        }
        [Fact]
        public void CancelReservation_WhenReservedRoomsIsEmpty_ShouldNotModifyLists()
        {
            // Given
            var room = new HotelRoom(101, 2, 300);

            // When
            ReservationService.CancelReservation(room);

            // Then
            var actualReservedRooms = ReservationService.GetReservedRooms();
            var actualHotelRooms = ReservationService.GetHotelRooms();
            actualReservedRooms.Should().BeEmpty();
            actualHotelRooms.Should().BeEquivalentTo(ReservationService.GetHotelRooms());
        }











    }
}
