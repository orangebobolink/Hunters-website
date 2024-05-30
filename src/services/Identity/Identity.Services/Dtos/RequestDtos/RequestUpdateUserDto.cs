﻿using Identity.Domain.Enums;

namespace Identity.Services.Dtos.RequestDtos
{
    public record class RequestUpdateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Sex { get; set; }
        public List<string> RoleNames { get; set; } = [];
    }
}
