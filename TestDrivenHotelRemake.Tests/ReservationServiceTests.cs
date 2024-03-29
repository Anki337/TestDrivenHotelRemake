﻿using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TestDrivenHotelRemake.BLL;
using Xunit.Sdk;

namespace TestDrivenHotelRemake.Tests
{
    public class ReservationServiceTests
    {


        [Fact]
        public void GetHotelRooms_ShouldReturnListOfHotelRooms()
        {
            // Given
           
            var room1 = new HotelRoom(101, 2, 300);
            var room2 = new HotelRoom(102, 3, 400);
            var room3 = new HotelRoom(103, 4, 500);

            // When
            var actualHotelRooms = ReservationService.InitializeHotelRooms();

            // Then
            actualHotelRooms.Should().BeEquivalentTo(new List<HotelRoom> { room1, room2, room3 });
        }
        [Fact]
        public void GetReservedRooms_ShouldReturnEmptyListAtDeploy()
        {
            // Given
            // Reserved rooms are empty at deploy.
            // When
            var actualReservedRooms = ReservationService.InitializeReservedRooms();

            // Then
            actualReservedRooms.Should().BeEmpty();
        }   

        [Fact] 
        public void GetHotelRooms_ShouldReturnEmptyListAfterAllReservations()
        {
            // Given
            var room1 = new HotelRoom(101, 2, 300);
            var room2 = new HotelRoom(102, 3, 400);
            var room3 = new HotelRoom(103, 4, 500);
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);

            // When
            ReservationService.AddReservation(room1, startDate, endDate);
            ReservationService.AddReservation(room2, startDate, endDate);
            ReservationService.AddReservation(room3, startDate, endDate);

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
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);

            // When
            ReservationService.AddReservation(room, startDate, endDate);

            // Then
            var actualReservation = ReservationService.GetReservedRooms().FirstOrDefault();
            actualReservation.Should().NotBeNull();
            actualReservation.StartDate.Should().Be(startDate);
            actualReservation.EndDate.Should().Be(endDate);
        }

        [Fact]
        public void AddReservation_WhenRoomIsAvailable_ShouldRemoveRoomFromHotelRooms()
        {
            // Given
            var room = new HotelRoom(101, 2, 300);
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);

            // When
            ReservationService.AddReservation(room, startDate, endDate);

            // Then
            var actualHotelRooms = ReservationService.GetHotelRooms();
            actualHotelRooms.Should().NotContain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsReserved_ShouldNotModifyLists()
        {
            // Given
            var room = new HotelRoom(101, 2, 300);
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);

            // When
            ReservationService.AddReservation(room, startDate, endDate);

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
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);

            //When
            ReservationService.AddReservation(room, startDate, endDate);
            // Then
            ReservationService.GetReservedRooms().Should().NotContain(room);
        }

        [Fact]
        public void AddReservation_WhenRoomIsNotAvailable_ShouldNotRemoveRoomFromHotelRooms()
        {
            // Given
            var room = new HotelRoom(9339, 1231232, 232222200);
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);

            // When
            ReservationService.AddReservation(room, startDate, endDate);

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
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);

            // When
            ReservationService.AddReservation(room, startDate, endDate);
            ReservationService.AddReservation(room2, startDate, endDate);

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
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);
            ReservationService.AddReservation(room, startDate, endDate);

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
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 5);
            ReservationService.AddReservation(room, startDate, endDate);

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
