using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
