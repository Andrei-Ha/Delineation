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
        [Column(TypeName = "varchar(20)")]
        public string Num { get; set; }
        [Display(Name = "Дата выдачи")]
        public DateTime Date { get; set; }
        [Display(Name = "ФИО заявителя")]
        public string FIO { get; set; }
        [Display(Name = "РЭС")]
        public D_Res Res { get; set; }
        public int? ResId { get; set; }
        [Display(Name = "Адрес объекта строительства")]
        public string Address { get; set; }
        [Display(Name = "Разрешенная мощность")]
        public double Pow { get; set; }
        [Display(Name = "Категория")]
        public int Category { get; set; }
        [Display(Name = "Точка подключения")]
        public string Point { get; set; }
        [Display(Name = "Инв. №")]
        public int InvNum { get; set; }
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
    }
}
