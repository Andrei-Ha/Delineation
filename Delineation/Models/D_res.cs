using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Delineation.Models;

namespace Delineation.Models
{
    enum Stat
    {
        Edit,
        Agreement,
        Completed
    }
    public class D_Res
    {
        [Key]
        [Display(Name = "Код")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Display(Name = "РЭС")]
        public string Name { get; set; }

        public D_Person Nach { get; set; }
        [Display(Name = "начальник")]
        public int? NachId { get; set; }
        public D_Person ZamNach { get; set; }
        [Display(Name = "зам.нач. по сбыту")]
        public int? ZamNachId { get; set; }
        public D_Person GlInzh { get; set; }
        [Display(Name = "гл. инженер")]
        public int? GlInzhId { get; set; }
        public D_Person Buh { get; set; }
        [Display(Name = "бухгалтер")]
        public int? BuhId { get; set; }

        [Display(Name = "город"), Column(TypeName = "nvarchar(30)"), StringLength(30)]
        public string City { get; set; }

        [Display(Name = "РЭСа"), Column(TypeName = "nvarchar(30)"), StringLength(30)]
        public string RESa { get; set; }

        [Display(Name = "РЭСом"), Column(TypeName = "nvarchar(30)"), StringLength(30)]
        public string RESom { get; set; }

        [Display(Name = "Ф.И.О. нач. в родит.падеже"), Column(TypeName = "nvarchar(30)"), StringLength(30)]
        public string FIOnachRod { get; set; }

        [Display(Name = "доверенность '№_ от _'"), Column(TypeName = "nvarchar(30)"), StringLength(30)]
        public string Dover { get; set; }
    }
    public class D_Person
    {
        public int Id { get; set; }
        [Display(Name = "Фамилия"), Column(TypeName = "nvarchar(70)"), StringLength(70)]
        public string Surname { get; set; }

        [Display(Name = "Имя"), Column(TypeName = "nvarchar(70)"), StringLength(70)]
        public string Name { get; set; }

        [Display(Name = "Отчество"), Column(TypeName = "nvarchar(70)"), StringLength(70)]
        public string Patronymic { get; set; }

