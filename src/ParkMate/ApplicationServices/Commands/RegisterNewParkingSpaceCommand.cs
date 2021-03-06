using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Events;

namespace ParkMate.ApplicationServices.Commands
{
    public class RegisterNewParkingSpaceCommand : IRequest<CommandResult>
    {
        public RegisterNewParkingSpaceCommand(
            string ownerId,
            ParkingSpaceDescription description,
            Address address,
            SpaceAvailability availability,
            BookingRate bookingRate)
        {
            OwnerId = ownerId;
            Description = description;
            Address = address;
            Availability = availability;
            BookingRate = bookingRate;
        }

        public string OwnerId { get; }
        public ParkingSpaceDescription Description { get; }
        public Address Address { get; }
        public SpaceAvailability Availability { get; }
        public BookingRate BookingRate { get; }
    }
    
    public class RegisterNewParkingSpaceCommandHandler 
        : IRequestHandler<RegisterNewParkingSpaceCommand, CommandResult>
    {
        private IRepository<ParkingSpace> _repository;
        private IMediator _mediator;

        public RegisterNewParkingSpaceCommandHandler(
            IRepository<ParkingSpace> repository, 
            IMediator mediator)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<CommandResult> Handle(
            RegisterNewParkingSpaceCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = new ParkingSpace(command.OwnerId, command.Description,
                command.Address, command.Availability, command.BookingRate);

            await _repository.AddAsync(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));
            
            return new CommandResult(true, "Parking Space was successfully registered");
        }
    }
}