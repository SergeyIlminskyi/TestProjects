﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SWAG.Data;

namespace SWAG.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SWAG.Data.EventEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Code");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<int>("Level");

                    b.Property<string>("Message")
                        .HasMaxLength(4000);

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("SWAG.Data.OperationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<double>("Result");

                    b.Property<string>("Serialized")
                        .IsRequired();

                    b.Property<byte>("Type");

                    b.HasKey("Id");

                    b.ToTable("Operation");

                    b.HasData(
                        new { Id = new Guid("68f4e9c2-3592-4086-a952-89b76f4b432e"), CreatedOn = new DateTime(2018, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), Result = 8.0, Serialized = "1;7", Type = (byte)1 }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}