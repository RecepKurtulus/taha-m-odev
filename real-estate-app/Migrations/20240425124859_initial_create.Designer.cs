﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using real_estate_app.Models;

#nullable disable

namespace real_estate_app.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240425124859_initial_create")]
    partial class initial_create
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("real_estate_app.Models.Emlak", b =>
                {
                    b.Property<int>("EmlakId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmlakId"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Fiyat")
                        .HasColumnType("integer");

                    b.Property<string>("Ilce")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MetreKare")
                        .HasColumnType("integer");

                    b.Property<int>("OdaSayisi")
                        .HasColumnType("integer");

                    b.Property<int>("SaticiId")
                        .HasColumnType("integer");

                    b.Property<bool>("SatildiMi")
                        .HasColumnType("boolean");

                    b.Property<string>("Sehir")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EmlakId");

                    b.HasIndex("SaticiId");

                    b.ToTable("Emlaklar");
                });

            modelBuilder.Entity("real_estate_app.Models.Satici", b =>
                {
                    b.Property<int>("SaticiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SaticiId"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SaticiId");

                    b.ToTable("Saticilar");
                });

            modelBuilder.Entity("real_estate_app.Models.Emlak", b =>
                {
                    b.HasOne("real_estate_app.Models.Satici", "Satici")
                        .WithMany()
                        .HasForeignKey("SaticiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Satici");
                });
#pragma warning restore 612, 618
        }
    }
}
