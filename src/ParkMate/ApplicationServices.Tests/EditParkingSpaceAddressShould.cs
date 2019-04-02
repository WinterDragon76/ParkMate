﻿using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Xunit;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationCore.ValueObjects;
using ApplicationServices.Commands;
using static ApplicationServices.Tests.TestHelper;

namespace ApplicationServices.Tests
{
    public class EditParkingSpaceAddressShould
    {
        [Fact]
        public async Task ChangeParkingSpaceToCorrectAddress()
        {
            await CreateTestParkingSpaceInDb("EditParkingSpaceAddress");

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("EditParkingSpaceAddress")))
            {
                var space = context.ParkingSpaces.FirstOrDefault();
                var repository = new ParkingSpaceRepository(context);
                var address = new Address("567 Test Road", "TestVille", "Tst", "56789", new Point(9,10));
                var command = new EditParkingSpaceAddressCommand(space.Id, address);
                var handler = new EditParkingSpaceAddressCommandHandler(repository);
               
                await handler.Handle(command);
                             
                Assert.NotNull(space.Address);
                Assert.NotEqual(GetTestAddress(), space.Address);
                Assert.Equal(address, space.Address);
                
            }
        }       
    }
}
