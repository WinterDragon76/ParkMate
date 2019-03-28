﻿using System;
using ParkMate.ApplicationCore.ValueObjects;

namespace ParkMate.ApplicationCore.Entities
{
    public class ParkingSpace : BaseEntity
    {
        private ParkingSpace()
        {
        }
        public ParkingSpace(
            string ownerId,
            ParkingSpaceDescription description,
            Address address,
            SpaceAvailability availability,
            BookingRate bookingRate)
        {
            OwnerId = string.IsNullOrWhiteSpace(ownerId) ? ownerId :
                throw new ArgumentNullException(nameof(ownerId));
            Description = description ?? 
                throw new ArgumentNullException(nameof(description));
            Address = address ?? 
                throw new ArgumentNullException(nameof(address));
            Availability = availability ?? 
                throw new ArgumentNullException(nameof(availability)); 
            BookingRate = bookingRate ?? 
                throw new ArgumentNullException(nameof(bookingRate));
        }

        public string OwnerId { get; private set; }
        public ParkingSpaceDescription Description { get; private set; }
        public Address Address { get; private set; }
        public SpaceAvailability Availability { get; private set; }
        public BookingRate BookingRate { get; private set; }
    }
}