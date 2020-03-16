using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delineation.Models
{
    public class D_Res
    {
        public int ID { get; set; }
        [Display(Name="Код")]
        public int Code { get; set; }
        [Display(Name ="Название")]
        public string Name { get; set; }
        public D_Person Nach { get; set; }
        public int? NachId { get; set; }
        public D_Person ZamNach { get; set; }
        public int? ZamNachId { get; set; }
        public D_Person GlInzh { get; set; }
        public int? GlInzhId { get; set; }
        public D_Person Buh { get; set; }
        public int? BuhId { get; set; }
    }
    public class D_Person
    {
        public int Id { get; set; }
        [Display(Name ="Фамилия")]        
        public string Surname { get; set; }
        [Display(Name="Имя")]
        public string Name { get; set; }
        [Display(Name="Отчество")]
        public string Patronymic { get; set; }
        [NotMapped]
        public string FIO { get; set; }
    }
    public class D_Tc
    {
        public int Id { get; set; }

        [Display(Name = "№ ТУ")]
        [Column(TypeName = "nvarchar(20)"), StringLength(20)]
        public string Num { get; set; }

        [Display(Name = "Дата выдачи"),DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "РЭС")]
        public D_Res Res { get; set; }
        [Display(Name = "РЭС")]
        public int? ResId { get; set; }

        [Display(Name ="Наименование организации"), Column(TypeName = "nvarchar(150)"), StringLength(150)]
        public string Company { get; set; }

        [Display(Name = "ФИО заявителя"), Column(TypeName = "nvarchar(70)"), StringLength(70)]
        public string FIO { get; set; }

        [Display(Name ="Наименование объекта"), Column(TypeName = "nvarchar(150)"), StringLength(150)]
        public string ObjName { get; set; }

        [Display(Name = "Адрес объекта строительства"), Column(TypeName = "nvarchar(200)"), StringLength(200)]
        public string Address { get; set; }

        [Display(Name = "Разрешенная мощность"),Column(TypeName ="nvarchar(7)"),StringLength(7,ErrorMessage ="допустимая длинна - 7 символов")]
        public string Pow { get; set; }

        [Display(Name = "Категория")]
        public int Category { get; set; }

        [Display(Name = "Точка подключения"), Column(TypeName = "nvarchar(50)"), StringLength(50)]
        public string Point { get; set; }

        [Display(Name = "Инв. №")]
        public int InvNum { get; set; }

        [Display(Name ="Опора")]
        public int Pillar { get; set; }
    }
    public class DelineationContext: DbContext 
    {
        public DbSet<D_Res> D_Reses { get; set; }
        public DbSet<D_Person> D_Persons { get; set; }
        public DbSet<D_Tc> D_Tces { get; set; }
        public DelineationContext( DbContextOptions<DelineationContext> options): base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
