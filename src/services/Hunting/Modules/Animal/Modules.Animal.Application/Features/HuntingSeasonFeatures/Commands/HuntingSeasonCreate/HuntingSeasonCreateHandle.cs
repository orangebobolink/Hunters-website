﻿using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Interfaces.Repositories;

namespace Modules.Animal.Application.Features.HuntingSeasonFeatures.Commands.HuntingSeasonCreate
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

            var existingHuntingSeason = await _huntingSeasonRepository.GetHuntingSeasonByTimePeriodForAnimal(
                huntingSeasonRequest.AnimalId, 
                huntingSeasonRequest.StartDate, 
                huntingSeasonRequest.EndDate, 
                cancellationToken);

            if(existingHuntingSeason is not null)
            {
                //_logger.LogInformation($"Animal with name {huntingSeasonRequest.Name} already exists.");
                //ThrowHelper.ThrowInvalidOperationException(
                //    ErrorMessageHelper.AnimalAlreadyExists(huntingSeasonRequest.Name));
            }

            var huntingSeason = huntingSeasonRequest.Adapt<HuntingSeason>();
            huntingSeason.Id = Guid.NewGuid();
            _huntingSeasonRepository.Create(huntingSeason);

            await _huntingSeasonRepository.SaveChangesAsync(cancellationToken);

            //await _publisher.Publish(new AnimalCreateEvent(huntingSeason), cancellationToken);

            var response = huntingSeason.Adapt<HuntingSeasonResponseDto>();

            return response;
        }
    }
}
