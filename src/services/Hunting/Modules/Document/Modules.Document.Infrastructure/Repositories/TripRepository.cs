﻿using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;
using System.Linq;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class TripRepository(
        DocumentDbContext context)
        : BaseRepository<Trip>(context),
        ITripRepository
    {
        public Task<List<Trip>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            return _context.Trips
                .AsSplitQuery()
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Issued)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Received)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Animal)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Land)
                .Include(t => t.TripParticipants)
                    .ThenInclude(tp => tp.Participant)
                .ToListAsync(cancellationToken);
        }

        public Task<Trip?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Trips
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Issued)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Received)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Land)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Animal)
                .Include(t => t.TripParticipants)
                    .ThenInclude(tp => tp.Participant)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public Task<List<Trip>> GetByParticipantIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Trips
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Issued)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Received)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Land)
                .Include(t => t.Permission)
                    .ThenInclude(p => p.Animal)
                .Include(t => t.TripParticipants)
                    .ThenInclude(tp => tp.Participant)
                .Where(
                    e => e.TripParticipants.Any(
                        p => p.ParticipantId == id))
                .ToListAsync(cancellationToken);
        }
    }
}
