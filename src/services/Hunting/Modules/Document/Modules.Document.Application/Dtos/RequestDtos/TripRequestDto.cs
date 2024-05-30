using Modules.Document.Domain.Enums;

namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class TripRequestDto
    { 
        public string Number { get; set; } = string.Empty;
        public Guid PermissionId { get; set; }
        public PermisionRequestDto? Permission { get; set; }
        public string SpecialConditions { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public ICollection<TripParticipantRequestDto> TripParticipants { get; set; } = [];
        public decimal Price { get; set; }
        public DateTime ReturnedDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
