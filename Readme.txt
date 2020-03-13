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
            // Типы данных: [Column(TypeName = "varchar(200)")] -- modelBuilder.Entity<User>().Property(u=>u.Name).HasColumnType("varchar(200)");


            // public DbSet<D_Tc> D_Tces { get; set; } -- modelBuilder.Entity<D_Tc>();
            //[NotMapped]
            //modelBuilder.Entity<D_Tc>().Ignore(p => p.TestFluentAPI);
            //modelBuilder.Entity<D_Tc>().
            //
            base.OnModelCreating(modelBuilder);
        }
    }