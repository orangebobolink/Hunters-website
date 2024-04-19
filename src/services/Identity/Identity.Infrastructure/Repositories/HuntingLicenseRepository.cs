using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

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

        public async Task<HuntingLicense?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.HuntingLicenses
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id && h.ExpiryDate > DateTime.Now);
        }

        public async Task<HuntingLicense?> GetByLicenseNumberAsync(
            string licenseNumber,
            CancellationToken cancellationToken)
        {
            return await _context.HuntingLicenses
               .AsNoTracking()
               .FirstOrDefaultAsync(h => h.LicenseNumber == licenseNumber
               && h.ExpiryDate > DateTime.Now);
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
