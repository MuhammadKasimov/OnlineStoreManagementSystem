﻿using Microsoft.EntityFrameworkCore;
using OnlineStoreManagementSystem.Domain.Entitties.Products;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using System.Net.Mail;

namespace OnlineStoreManagementSystem.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(true);
        }
    }
}