        [NotMapped]
        public string FIO { get; set; }
    }
    public class D_Tc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Display(Name = "№ ТУ")]
        [Column(TypeName = "nvarchar(20)"), StringLength(20)]
        public string Num { get; set; }

        [Display(Name = "Дата выдачи"), DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "РЭС")]
        public D_Res Res { get; set; }
        [Display(Name = "РЭС")]
        public int? ResId { get; set; }

        [Display(Name = "Наименование организации"), Column(TypeName = "nvarchar(500)"), StringLength(500)]
        public string Company { get; set; }

        [Display(Name = "ФИО заявителя"), Column(TypeName = "nvarchar(70)"), StringLength(70)]
        public string FIO { get; set; }

        [Display(Name = "Наименование объекта"), Column(TypeName = "nvarchar(500)"), StringLength(500)]
        public string ObjName { get; set; }

        [Display(Name = "Адрес объекта строительства"), Column(TypeName = "nvarchar(250)"), StringLength(250)]
        public string Address { get; set; }

        [Display(Name = "Разрешенная мощность, кВт."), Column(TypeName = "nvarchar(7)"), StringLength(7, ErrorMessage = "допустимая длинна - 7 символов")]
        public string Pow { get; set; }
        
        [Display(Name = "Категория по надежн. эл. снабжен. эл. установок потребителя"), Column(TypeName = "nvarchar(5)"), StringLength(5)]
        public string Category { get; set; }

        [Display(Name = "Категория по надежн. эл. снабжен. внешней схемы"), Column(TypeName = "nvarchar(5)"), StringLength(5)]
        public string Category2 { get; set; }

        [Display(Name = "Название ПС"), Column(TypeName = "nvarchar(50)"), StringLength(50)]
        public string PS { get; set; }

        [Display(Name = "Инвентарный №")]
        public int PSInvNum { get; set; }

        [Display(Name = "Линия 10кВ"), Column(TypeName = "nvarchar(50)"), StringLength(50)]
        public string Line10 { get; set; }

        [Display(Name = "Инвентарный №")]
        public int Line10InvNum { get; set; }

        [Display(Name = "Название ТП"), Column(TypeName = "nvarchar(50)"), StringLength(50)]
        public string TP { get; set; }

        [Display(Name = "диспетчерский № ТП")]
        public int TPnum { get; set; }

        [Display(Name = "Инвентарный №")]
        public int TPInvNum { get; set; }

        [Display(Name = "Линия 0,4кВ"), Column(TypeName = "nvarchar(50)"), StringLength(50)]
        public string Line04 { get; set; }

        [Display(Name = "Инвентарный №")]
        public int Line04InvNum { get; set; }

        [Display(Name = "Опора №"), Column(TypeName = "nvarchar(10)"), StringLength(10)]
        public string Pillar { get; set; }
    }
    public class D_Act
    {
        public int Id { get; set; }

        [Display(Name = "дата выдачи акта"), DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public D_Tc Tc { get; set; }
        [Display(Name = "ТУ")]
        public int? TcId { get; set; }

        [Display(Name = "юр. лицо")]
        public bool IsEntity { get; set; }

        [Display(Name = "действующ. на основании (доверенности, устава) №"), Column(TypeName = "nvarchar(50)"), StringLength(50)]
        public string EntityDoc { get; set; }

        [Display(Name = "На балансе потребителя"), Column(TypeName = "nvarchar(250)"), StringLength(250)]
        public string ConsBalance { get; set; }

        [Display(Name = "Граница баланс. принадлежн. находится на "), Column(TypeName = "nvarchar(150)"), StringLength(150)]
        public string DevBalance { get; set; }

        [Display(Name = "Эксплутационная ответственность потребителя"), Column(TypeName = "nvarchar(250)"), StringLength(250)]
        public string ConsExpl { get; set; }

        [Display(Name = "Граница эксплутационной отв. находится на "), Column(TypeName = "nvarchar(150)"), StringLength(150)]
        public string DevExpl { get; set; }

        [Display(Name = "транзитные сети")]
        public bool IsTransit { get; set; }

        [Display(Name = "ФИО представителя владельца транзитных электрических сетей"), Column(TypeName = "nvarchar(50)"), StringLength(50)]
        public string FIOtrans { get; set; }

        [Display(Name = "Срок действия акта"), Column(TypeName = "nvarchar(50)"), StringLength(50)]
        public string Validity { get; set; }

        [Display(Name = "статус")]
        public int State { get; set; }

        public List<D_Agreement> Agreements { get; set; }

        [Display(Name = "Основная питающая линия 10кВ")]
        [NotMapped]
        public string Temp { get; set; }
        
        public string StrPSline10 { get; set; }
    }
    public class D_Agreement
    {
        public int Id { get; set; }
        public D_Act Act { get; set; }
        public int ActId { get; set; }
        public D_Person Person { get; set; }
        public int PersonId { get; set; }
        public bool Accept { get; set; }
        [Display(Name = "Замечания"), Column(TypeName = "nvarchar(250)"), StringLength(250)]
        public string Note { get; set; }
        [Display(Name = "дата выдачи акта"), DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "информация"), Column(TypeName = "nvarchar(100)"), StringLength(100)]
        public string Info { get; set; }
    }
    public class DelineationContext : DbContext
    {
        public DbSet<D_Res> D_Reses { get; set; }
        public DbSet<D_Person> D_Persons { get; set; }
        public DbSet<D_Tc> D_Tces { get; set; }
        public DbSet<D_Act> d_Acts { get; set; }
        public DelineationContext(DbContextOptions<DelineationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<D_Person>().HasData(new D_Person[] {
            new D_Person { Id = 1, Surname = "-", Name = "-", Patronymic = "-" },
            new D_Person { Id = 2, Surname = "Булавин", Name = "Виталий", Patronymic = "Федорович" },
            new D_Person { Id = 3, Surname = "Литвинчук", Name = "Александр", Patronymic = "Иванович" },
            new D_Person { Id = 4, Surname = "Германович", Name = "Андрей", Patronymic = "Михайлович" },
            new D_Person { Id = 5, Surname = "Велесницкая", Name = "Татьяна", Patronymic = "Вячеславовна" },
            new D_Person { Id = 6, Surname = "Забавнюк", Name = "Владимир", Patronymic = "Францевич" },
            new D_Person { Id = 7, Surname = "Калилец", Name = "Федор", Patronymic = "Иванович" },
            new D_Person { Id = 8, Surname = "Климович", Name = "Анатолий", Patronymic = "Леонидович" },
            new D_Person { Id = 9, Surname = "Крейдич", Name = "Сиана", Patronymic = "Владимировна" }
            }
            );
            modelBuilder.Entity<D_Res>().HasData(
                new D_Res[]
                {
                    new D_Res{ Id=541000, Name="Пинский Городской", BuhId=5, GlInzhId=3, NachId=2, ZamNachId=4, City="Пинск", RESa="Пинского Городского", RESom="Пинским Городским", FIOnachRod="Булавина Виталия Федоровича", Dover="от 01.09.2019 №2432"},
                    new D_Res{ Id=542000, Name="Пинский Сельский", BuhId=9, GlInzhId=8, NachId=6, ZamNachId=7, City="Пинск", RESa="Пинсого Сельского", RESom="Пинским Сельским", FIOnachRod="Забавнюка Владимира Францевича", Dover="от 01.09.2019 №2432"},
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
        public DbSet<Delineation.Models.D_Act> D_Act { get; set; }
    }
    [NotMapped]
    public class SelList{
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
