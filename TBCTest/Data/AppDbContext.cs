using Microsoft.EntityFrameworkCore;
using System;
using TBCTest.Models;

namespace TBCTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PersonRelation> PersonRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonRelation>()
                .HasOne(r => r.Person)
                .WithMany(p => p.RelatedPeople)
                .HasForeignKey(r => r.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonRelation>()
                .HasOne(r => r.RelatedPerson)
                .WithMany(p => p.RelatedToPeople)
                .HasForeignKey(r => r.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}