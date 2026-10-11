using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Raff.Models;
using Microsoft.Extensions.Configuration;


namespace Raff.Data;


public partial class BooksContext : DbContext
{

    // private readonly IConfiguration _configuration;



    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options)
    {
        // _configuration = configuration;

    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<ReadingEntry> ReadingEntries { get; set; }

    public virtual DbSet<User> Users { get; set; }




    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("reading_status", new[] { "WANT_TO_READ", "READING", "FINISHED" });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("books_pkey");

            entity.ToTable("books");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CoverUrl).HasColumnName("cover_url");
            entity.Property(e => e.PublishedYear).HasColumnName("published_year");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("book_authors_author_id_fkey"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("book_authors_book_id_fkey"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId").HasName("book_authors_pkey");
                        j.ToTable("book_authors");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("author_id");
                    });
        });

        modelBuilder.Entity<ReadingEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("book_entries_pkey");

            entity.ToTable("reading_entries");

            entity.HasIndex(e => new { e.UserId, e.BookId }, "unique_user_book").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FinishedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("finished_at");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.StartedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("started_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book).WithMany(p => p.ReadingEntries)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_entries_book_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ReadingEntries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_entries_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey1");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key1").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        // modelBuilder.Entity<test>(entity =>
        //        {
        //            entity.HasKey(e => e.Id).HasName("users_pkey1");

        //            entity.ToTable("users");

        //            entity.HasIndex(e => e.Email, "users_email_key1").IsUnique();

        //            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

        //            entity.Property(e => e.Id)
        //                .UseIdentityAlwaysColumn()
        //                .HasColumnName("id");
        //            entity.Property(e => e.CreatedAt)
        //                .HasDefaultValueSql("now()")
        //                .HasColumnType("timestamp without time zone")
        //                .HasColumnName("created_at");
        //            entity.Property(e => e.Email).HasColumnName("email");
        //            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
        //            entity.Property(e => e.Username).HasColumnName("username");
        //        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
