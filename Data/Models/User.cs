using System;
using System.Collections.Generic;
using Raff.Domain.Enums;

namespace Raff.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.Reader;

    public string Email { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<ReadingEntry> ReadingEntries { get; set; } = new List<ReadingEntry>();
}
