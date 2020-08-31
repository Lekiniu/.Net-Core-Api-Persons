using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persons.Data.Entities
{
   public  class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options)
             : base(options)
        {
        }

        public DbSet<Persons> Persons { get; set; }
        public DbSet<RelatedPersons> RelatedPersons { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<PersonTypes> PersonTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<RelatedPersons>()
            //    .HasKey(u => new { u.RelatedPersonId, u.PersonId });

            //modelBuilder.Entity<Persons>()
            //    .HasMany(u => u.RelatedPersons)
            //    .WithOne(f => f.Person)
            //    .HasForeignKey(f => f.PersonId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Persons>()
            //    .HasMany(u => u.Persons)
            //    .WithOne(f => f.RelatedPerson)
            //    .HasForeignKey(f => f.RelatedPersonId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<RelatedPersons>(b =>
            //{
            //    b.HasKey(e => new { e.RelatedPersonId, e.PersonId });
            //    b.HasOne(e => e.Person).WithMany(e => e.RelatedPersons);
            //    b.HasOne(e => e.Person).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
            //});

            modelBuilder.Entity<RelatedPersons>()
             .HasOne(pt => pt.RelatedPerson)
             .WithMany(p => p.RelatedPersons) 
             .HasForeignKey(pt => pt.RelatedPersonId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelatedPersons>()
                .HasOne(pt => pt.Person)
                .WithMany(t => t.PersonsGroup)
                .HasForeignKey(pt => pt.PersonId);
        }
    }
}
