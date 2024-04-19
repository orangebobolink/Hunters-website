using Modules.Document.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Document.Application.Dtos.ResponseDto
{
    public class PermisionResponseDto
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid AnimalId { get; set; }
        public AnimalResponseDto? Animal { get; set; }
        public Guid IssuedId { get; set; }
        public UserResponseDto? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public UserResponseDto? Received { get; set; }
        public List<CouponResponseDto> Coupons { get; set; } = [];
    }
}
