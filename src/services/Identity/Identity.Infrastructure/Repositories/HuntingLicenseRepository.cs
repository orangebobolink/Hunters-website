using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Infrastructure.Repositories
{
    internal class HuntingLicenseRepository(
        ApplicationDbContext context)
        : IHyntingLicenseRepository
    {
        private readonly ApplicationDbContext _context = context;

        public void Create(HuntingLicense entity)
        {
            _context.HuntingLicenses.Add(entity);
        }

        public void Delete(HuntingLicense entity)
        {
            _context.HuntingLicenses.Remove(entity);
        }

        public async Task<List<HuntingLicense>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.HuntingLicenses
                .AsNoTracking()
                .Where(h => h.ExpiryDate < DateTime.Now)
                .ToListAsync();
        }

        public async Task<IEnumerable<HuntingLicense>> GetAllByPredicate(Expression<Func<HuntingLicense, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.HuntingLicenses
               .AsNoTracking()
               .Where(predicate)
               .ToListAsync(cancellationToken);
        }

        public async Task<HuntingLicense?> GetByPredicate(Expression<Func<HuntingLicense, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.HuntingLicenses
              .AsNoTracking()
              .Where(h => h.ExpiryDate > DateTime.Now)
              .FirstOrDefaultAsync(predicate);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(HuntingLicense entity)
        {
            _context.HuntingLicenses.Update(entity);
        }
    }
}
