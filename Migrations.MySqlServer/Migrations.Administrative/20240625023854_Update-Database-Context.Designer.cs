﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Migrations.MySqlServer;

#nullable disable

namespace Migrations.MySqlServer.Migrations.Administrative
{
    [DbContext(typeof(MySqlServerContextAdministrative))]
    [Migration("20240625023854_Update-Database-Context")]
    partial class UpdateDatabaseContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Administrative.Agreggates.AdministrativeAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DtCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("PerfilTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PerfilTypeId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("Domain.Administrative.ValueObject.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Perfil", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Administrador"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Normal"
                        });
                });

            modelBuilder.Entity("Domain.Administrative.Agreggates.AdministrativeAccount", b =>
                {
                    b.HasOne("Domain.Administrative.ValueObject.Perfil", "PerfilType")
                        .WithMany("Users")
                        .HasForeignKey("PerfilTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Core.ValueObject.Login", "Login", b1 =>
                        {
                            b1.Property<Guid>("AdministrativeAccountId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("varchar(150)")
                                .HasColumnName("Email");

                            b1.Property<string>("Password")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("varchar(255)")
                                .HasColumnName("Password");

                            b1.HasKey("AdministrativeAccountId");

                            b1.HasIndex("Email")
                                .IsUnique();

                            b1.ToTable("Account");

                            b1.WithOwner()
                                .HasForeignKey("AdministrativeAccountId");
                        });

                    b.Navigation("Login")
                        .IsRequired();

                    b.Navigation("PerfilType");
                });

            modelBuilder.Entity("Domain.Administrative.ValueObject.Perfil", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
