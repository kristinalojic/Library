﻿using System;
using System.Collections.Generic;
using System.IO;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Library.Models;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MembershipFee> MembershipFees { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("LibraryDb");

        optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("book");

            entity.HasIndex(e => e.Id, "Id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.GenreId, "fk_book_genre1_idx");

            entity.Property(e => e.Author).HasMaxLength(45);
            entity.Property(e => e.AvailableCopies).HasColumnName("Available_copies");
            entity.Property(e => e.GenreId).HasColumnName("genre_Id");
            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("Is_available");
            entity.Property(e => e.Title).HasMaxLength(45);
            entity.Property(e => e.YearOfPublication).HasColumnName("Year_of_publication");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_book_genre1");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.HasIndex(e => e.Email, "Email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Jbmg, "JBMG_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Phone, "Phone_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Username, "Username_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.AccountType)
                .HasDefaultValueSql("'2'")
                .HasColumnName("Account_type");
            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.IsAcive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("Is_acive");
            entity.Property(e => e.Jbmg)
                .HasMaxLength(45)
                .HasColumnName("JBMG");
            entity.Property(e => e.Name).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.Phone).HasMaxLength(45);
            entity.Property(e => e.Surname).HasMaxLength(15);
            entity.Property(e => e.Username).HasMaxLength(45);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("genre");

            entity.HasIndex(e => e.Id, "Id_UNIQUE").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => new { e.Book, e.Member, e.IssueDate })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("loans");

            entity.HasIndex(e => e.Member, "fk_zaduzivanje_member1_idx");

            entity.Property(e => e.Book).HasColumnName("book");
            entity.Property(e => e.Member).HasColumnName("member");
            entity.Property(e => e.IssueDate).HasColumnName("Issue_date");
            entity.Property(e => e.DueDate).HasColumnName("Due_date");
            entity.Property(e => e.HasBeenExtended).HasColumnName("Has_been_extended");
            entity.Property(e => e.IsReturned).HasColumnName("Is_Returned");

            entity.HasOne(d => d.BookNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.Book)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_zaduzivanje_book1");

            entity.HasOne(d => d.MemberNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.Member)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_zaduzivanje_member1");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MembershipCardNumber).HasName("PRIMARY");

            entity.ToTable("member");

            entity.HasIndex(e => e.Email, "Email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Phone, "Phone_UNIQUE").IsUnique();

            entity.HasIndex(e => e.MembershipCardNumber, "id_UNIQUE").IsUnique();

            entity.Property(e => e.MembershipCardNumber).HasColumnName("Membership_card_number");
            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(15);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(15);
        });

        modelBuilder.Entity<MembershipFee>(entity =>
        {
            entity.HasKey(e => e.Member).HasName("PRIMARY");

            entity.ToTable("membership_fees");

            entity.Property(e => e.Member)
                .ValueGeneratedNever()
                .HasColumnName("member");

            entity.HasOne(d => d.MemberNavigation).WithOne(p => p.MembershipFee)
                .HasForeignKey<MembershipFee>(d => d.Member)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_membership_fees_member1");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Employee).HasName("PRIMARY");

            entity.ToTable("settings");

            entity.HasIndex(e => e.Employee, "employee_UNIQUE").IsUnique();

            entity.Property(e => e.Employee)
                .ValueGeneratedNever()
                .HasColumnName("employee");
            entity.Property(e => e.Color)
                .HasMaxLength(45)
                .HasDefaultValueSql("'Purple'");
            entity.Property(e => e.Language)
                .HasMaxLength(45)
                .HasDefaultValueSql("'Serbian'");
            entity.Property(e => e.Theme)
                .HasMaxLength(45)
                .HasDefaultValueSql("'Light'");

            entity.HasOne(d => d.EmployeeNavigation).WithOne(p => p.Setting)
                .HasForeignKey<Setting>(d => d.Employee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_settings_employee1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
