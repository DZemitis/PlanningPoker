﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScrumPoker.DataAccess.Models.EFContext;

#nullable disable

namespace ScrumPoker.DataAccess.Models.Migrations
{
    [DbContext(typeof(ScrumPokerContext))]
    partial class ScrumPokerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.GameRoomDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CurrentRoundId")
                        .HasColumnType("int");

                    b.Property<int>("MasterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentRoundId")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.HasIndex("MasterId");

                    b.ToTable("GameRooms", (string)null);
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.GameRoomPlayer", b =>
                {
                    b.Property<int>("GameRoomId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("GameRoomId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("GameRoomId", "PlayerId");

                    b.ToTable("GameRoomsPlayers", (string)null);
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.PlayerDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PLayersVoteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PLayersVoteId");

                    b.ToTable("Players", (string)null);
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.RoundDto", b =>
                {
                    b.Property<int>("RoundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoundId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameRoomId")
                        .HasColumnType("int");

                    b.Property<int>("RoundState")
                        .HasColumnType("int");

                    b.HasKey("RoundId");

                    b.HasIndex("GameRoomId");

                    b.HasIndex("RoundId", "GameRoomId");

                    b.ToTable("Rounds", (string)null);
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.VoteRegistrationDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("RoundId")
                        .HasColumnType("int");

                    b.Property<int>("Vote")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RoundId");

                    b.ToTable("Votes", (string)null);
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.GameRoomDto", b =>
                {
                    b.HasOne("ScrumPoker.DataAccess.Models.Models.RoundDto", "CurrentRound")
                        .WithOne()
                        .HasForeignKey("ScrumPoker.DataAccess.Models.Models.GameRoomDto", "CurrentRoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScrumPoker.DataAccess.Models.Models.PlayerDto", "Master")
                        .WithMany()
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentRound");

                    b.Navigation("Master");
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.GameRoomPlayer", b =>
                {
                    b.HasOne("ScrumPoker.DataAccess.Models.Models.GameRoomDto", "GameRoom")
                        .WithMany("GameRoomPlayers")
                        .HasForeignKey("GameRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScrumPoker.DataAccess.Models.Models.PlayerDto", "Player")
                        .WithMany("PlayerGameRooms")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameRoom");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.PlayerDto", b =>
                {
                    b.HasOne("ScrumPoker.DataAccess.Models.Models.VoteRegistrationDto", "PLayersVote")
                        .WithMany()
                        .HasForeignKey("PLayersVoteId");

                    b.Navigation("PLayersVote");
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.RoundDto", b =>
                {
                    b.HasOne("ScrumPoker.DataAccess.Models.Models.GameRoomDto", "GameRoom")
                        .WithMany("Rounds")
                        .HasForeignKey("GameRoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GameRoom");
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.VoteRegistrationDto", b =>
                {
                    b.HasOne("ScrumPoker.DataAccess.Models.Models.PlayerDto", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScrumPoker.DataAccess.Models.Models.RoundDto", "Round")
                        .WithMany("Votes")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Round");
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.GameRoomDto", b =>
                {
                    b.Navigation("GameRoomPlayers");

                    b.Navigation("Rounds");
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.PlayerDto", b =>
                {
                    b.Navigation("PlayerGameRooms");
                });

            modelBuilder.Entity("ScrumPoker.DataAccess.Models.Models.RoundDto", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
