﻿// <auto-generated />
using System;
using Delineation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Delineation.Migrations
{
    [DbContext(typeof(DelineationContext))]
    [Migration("20200317180931_act")]
    partial class act
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Delineation.Models.D_Act", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConsBalance")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("ConsExpl")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DevBalabce")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("DevExpl")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("EntityDoc")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FIOtrans")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsEntity")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTransit")
                        .HasColumnType("bit");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.Property<int?>("TcId")
                        .HasColumnType("int");

                    b.Property<string>("Validity")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("TcId");

                    b.ToTable("D_Act");
                });

            modelBuilder.Entity("Delineation.Models.D_Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("D_Persons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "-",
                            Patronymic = "-",
                            Surname = "-"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Виталий",
                            Patronymic = "Федорович",
                            Surname = "Булавин"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Александр",
                            Patronymic = "Иванович",
                            Surname = "Литвинчук"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Андрей",
                            Patronymic = "Михайлович",
                            Surname = "Германович"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Татьяна",
                            Patronymic = "Вячеславовна",
                            Surname = "Велесницкая"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Владимир",
                            Patronymic = "Францевич",
                            Surname = "Забавнюк"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Федор",
                            Patronymic = "Иванович",
                            Surname = "Калилец"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Анатолий",
                            Patronymic = "Леонидович",
                            Surname = "Климович"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Сиана",
                            Patronymic = "Владимировна",
                            Surname = "Крейдич"
                        });
                });

            modelBuilder.Entity("Delineation.Models.D_Res", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("BuhId")
                        .HasColumnType("int");

                    b.Property<int?>("GlInzhId")
                        .HasColumnType("int");

                    b.Property<int?>("NachId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZamNachId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuhId");

                    b.HasIndex("GlInzhId");

                    b.HasIndex("NachId");

                    b.HasIndex("ZamNachId");

                    b.ToTable("D_Reses");

                    b.HasData(
                        new
                        {
                            Id = 54100,
                            BuhId = 5,
                            GlInzhId = 3,
                            NachId = 2,
                            Name = "Пинский Городской РЭС",
                            ZamNachId = 4
                        },
                        new
                        {
                            Id = 54200,
                            BuhId = 1,
                            GlInzhId = 1,
                            NachId = 1,
                            Name = "Пинский Сельский РЭС",
                            ZamNachId = 1
                        },
                        new
                        {
                            Id = 54300,
                            BuhId = 9,
                            GlInzhId = 8,
                            NachId = 6,
                            Name = "Лунинецкий РЭС",
                            ZamNachId = 7
                        },
                        new
                        {
                            Id = 54400,
                            BuhId = 1,
                            GlInzhId = 1,
                            NachId = 1,
                            Name = "Столинский РЭС",
                            ZamNachId = 1
                        },
                        new
                        {
                            Id = 54500,
                            BuhId = 1,
                            GlInzhId = 1,
                            NachId = 1,
                            Name = "Ивановский РЭС",
                            ZamNachId = 1
                        },
                        new
                        {
                            Id = 54600,
                            BuhId = 1,
                            GlInzhId = 1,
                            NachId = 1,
                            Name = "Дрогичинский РЭС",
                            ZamNachId = 1
                        });
                });

            modelBuilder.Entity("Delineation.Models.D_Tc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FIO")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<int>("InvNum")
                        .HasColumnType("int");

                    b.Property<string>("Num")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("ObjName")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Pillar")
                        .HasColumnType("int");

                    b.Property<string>("Point")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Pow")
                        .HasColumnType("nvarchar(7)")
                        .HasMaxLength(7);

                    b.Property<int?>("ResId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResId");

                    b.ToTable("D_Tces");
                });

            modelBuilder.Entity("Delineation.Models.D_Act", b =>
                {
                    b.HasOne("Delineation.Models.D_Tc", "Tc")
                        .WithMany()
                        .HasForeignKey("TcId");
                });

            modelBuilder.Entity("Delineation.Models.D_Res", b =>
                {
                    b.HasOne("Delineation.Models.D_Person", "Buh")
                        .WithMany()
                        .HasForeignKey("BuhId");

                    b.HasOne("Delineation.Models.D_Person", "GlInzh")
                        .WithMany()
                        .HasForeignKey("GlInzhId");

                    b.HasOne("Delineation.Models.D_Person", "Nach")
                        .WithMany()
                        .HasForeignKey("NachId");

                    b.HasOne("Delineation.Models.D_Person", "ZamNach")
                        .WithMany()
                        .HasForeignKey("ZamNachId");
                });

            modelBuilder.Entity("Delineation.Models.D_Tc", b =>
                {
                    b.HasOne("Delineation.Models.D_Res", "Res")
                        .WithMany()
                        .HasForeignKey("ResId");
                });
#pragma warning restore 612, 618
        }
    }
}