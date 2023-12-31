﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace pocketpay.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20231004020957_transference")]
    partial class transference
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("AccountModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("ClientModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("DepositModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TransactionId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Deposit");
                });

            modelBuilder.Entity("SellerModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CNPJ")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("pocketpay.Models.TransactionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("pocketpay.Models.TransferenceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Transferences");
                });

            modelBuilder.Entity("pocketpay.Models.WalletModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Wallet");
                });

            modelBuilder.Entity("pocketpay.Models.WithdrawModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TransactionId")
                        .HasColumnType("TEXT");

                    b.Property<double>("value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Withdraw");
                });

            modelBuilder.Entity("ClientModel", b =>
                {
                    b.HasOne("AccountModel", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DepositModel", b =>
                {
                    b.HasOne("AccountModel", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("pocketpay.Models.TransactionModel", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId");

                    b.Navigation("Account");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("SellerModel", b =>
                {
                    b.HasOne("AccountModel", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("pocketpay.Models.TransactionModel", b =>
                {
                    b.HasOne("AccountModel", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("pocketpay.Models.TransferenceModel", b =>
                {
                    b.HasOne("AccountModel", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountModel", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pocketpay.Models.TransactionModel", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("pocketpay.Models.WalletModel", b =>
                {
                    b.HasOne("AccountModel", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("pocketpay.Models.WithdrawModel", b =>
                {
                    b.HasOne("AccountModel", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("pocketpay.Models.TransactionModel", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId");

                    b.Navigation("Account");

                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
