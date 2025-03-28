﻿// <auto-generated />
using System;
using EMG_MED1000_BACKEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EMG_Med1000_backend.Migrations
{
    [DbContext(typeof(VoitureContext))]
    partial class VoitureContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EMG_MED1000_BACKEND.Entities.Marque", b =>
                {
                    b.Property<int>("MarqId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MarqId"));

                    b.Property<string>("NomMarq")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("modeleModelId")
                        .HasColumnType("int");

                    b.HasKey("MarqId");

                    b.HasIndex("NomMarq")
                        .IsUnique();

                    b.HasIndex("modeleModelId");

                    b.ToTable("Marques");
                });

            modelBuilder.Entity("EMG_MED1000_BACKEND.Entities.Modele", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ModelId"));

                    b.Property<int>("MarqId")
                        .HasColumnType("int");

                    b.Property<DateTime>("anneeModele")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("nomModele")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("ModelId");

                    b.HasIndex("MarqId");

                    b.HasIndex("nomModele")
                        .IsUnique();

                    b.ToTable("Modeles");
                });

            modelBuilder.Entity("EMG_MED1000_BACKEND.Entities.Voiture", b =>
                {
                    b.Property<int>("VoitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("VoitId"));

                    b.Property<int>("MarqId")
                        .HasColumnType("int");

                    b.Property<DateTime>("anneeVoiture")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("descrVoiture")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("marqueMarqId")
                        .HasColumnType("int");

                    b.Property<string>("photoVoiture")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("statutVoiture")
                        .HasColumnType("int");

                    b.HasKey("VoitId");

                    b.HasIndex("marqueMarqId");

                    b.ToTable("Voitures");
                });

            modelBuilder.Entity("EMG_MED1000_BACKEND.Entities.Marque", b =>
                {
                    b.HasOne("EMG_MED1000_BACKEND.Entities.Modele", "modele")
                        .WithMany()
                        .HasForeignKey("modeleModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("modele");
                });

            modelBuilder.Entity("EMG_MED1000_BACKEND.Entities.Modele", b =>
                {
                    b.HasOne("EMG_MED1000_BACKEND.Entities.Marque", "Marque")
                        .WithMany("ListModele")
                        .HasForeignKey("MarqId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marque");
                });

            modelBuilder.Entity("EMG_MED1000_BACKEND.Entities.Voiture", b =>
                {
                    b.HasOne("EMG_MED1000_BACKEND.Entities.Marque", "marque")
                        .WithMany()
                        .HasForeignKey("marqueMarqId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("marque");
                });

            modelBuilder.Entity("EMG_MED1000_BACKEND.Entities.Marque", b =>
                {
                    b.Navigation("ListModele");
                });
#pragma warning restore 612, 618
        }
    }
}
