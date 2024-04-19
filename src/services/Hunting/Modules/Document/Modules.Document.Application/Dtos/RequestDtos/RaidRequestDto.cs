﻿namespace Modules.Document.Application.Dtos.RequestDtos
{
    public class RaidRequestDto
    {
        public DateTime ExitTime { get; set; }
        public DateTime ReturnedTime { get; set; }
        public List<UserRequestDto> Participants { get; set; } = [];
        public string Note { get; set; } = string.Empty;
    }
}
