﻿using MediatR;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.HuntingSeasonFeatures.Commands.HuntingSeasonCreate
{
    public record HuntingSeasonCreateCommand(
        HuntingSeasonRequestDto HuntingSeasonRequest) 
        : IRequest<HuntingSeasonResponseDto>;
}
