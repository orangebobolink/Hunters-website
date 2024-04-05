﻿using MediatR;
using Modules.Animal.Application.Dtos.ResponseDtos;

namespace Modules.Animal.Application.Features.Animal.Queries
{
    public record class GetAllAnimalsWithFullInformation() : IRequest<List<AnimalInfoResponseDto>>;
}