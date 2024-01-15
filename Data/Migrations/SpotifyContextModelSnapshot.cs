﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spotify.Data;

#nullable disable

namespace Spotify.Data.Migrations
{
    [DbContext(typeof(SpotifyContext))]
    partial class SpotifyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("Spotify.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Homayoun Shajarian"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mohammadreza Shajarian"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mohammad Motamedi"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Keyvan Kalhor"
                        });
                });

            modelBuilder.Entity("Spotify.Models.Music", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtistId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Musics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArtistId = 1,
                            Link = "abr-biseda-mibarad.mp4",
                            Title = "Abr Biseda Mibarad"
                        },
                        new
                        {
                            Id = 2,
                            ArtistId = 1,
                            Link = "tasnif-ghollab.mp4",
                            Title = "Tasnif Ghollab"
                        },
                        new
                        {
                            Id = 3,
                            ArtistId = 2,
                            Link = "rahe-meykhane.mp4",
                            Title = "Rahe Meykhane"
                        },
                        new
                        {
                            Id = 4,
                            ArtistId = 2,
                            Link = "saghi.mp4",
                            Title = "Saghi"
                        },
                        new
                        {
                            Id = 5,
                            ArtistId = 3,
                            Link = "hala-ke-miravi.mp4",
                            Title = "Hala Ke Miravi"
                        },
                        new
                        {
                            Id = 6,
                            ArtistId = 3,
                            Link = "dastam-ra-begir.mp4",
                            Title = "Dastam Ra Begir"
                        },
                        new
                        {
                            Id = 7,
                            ArtistId = 4,
                            Link = "khatoon.mp4",
                            Title = "Khatoon"
                        },
                        new
                        {
                            Id = 8,
                            ArtistId = 4,
                            Link = "gol-va-khak.mp4",
                            Title = "Gol Va Khak"
                        });
                });

            modelBuilder.Entity("Spotify.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "hoseinshaemi@gmail.com",
                            IsAdmin = true,
                            IsVerified = false,
                            Password = "$2b$10$VCdYyQLs.eEiOX3dwIC9hOrSJdhJFUuWzpjCkLpHGy0YG5mfBf37S",
                            Username = "hshaemi"
                        },
                        new
                        {
                            Id = 2,
                            Email = "amirhosseinfathi@gmail.com",
                            IsAdmin = false,
                            IsVerified = false,
                            Password = "$2b$10$x2VMvst1.2JqapA50Dzam.KuGqiQMwwF92tRsqnPgyfv60YgEDmdG",
                            Username = "afathi"
                        },
                        new
                        {
                            Id = 3,
                            Email = "alinikaein@gmail.com",
                            IsAdmin = false,
                            IsVerified = false,
                            Password = "$2b$10$k5zd26k/wenK2bSYbwhMYO.2PcLn9Z/lhKFbim4aTKpPDFvE3KpcC",
                            Username = "anikaein"
                        },
                        new
                        {
                            Id = 4,
                            Email = "mammadmmp@gmail.com",
                            IsAdmin = false,
                            IsVerified = false,
                            Password = "$2b$10$0G5H1OJH.ywohdka4HFNnOUjtrj.x1juMYDNPVjcvQqT3OirNl3eG",
                            Username = "mamadmmp"
                        });
                });

            modelBuilder.Entity("Spotify.Models.UserMusic", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MusicId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "MusicId");

                    b.HasIndex("MusicId");

                    b.ToTable("UserMusics");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            MusicId = 2
                        },
                        new
                        {
                            UserId = 1,
                            MusicId = 3
                        },
                        new
                        {
                            UserId = 2,
                            MusicId = 3
                        },
                        new
                        {
                            UserId = 3,
                            MusicId = 4
                        },
                        new
                        {
                            UserId = 3,
                            MusicId = 5
                        },
                        new
                        {
                            UserId = 4,
                            MusicId = 1
                        },
                        new
                        {
                            UserId = 4,
                            MusicId = 6
                        });
                });

            modelBuilder.Entity("Spotify.Models.Music", b =>
                {
                    b.HasOne("Spotify.Models.Artist", "Artist")
                        .WithMany("Musics")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Spotify.Models.UserMusic", b =>
                {
                    b.HasOne("Spotify.Models.Music", "Music")
                        .WithMany("UserMusics")
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotify.Models.User", "User")
                        .WithMany("UserMusics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Spotify.Models.Artist", b =>
                {
                    b.Navigation("Musics");
                });

            modelBuilder.Entity("Spotify.Models.Music", b =>
                {
                    b.Navigation("UserMusics");
                });

            modelBuilder.Entity("Spotify.Models.User", b =>
                {
                    b.Navigation("UserMusics");
                });
#pragma warning restore 612, 618
        }
    }
}
