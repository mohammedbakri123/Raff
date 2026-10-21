using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Raff.Models;

[Table("reading_entries")]
public partial class ReadingEntry
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }


    // Progress starts at 0 and can reach 100.
    [Precision(5, 2)]
    [Range(typeof(decimal), "0", "100")]
    public decimal Percentage { get; set; }

    // A rating is optional and, when supplied, must be 1–10.
    [Precision(4, 2)]
    [Range(typeof(decimal), "1", "10")]
    public decimal? Rating { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    // Example limit; choose one that fits your application.
    [MaxLength(2000)]
    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ReadingStatus? Status { get; set; }
        = ReadingStatus.WANT_TO_READ;

    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;
}