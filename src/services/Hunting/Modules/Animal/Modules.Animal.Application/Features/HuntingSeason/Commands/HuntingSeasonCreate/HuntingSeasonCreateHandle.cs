using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Application.Features.Animal.Events.AnimalCreate;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Helpers;
using Modules.Animal.Domain.Interfaces.Repositories;
using Shared.Helpers;

namespace Modules.Animal.Application.Features.HuntingSeason.Commands.HuntingSeasonCreate
{
    public class HuntingSeasonCreateHandle(
        IHuntingSeasonRepository huntingSeasonRepository,
        ILogger<HuntingSeasonCreateHandle> logger,
        IPublisher publisher)
        : IRequestHandler<HuntingSeasonCreateCommand, HuntingSeasonResponseDto>
    {
        private readonly IHuntingSeasonRepository _huntingSeasonRepository = huntingSeasonRepository;
        private readonly ILogger<HuntingSeasonCreateHandle> _logger = logger;
        private readonly IPublisher _publisher = publisher;

        public async Task<HuntingSeasonResponseDto> Handle(HuntingSeasonCreateCommand request, CancellationToken cancellationToken)
        {
            var huntingSeasonRequest = request.HuntingSeasonRequest;

            var existingAnimal = await _huntingSeasonRepository.GetHuntingSeasonsByTimePeriod(huntingSeasonRequest.StartDate, huntingSeasonRequest.EndDate, cancellationToken);

            if(existingAnimal is not null)
            {
                _logger.LogInformation($"Animal with name {animalRequestDto.Name} already exists.");
                ThrowHelper.ThrowInvalidOperationException(
                    ErrorMessageHelper.AnimalAlreadyExists(animalRequestDto.Name));
            }

            var id = Guid.NewGuid();
            var animal = animalRequestDto.Adapt<AnimalInfo>();
            animal.Id = id;
            _animalRepository.Create(animal);

            await _animalRepository.SaveChangesAsync(cancellationToken);

            await _publisher.Publish(new AnimalCreateEvent(animal), cancellationToken);

            var response = animal.Adapt<AnimalInfoResponseDto>();

            return response;
        }
    }
}
