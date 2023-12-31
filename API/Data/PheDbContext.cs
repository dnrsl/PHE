﻿using API.DTOs.Roles;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PheDbContext : DbContext
    {
        public PheDbContext(DbContextOptions<PheDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
               new DefaultRoleDto
               {
                   Guid = Guid.Parse("5eeda544-ee8f-495d-9366-6c04e0904a5c"),
                   Name = "Manager"
               }, new DefaultRoleDto
               {
                   Guid = Guid.Parse("4016bbf3-5514-4478-97f8-85a3baef09c2"),
                   Name = "Admin"
               }, new DefaultRoleDto
               {
                   Guid = Guid.Parse("24706f51-2651-4cd2-9ca0-c8e510969b7d"),
                   Name = "Vendor"
               });

            modelBuilder.Entity<User>().HasIndex(user => new
            {
                user.Email,
                user.Telephone
            }).IsUnique();

            // (1:N) One User to Many UserProject
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserProjects)
                .WithOne(uProject => uProject.User)
                .HasForeignKey(uProject => uProject.UserGuid)
                .OnDelete(DeleteBehavior.Restrict);

            // (1:1) One User to One Account
            modelBuilder.Entity<User>()
                .HasOne(user => user.Account)
                .WithOne(account => account.User)
                .HasForeignKey<Account>(account => account.Guid)
                .OnDelete(DeleteBehavior.Cascade);

            // (1:N) One Account to Many AccountRole
            modelBuilder.Entity<Account>()
                .HasMany(account => account.AccountRoles)
                .WithOne(accountRole => accountRole.Account)
                .HasForeignKey(accountRole => accountRole.AccountGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // (1:1) One Account to One Characteristic
            modelBuilder.Entity<Account>()
                .HasOne(account => account.Characteristic)
                .WithOne(charac => charac.Account)
                .HasForeignKey<Characteristic>(charac => charac.Guid)
                .OnDelete(DeleteBehavior.Cascade);

            // (1:N) One Role to Many AccountRole
            modelBuilder.Entity<Role>()
                .HasMany(role => role.AccountRoles)
                .WithOne(accRole => accRole.Role)
                .HasForeignKey(accRole => accRole.RoleGuid)
                .OnDelete(DeleteBehavior.Cascade);

            // (1:N) One Project to Many UserProject
            modelBuilder.Entity<Project>()
                .HasMany(p => p.UserProjects)
                .WithOne(uProject => uProject.Project)
                .HasForeignKey(uProject => uProject.ProjectGuid)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
