using Modules.Document.Application.Dtos.ResponseDtos;

namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class TripRequestDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid PermissionId { get; set; }
        public PermisionRequestDto? Permission { get; set; }
        public string SpecialConditions { get; set; } = string.Empty;
        public DateTime ReceivedDate { get; set; }
        public DateTime EventDate { get; set; }
        public List<TripParticipantRequestDto> TripParticipants { get; set; } = [];
        public DateTime ReturnedDate { get; set; }
        public Guid AcceptedId { get; set; }
        public UserRequestDto? Accepted { get; set; }
        public decimal AmountOfFee { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
