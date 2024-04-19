using Modules.Document.Application.Dtos.ResponseDto;

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
        public Guid IssuedId { get; set; }
        public UserRequestDto? Issued { get; set; }
        public Guid ReceivedId { get; set; }
        public UserRequestDto? Received { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime EventDate { get; set; }
        public List<TripParticipantRequestDto> TripParticipants { get; set; } = [];
        public DateTime ReturnedDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public Guid AcceptedId { get; set; }
        public UserRequestDto? Accepted { get; set; }
        public decimal AmountOfFee { get; set; }
    }
}
