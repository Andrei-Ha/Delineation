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
    [Migration("20200423135404_agreement")]
    partial class agreement
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("DevBalance")
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

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("StrPSline10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TcId")
                        .HasColumnType("int");

                    b.Property<string>("Validity")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("TcId");

                    b.ToTable("D_Act");
                });

            modelBuilder.Entity("Delineation.Models.D_Agreement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Accept")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ActId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActId");

                    b.HasIndex("PersonId");

                    b.ToTable("D_Agreement");
                });

            modelBuilder.Entity("Delineation.Models.D_Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

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

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Dover")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("FIOnachRod")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int?>("GlInzhId")
                        .HasColumnType("int");

                    b.Property<int?>("NachId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RESa")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("RESom")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

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
                            Id = 541000,
                            BuhId = 5,
                            City = "Пинск",
                            Dover = "от 01.09.2019 №2432",
                            FIOnachRod = "Булавина Виталия Федоровича",
                            GlInzhId = 3,
                            NachId = 2,
                            Name = "Пинский Городской",
                            RESa = "Пинского Городского",
                            RESom = "Пинским Городским",
                            ZamNachId = 4
                        },
                        new
                        {
                            Id = 542000,
                            BuhId = 9,
                            City = "Пинск",
                            Dover = "от 01.09.2019 №2432",
                            FIOnachRod = "Забавнюка Владимира Францевича",
                            GlInzhId = 8,
                            NachId = 6,
                            Name = "Пинский Сельский",
                            RESa = "Пинсого Сельского",
                            RESom = "Пинским Сельским",
                            ZamNachId = 7
                        },
                        new
                        {
                            Id = 543000,
                            BuhId = 1,
                            GlInzhId = 1,
                            NachId = 1,
                            Name = "Лунинецкий",
                            ZamNachId = 1
                        },
                        new
                        {
                            Id = 544000,
                            BuhId = 1,
                            GlInzhId = 1,
                            NachId = 1,
                            Name = "Столинский",
                            ZamNachId = 1
                        },
                        new
                        {
                            Id = 545000,
                            BuhId = 1,
                            GlInzhId = 1,
                            NachId = 1,
                            Name = "Ивановский",
                            ZamNachId = 1
                        },
                        new
                        {
                            Id = 546000,
                            BuhId = 1,
                            GlInzhId = 1,
                            NachId = 1,
                            Name = "Дрогичинский",
                            ZamNachId = 1
                        });
                });

            modelBuilder.Entity("Delineation.Models.D_Tc", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Category2")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FIO")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("Line04")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Line04InvNum")
                        .HasColumnType("int");

                    b.Property<string>("Line10")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Line10InvNum")
                        .HasColumnType("int");

                    b.Property<string>("Num")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("ObjName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("PS")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("PSInvNum")
                        .HasColumnType("int");

                    b.Property<string>("Pillar")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Pow")
                        .HasColumnType("nvarchar(7)")
                        .HasMaxLength(7);

                    b.Property<int?>("ResId")
                        .HasColumnType("int");

                    b.Property<string>("TP")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("TPInvNum")
                        .HasColumnType("int");

                    b.Property<int>("TPnum")
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

            modelBuilder.Entity("Delineation.Models.D_Agreement", b =>
                {
                    b.HasOne("Delineation.Models.D_Act", "Act")
                        .WithMany("Agreements")
                        .HasForeignKey("ActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Delineation.Models.D_Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
