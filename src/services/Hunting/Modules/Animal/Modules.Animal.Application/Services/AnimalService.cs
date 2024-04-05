using Mapster;
using Microsoft.Extensions.Logging;
using Modules.Animal.Application.Dtos.RequestDtos;
using Modules.Animal.Application.Dtos.ResponseDtos;
using Modules.Animal.Application.Interfaces;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Domain.Interfaces.Repositories;

namespace Modules.Animal.Application.Services
{
    internal class AnimalService(IAnimalRepository animalRepository, 
        ILogger<AnimalService> logger) 
        : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository = animalRepository;
        private readonly ILogger<AnimalService> _logger = logger;

        public async Task<AnimalInfoResponseDto> CreateAsync(AnimalInfoRequestDto requestAnimal, 
                                                            CancellationToken cancellationToken)
        {
            var existingAnimal = await _animalRepository.GetByNameAsync(requestAnimal.Name, cancellationToken);

            if (existingAnimal is not null)
            {
                _logger.LogInformation($"Animal with name {requestAnimal.Name} already exists.");
                throw new InvalidOperationException("An animal with this name already exists.");
            }

            var id = Guid.NewGuid();
            var animal = requestAnimal.Adapt<AnimalInfo>();
            animal.Id = id;
            _animalRepository.Create(animal);

            await _animalRepository.SaveChangesAsync(cancellationToken);

            var response = animal.Adapt<AnimalInfoResponseDto>();
            // TODO: Handler

            return response;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingAnimal = await _animalRepository.GetByIdAsync(id, cancellationToken);

            if(existingAnimal is null)
            {
                _logger.LogWarning($"Animal with id {id} not found in the database.");
                throw new KeyNotFoundException("An animal with this Id not found in the database.");
            }

            var animal = existingAnimal.Adapt<AnimalInfo>();
            _animalRepository.Delete(animal);

            await _animalRepository.SaveChangesAsync(cancellationToken);

            // TODO: Handler
        }

        public async Task<List<AnimalInfoResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var animals = await _animalRepository.GetAllAsync(cancellationToken);

            if(animals is null)
            {
                _logger.LogWarning("No animals found in the database.");
                throw new InvalidOperationException("No animals found in the database.");
            }

            var response = animals.Adapt< List<AnimalInfoResponseDto>>();

            return response;
        }

        public async Task<List<AnimalInfoResponseDto>> GetAllWithFullInformationAsync(CancellationToken cancellationToken)
        {
            var animals = await _animalRepository.GetAllWithFullInformationAsync(cancellationToken);

            if(animals is null)
            {
                _logger.LogWarning("No animals found in the database.");
                throw new InvalidOperationException("No animals found in the database.");
            }

            var response = animals.Adapt<List<AnimalInfoResponseDto>>();

            return response;
        }

        public async Task<AnimalInfoResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var animal = await _animalRepository.GetByIdAsync(id, cancellationToken);

            if(animal is null)
            {
                _logger.LogWarning($"An animal with id {id} not found in the database.");
                throw new KeyNotFoundException("An animal with this Id not found in the database.");
            }

            var response = animal.Adapt<AnimalInfoResponseDto>();

            return response;
        }

        public async Task<AnimalInfoResponseDto> UpdateAsync(Guid id, 
                                                            AnimalInfoRequestDto requestAnimal, 
                                                            CancellationToken cancellationToken)
        {
            var existingAnimal = await _animalRepository.GetByIdAsync(id, cancellationToken);

            if(existingAnimal is null)
            {
                _logger.LogWarning($"An animal with id {id} not found in the database.");
                throw new KeyNotFoundException("An animal with this Id not found in the database.");
            }

            var animal = requestAnimal.Adapt(existingAnimal);
            _animalRepository.Update(animal);

            await _animalRepository.SaveChangesAsync(cancellationToken);

            var updatedAnimal = await _animalRepository.GetByNameAsync(requestAnimal.Name, cancellationToken);

            // TODO: Handler

            var response = updatedAnimal.Adapt<AnimalInfoResponseDto>();

            return response;
        }
    }
}
