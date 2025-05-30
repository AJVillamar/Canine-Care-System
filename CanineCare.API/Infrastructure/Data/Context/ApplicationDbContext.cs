﻿using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Entities.Pet;
using Infrastructure.Data.Entities.People;
using Infrastructure.Data.Entities.Identity;

namespace Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<PersonEntity> Persons { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<AdminEntity> Admins { get; set; }

        public DbSet<OwnerEntity> Owners { get; set; }

        public DbSet<ProfessionalEntity> Professionals { get; set; }

        public DbSet<BreedEntity> Breeds { get; set; }

        public DbSet<PetEntity> Pets { get; set; }

        public DbSet<PetExtraInfoEntity> PetExtras { get; set; }

        public DbSet<ServiceEntity> Services { get; set; }

        public DbSet<ServiceDetailEntity> ServiceDetails { get; set; }

        public DbSet<AppointmentEntity> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppointmentEntity>()
                .HasOne(a => a.Professional)
                .WithMany()
                .HasForeignKey(a => a.ProfessionalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppointmentEntity>()
                .HasOne(a => a.Pet)
                .WithMany()
                .HasForeignKey(a => a.PetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppointmentEntity>()
                .HasOne(a => a.Service)
                .WithMany()
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
