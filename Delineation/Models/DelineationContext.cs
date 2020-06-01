using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Delineation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CustomIdentity.Models;

namespace Delineation.Models
{
    public class DelineationContext : IdentityDbContext<User>//DbContext
    {
        public DbSet<D_Res> D_Reses { get; set; }
        public DbSet<D_Person> D_Persons { get; set; }
        public DbSet<D_Tc> D_Tces { get; set; }
        public DbSet<D_Act> D_Acts { get; set; }
        public DbSet<D_Agreement> D_Agreements { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<D_Position> Positions { get; set; }
        public DelineationContext(DbContextOptions<DelineationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  получение данных через представление
            modelBuilder.Entity<Unit>(p => { p.HasNoKey(); p.ToTable("sprpodr"); });
            modelBuilder.Entity<D_Position>().HasData(new D_Position[] {
                new D_Position() {Id=1, Name="Начальник РЭС"},
                new D_Position() {Id=2, Name="Зам. начальника РЭС"},
                new D_Position() {Id=3, Name="Главный инженер РЭС"},
                new D_Position() {Id=4, Name="Бухгалтер РЭС"},
                new D_Position() {Id=5, Name="Главный инженер ВРЭС"},
                new D_Position() {Id=6, Name="Начальник ВРЭС"},
                new D_Position() {Id=7, Name="Старший мастер ССЭЭ"},
                new D_Position() {Id=8, Name="Бухгалтер по учёту ОС"},
                new D_Position() {Id=9, Name="Начальник ОДС"},
                new D_Position() {Id=10, Name="Начальник СРС"},
                new D_Position() {Id=11, Name="Начальник ССЭЭ"},
                new D_Position() {Id=12, Name="Главный бухгалтер"}
            });

            modelBuilder.Entity<D_Person>().HasData(new D_Person[] {
            new D_Person { Id = 1, Surname = "Забавнюк", Name = "Владимир", Patronymic = "Францевич", Kod_long="542000", Linom="505", PositionId=1 },
            new D_Person { Id = 2, Surname = "Калилец", Name = "Федор", Patronymic = "Иванович", Kod_long="542000", Linom="1785", PositionId=2  },
            new D_Person { Id = 3, Surname = "Климович", Name = "Анатолий", Patronymic = "Леонидович", Kod_long="542000", Linom="1672", PositionId=3  },
            new D_Person { Id = 4, Surname = "Крейдич", Name = "Сиана", Patronymic = "Владимировна", Kod_long="542000", Linom="2149", PositionId=4  }
            }
            );
            modelBuilder.Entity<D_Res>().HasData(
                new D_Res[]
                {
                    new D_Res{ Id=541000, Name="Пинский Городской", BuhId=1, GlInzhId=1, NachId=1, ZamNachId=1, City="Пинск", RESa="Пинского Городского", RESom="Пинским Городским", FIOnachRod="Булавина Виталия Федоровича", Dover="от 01.09.2019 №2432"},
                    new D_Res{ Id=542000, Name="Пинский Сельский", BuhId=4, GlInzhId=3, NachId=1, ZamNachId=2, City="Пинск", RESa="Пинского Сельского", RESom="Пинским Сельским", FIOnachRod="Забавнюка Владимира Францевича", Dover="от 01.09.2019 №2432"},
                    new D_Res{ Id=543000, Name="Лунинецкий", BuhId=1, GlInzhId=1, NachId=1, ZamNachId=1},
                    new D_Res{ Id=544000, Name="Столинский", BuhId=1, GlInzhId=1, NachId=1, ZamNachId=1},
                    new D_Res{ Id=545000, Name="Ивановский", BuhId=1, GlInzhId=1, NachId=1, ZamNachId=1},
                    new D_Res{ Id=546000, Name="Дрогичинский", BuhId=1, GlInzhId=1, NachId=1, ZamNachId=1}
                });
            modelBuilder.Entity<D_Act>().Property(p => p.Date).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<D_Act>().Property(u => u.State).HasDefaultValue(Stat.Edit);
            modelBuilder.Entity<D_Agreement>().Property(p => p.Accept).HasDefaultValue(false);
            modelBuilder.Entity<D_Agreement>().Property(p => p.Date).HasDefaultValueSql("GETDATE()");
            base.OnModelCreating(modelBuilder);
            {
                //Атрибут Table позволяет переопределить сопоставление с таблицей по имени: [Table("People")]
                //modelBuilder.Entity<D_Tc>().ToTable("TU")
                //С помощью дополнительного параметра schema можно определить схему, к которой будет принадлежать таблица:
                //modelBuilder.Entity<User>().ToTable("People", schema: "userstore");
                //Сопоставление столбцов [Column("user_id")] -- modelBuilder.Entity<User>().Property(u=>u.Id).HasColumnName("user_id");
                //По умолчанию в качестве ключа используется свойство, которое называется Id или or [имя_класса]Id. // [Key] -- 
                // Составной ключ - modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("UsersPrimaryKey");
                // Альтернативные ключи: modelBuilder.Entity<User>().HasAlternateKey(u => u.Passport);
                // настройка индексов: modelBuilder.Entity<User>().HasIndex(u => u.Passport);  уникальность: modelBuilder.Entity<User>().HasIndex(u => u.Passport).IsUnique();
                // отключить автогенерацию значения при добавлении: [DatabaseGenerated(DatabaseGeneratedOption.None)] -- modelBuilder.Entity<User>().Property(b => b.Id).ValueGeneratedNever();
                // включить автогенерацию значения при добавлении: [DatabaseGenerated(DatabaseGeneratedOption.Identity)] --
                //Значения по умолчанию: modelBuilder.Entity<User>().Property(u => u.Age).HasDefaultValue(18);
                // HasDefaultValueSql() значение устанавливается на основе кода SQL: modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
                // HasComputedColumnSql() можно установить в бд SQL-выражение: modelBuilder.Entity<User>().Property(u => u.Name).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
                // Обязательные свойства: [Required] -- modelBuilder.Entity<User>().Property(b => b.Name).IsRequired();
                // Ограничения по длине: [MaxLength(50)] -- modelBuilder.Entity<User>().Property(b => b.Name).HasMaxLength(50);
                // Типы данных: [Column(TypeName = "nvarchar(200)")] -- modelBuilder.Entity<User>().Property(u=>u.Name).HasColumnType("nvarchar(200)");
                /* Инициализация БД начальными данными:
                    modelBuilder.Entity<User>().HasData(
                    new User[] 
                    {
                        new User { Id=1, Name="Tom", Age=23},
                        new User { Id=2, Name="Alice", Age=26},
                        new User { Id=3, Name="Sam", Age=28}
                    });*/

                // public DbSet<D_Tc> D_Tces { get; set; } -- modelBuilder.Entity<D_Tc>();
                //[NotMapped]
                //modelBuilder.Entity<D_Tc>().Ignore(p => p.TestFluentAPI);
                //modelBuilder.Entity<D_Tc>().
                //
            }
        }
        public DbSet<Delineation.Models.D_Position> D_Position { get; set; }
    }
}
//sql to delete all tables
/*
DROP TABLE D_Agreements;
DROP TABLE D_Acts;
DROP TABLE D_Tces;
DROP TABLE D_Reses;
DROP TABLE D_Persons;
DROP TABLE D_Position;*/