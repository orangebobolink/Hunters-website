﻿namespace Identity.Services.Dtos.RequestDtos
{
    public record class RequestUserDto
    {
        public string Email { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
