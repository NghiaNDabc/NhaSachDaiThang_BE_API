﻿using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Customers { get; set; } = new List<User>();

}
