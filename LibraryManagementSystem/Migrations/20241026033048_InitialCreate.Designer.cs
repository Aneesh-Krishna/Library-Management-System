﻿// <auto-generated />
using System;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241026033048_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagementSystem.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Author");

                    b.Property<int>("AvailableCopies")
                        .HasColumnType("int")
                        .HasColumnName("Copies_Available");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("BookId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("LibraryManagementSystem.Models.BorrowingRecord", b =>
                {
                    b.Property<int>("BorrowingRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BorrowingRecordId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BookId");

                    b.Property<DateTime>("BorrowedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Borrowed_Date");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Due_On");

                    b.Property<int>("LibraryMemberId")
                        .HasColumnType("int")
                        .HasColumnName("Borrower");

                    b.Property<DateTime?>("ReturnedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("Returned_On");

                    b.HasKey("BorrowingRecordId");

                    b.HasIndex("BookId");

                    b.HasIndex("LibraryMemberId");

                    b.ToTable("BorrowingRecords");
                });

            modelBuilder.Entity("LibraryManagementSystem.Models.LibraryMember", b =>
                {
                    b.Property<int>("LibraryMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibraryMemberId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Full_Name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Contact");

                    b.HasKey("LibraryMemberId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Library_Members", (string)null);
                });

            modelBuilder.Entity("LibraryManagementSystem.Models.BorrowingRecord", b =>
                {
                    b.HasOne("LibraryManagementSystem.Models.Book", "Book")
                        .WithMany("BorrowingRecords")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LibraryManagementSystem.Models.LibraryMember", "LibraryMember")
                        .WithMany("BorrowingRecords")
                        .HasForeignKey("LibraryMemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("LibraryMember");
                });

            modelBuilder.Entity("LibraryManagementSystem.Models.Book", b =>
                {
                    b.Navigation("BorrowingRecords");
                });

            modelBuilder.Entity("LibraryManagementSystem.Models.LibraryMember", b =>
                {
                    b.Navigation("BorrowingRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
