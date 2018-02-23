using System;
using System.Collections.Generic;
using System.Text;
using Domain.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Domain.Ef
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options)
            : base(options)
        {
        }

        public DbSet<Routes> Routes { get; set; }
        public DbSet<Trips> Trips { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Passangers> Passangers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trips>(entity =>
            {
                entity.HasOne(p => p.Driver)
                    .WithMany(p => p.DriverTrips)
                    .HasForeignKey(p => p.DriverId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Passangers>(entity =>
            {
                // составной первичный ключ
                entity.HasKey(e => new { e.TripId, e.PassangerId });

                entity.HasOne(p => p.Trip)
                    .WithMany(p => p.Passangers)
                    .HasForeignKey(p => p.PassangerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Passanger)
                    .WithMany(p => p.Passangers)
                    .HasForeignKey(p => p.PassangerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Routes>(entity =>
            {
                entity.HasOne(p => p.StartLocation)
                    .WithMany(p => p.StartRoutes)
                    .HasForeignKey(p => p.StartLocationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.DestinationLocation)
                    .WithMany(p => p.DestRoutes)
                    .HasForeignKey(p => p.DestinationLocationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}