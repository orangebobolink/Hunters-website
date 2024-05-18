using Modules.Document.Domain.Entities;
using Modules.Document.Domain.Interfaces;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Repositories
{
    internal class HuntingLicenseRepository(DocumentDbContext context)
                : BaseRepository<HuntingLicense>(context), IHuntingLicenseRepository
    {
    }
}
