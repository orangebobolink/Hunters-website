﻿namespace Identity.Services.Dtos.ResponseDtos
{
    public class ResponseCreateUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
