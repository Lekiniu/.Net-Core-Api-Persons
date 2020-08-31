﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persons.Data.Entities;

namespace Persons.Data.Migrations
{
    [DbContext(typeof(PersonsDbContext))]
    [Migration("20200830093939_4thMigration")]
    partial class _4thMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Persons.Data.Entities.Addresses", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasMaxLength(20);

                    b.Property<string>("Country")
                        .HasMaxLength(20);

                    b.Property<int>("PersonId");

                    b.Property<string>("Street")
                        .HasMaxLength(50);

                    b.HasKey("AddressId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Persons.Data.Entities.Files", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("PersonId");

                    b.Property<string>("Url");

                    b.HasKey("FileId");

                    b.HasIndex("PersonId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Persons.Data.Entities.Persons", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NameEng")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(12);

                    b.Property<string>("PrivateNumber")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("SurnameEng")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Persons.Data.Entities.RelatedPersons", b =>
                {
                    b.Property<int>("RelateId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PersonId");

                    b.Property<int?>("RelatedPersonId");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("RelateId");

                    b.HasIndex("PersonId");

                    b.HasIndex("RelatedPersonId");

                    b.ToTable("RelatedPersons");
                });

            modelBuilder.Entity("Persons.Data.Entities.Addresses", b =>
                {
                    b.HasOne("Persons.Data.Entities.Persons", "Person")
                        .WithOne("Address")
                        .HasForeignKey("Persons.Data.Entities.Addresses", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persons.Data.Entities.Files", b =>
                {
                    b.HasOne("Persons.Data.Entities.Persons", "Person")
                        .WithMany("Files")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persons.Data.Entities.RelatedPersons", b =>
                {
                    b.HasOne("Persons.Data.Entities.Persons", "Person")
                        .WithMany("PersonsGroup")
                        .HasForeignKey("PersonId");

                    b.HasOne("Persons.Data.Entities.Persons", "RelatedPerson")
                        .WithMany("RelatedPersons")
                        .HasForeignKey("RelatedPersonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
