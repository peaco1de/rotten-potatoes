﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rotten_potatoes_api.Models;

namespace rotten_potatoes_api.Migrations
{
    [DbContext(typeof(ReviewsContext))]
    [Migration("20200919212254_CompoundKey")]
    partial class CompoundKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("rotten_potatoes_api.Models.Review", b =>
                {
                    b.Property<int>("Game")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AddDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2020, 9, 19, 17, 22, 54, 533, DateTimeKind.Local).AddTicks(4959));

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Game", "User");

                    b.ToTable("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
