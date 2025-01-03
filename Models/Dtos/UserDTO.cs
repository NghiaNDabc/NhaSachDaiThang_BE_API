﻿namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? IdNumber { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? RoleName { get; set; }
        public string? Image { get; set; }
        public int? RoleId { get; set; }
        public string?PassWord { get; set; }    
    }
}
