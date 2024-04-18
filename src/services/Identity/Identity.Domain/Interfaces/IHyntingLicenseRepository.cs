﻿using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces
{
    public interface IHyntingLicenseRepository
        : IRepository<HuntingLicense>
    {
        public Task<HuntingLicense?> GetByLicenseNumberAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
