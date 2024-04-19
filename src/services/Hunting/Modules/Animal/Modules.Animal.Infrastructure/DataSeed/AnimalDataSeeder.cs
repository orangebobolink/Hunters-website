using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Animal.Domain.Entities;
using Modules.Animal.Infrastructure.Contexts;
using Modules.Animal.Infrastructure.Interfaces;
using Modules.Animal.Application.Features.AnimalFeatures.Events.AnimalCreate;

namespace Modules.Animal.Infrastructure.DataSeed
{
    internal class AnimalDataSeeder(IMediator mediator, 
        ApplicationDbContext context) 
        : IAnimalDataSeeder
    {
        private readonly IMediator _mediator = mediator;
        private readonly ApplicationDbContext _context = context;
        private readonly List<AnimalInfo> _animals = DataSeederGenerator.GetAnimalInfo();

        public async Task SeedAsync()
        {
            if(!await _context.Animals.AnyAsync())
            {
                _context.AddRange(_animals);

                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedMessageAsync()
        {
            var animalsName = _animals
                .Select(u => u.Name)
                .ToList();
            var animals = await _context.Animals
                .Where(u => animalsName.Contains(u.Name))
                .ToListAsync();
            var animalsMessage = new AnimalCreateRangeEvent(animals);

            await _mediator.Publish(animalsMessage);
        }
    }
}
