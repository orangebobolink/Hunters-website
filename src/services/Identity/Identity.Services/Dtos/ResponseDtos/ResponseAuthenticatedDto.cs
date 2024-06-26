﻿namespace Identity.Services.Dtos.ResponseDtos
{
    public class ResponseAuthenticatedDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
        public Guid? HuntingLicenseId { get; set; }
        public bool? IsPaid { get; set; } = false;
        public string? AccessToken { get; set; }
        public string AvatarUrl { get; set; } = string.Empty;
    }
}