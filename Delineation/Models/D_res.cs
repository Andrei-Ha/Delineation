using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Delineation.Models
{
    public class D_Res
    {
        public int ID { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public D_Person Nach { get; set; }
        public D_Person ZamNach { get; set; }
        public D_Person GlInzh { get; set; }
        public D_Person Buh { get; set; }
    }
    public class D_Person
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
    }
    public class DelineationContext: DbContext 
    {
        public DbSet<D_Res> D_Reses { get; set; }
        public DbSet<D_Person> D_Persons { get; set; }
        public DelineationContext( DbContextOptions<DelineationContext> options): base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
