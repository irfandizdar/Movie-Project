﻿using System;
using System.Collections.Generic;

namespace Movie_Backend.Models;

public partial class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}

