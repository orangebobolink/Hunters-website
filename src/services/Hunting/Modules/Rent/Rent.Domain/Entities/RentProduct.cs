using Rent.Domain.Enums;
using Rent.Domain.Interfaces;

namespace Rent.Domain.Entities
{
    public class RentProduct
        : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public RentStatus Status { get; set; }
    }
}
