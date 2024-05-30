using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.DataSeedMessages;

using RentInterface = Rent.Domain.Interfaces;
using DocumentInterface = Modules.Document.Domain.Interfaces;
using Modules.Document.Domain.Entities;

namespace Hunting.Bus.Consumers
{
    public class UserDataSeedConsumer(
        DocumentInterface.IUserRepository userRepository,
        RentInterface.IUserRepository rentUserRepository,
        ILogger<UserDataSeedConsumer> logger)
        : IConsumer<UserDataSeedMessage>
    {
        private readonly DocumentInterface.IUserRepository _userRepository = userRepository;
        private readonly RentInterface.IUserRepository _userRentRepository = rentUserRepository;
        private readonly ILogger<UserDataSeedConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<UserDataSeedMessage> context)
        {
            if (!(await _userRepository.GetAllAsync(CancellationToken.None)).Any())
            {
                var users = context.Message.Users
                    .Adapt<List<User>>();

                users.ForEach(_userRepository.Create);

                await _userRepository.SaveChangesAsync(CancellationToken.None);

                _logger.LogInformation("Users was created");
            }

            if (!(await _userRentRepository.GetAllAsync(CancellationToken.None)).Any())
            {
                var users = context.Message.Users
                    .Adapt<List<Rent.Domain.Entities.User>>();

                await _userRentRepository.CreateRange(users);

                await _userRentRepository.SaveChangesAsync(CancellationToken.None);

                _logger.LogInformation("Users was created");
            }
        }
    }
}
