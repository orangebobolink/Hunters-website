﻿using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class RaidRepository(
        DocumentDbContext context)
        : BaseRepository<Raid>(context), IRaidRepository
    {
        public Task<List<Raid>> GetAllIncludeAsync(CancellationToken cancellationToken)
        {
            return _context.Raids
                .Include(r => r.Participants)
                .Include(r => r.Land)
                .ToListAsync(cancellationToken);
        }

        public Task<Raid?> GetByIdIncludeAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Raids
                .Include(r => r.Participants)
                .Include(r => r.Land)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public Task<List<Raid>> GetRaidsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Raids
                .Include(r => r.Participants)
                .Include(r => r.Land)
                .Where(r => r.Participants
                            .Any(u => u.Id == id))
                .ToListAsync(cancellationToken);
        }
    }
}
