// <auto-generated />
using System;
using BankAccountApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankAccountApi.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210529153528_UpdateBankAccountEntity2")]
    partial class UpdateBankAccountEntity2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("BankAccountApi.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("BankAccountApi.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Balance")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("BankAccountApi.Entities.BankAccount", b =>
                {
                    b.HasOne("BankAccountApi.Entities.AppUser", "Owner")
                        .WithMany("BankAccounts")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BankAccountApi.Entities.AppUser", b =>
                {
                    b.Navigation("BankAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
