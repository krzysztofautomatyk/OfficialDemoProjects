using Microsoft.EntityFrameworkCore;

namespace WebApplication_Basic_EntityFramework.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Epic> Epic { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkItemState> WorkItemsState { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This is custom values for tables with special properties
            modelBuilder.Entity<WorkItem>()
                .Property(x => x.Area)
                .HasColumnType("varchar(200)");

            modelBuilder.Entity<WorkItem>()
                .Property(x => x.Area)
                .HasColumnType("varchar(200)");

            modelBuilder.Entity<WorkItemState>()
                .Property(x => x.State)
                .IsRequired()
                .HasMaxLength(60);

            modelBuilder.Entity<Epic>()
                .Property(w => w.EndDate)
                .HasPrecision(3);

            modelBuilder.Entity<Task>()
                .Property(w => w.Activity)
                .HasMaxLength(200);

            modelBuilder.Entity<Task>()
                .Property(w => w.RemaningWork)
                .HasPrecision(14, 2);


            modelBuilder.Entity<Issue>()
                .Property(w => w.Efford)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(x => x.Area).HasColumnType("varchar(200)");

                eb.Property(x => x.IterationPath).HasColumnName("Iteration_Path");
  
                // Default value
                eb.Property(x => x.Priority).HasDefaultValue(1);

                // Key N:1
                eb.HasMany(x => x.Comments)
                    .WithOne(x => x.WorkItem)
                    .HasForeignKey(x => x.WorkItemId);

                // Key 1:N
                eb.HasOne(x => x.Author)
                    .WithMany(x => x.WorkItems)
                    .HasForeignKey(x => x.AuthorId);

                // Key N:N
                // With this code we can create a table with 3 columns
                // WorkItemId, TagId, PublicationDate

                eb.HasMany(x => x.Tags)
                    .WithMany(Tags => Tags.WorkItems)
                    .UsingEntity<WorkItemTag>(
                        workItemTag => workItemTag.HasOne(x => x.Tag)
                            .WithMany()
                            .HasForeignKey(x => x.TagId),
                        workItemTag => workItemTag.HasOne(x => x.WorkItem)
                            .WithMany()
                            .HasForeignKey(x => x.WorkItemId),
                        workItemTag =>
                        {
                            workItemTag.HasKey(x => new { x.WorkItemId, x.TagId });
                            workItemTag.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                        }
                    );

                eb.HasOne(x => x.WorkItemState)
                .WithMany()
                .HasForeignKey(x => x.WorkItemStateId);

            });

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
                eb.HasOne(x=> x.Author)
                    .WithMany(c=>c.Comments)
                    .HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Key 1:1
            modelBuilder.Entity<User>() // User and UserId are used to create a one-to-one relationship between Address and User
                .HasOne(a => a.Address)
                .WithOne(u => u.User)
                .HasForeignKey<Address>(a => a.UserId);


            // Key N:N
            // This is for old version of EF Core we not use this class
            //modelBuilder.Entity<WorkItemTag>()
            //    .HasKey(wt => new { wt.WorkItemId, wt.TagId });


            // Seed data
            modelBuilder.Entity<WorkItemState>()
                .HasData(
                new WorkItemState { Id = 1, State = "To Do" },
                new WorkItemState { Id = 2, State = "Doing" },
                new WorkItemState { Id = 3, State = "Done" }
                );

            modelBuilder.Entity<Tag>(eb =>
            {
                eb.HasData(
                    new Tag { Id = 1, Value = "Web" },
                    new Tag { Id = 2, Value = "UI" },
                    new Tag { Id = 3, Value = "Desktop" },
                    new Tag { Id = 4, Value = "API" },
                    new Tag { Id = 5, Value = "Service" } 
                     
                    );
            });

        }


        //Create primary key for WorkItem
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<WorkItem>().HasKey(w => w.Id);
        //    modelBuilder.Entity<User>().HasKey(u => u.Id);
        //    modelBuilder.Entity<Tag>().HasKey(t => t.Id);
        //    modelBuilder.Entity<Comment>().HasKey(c => c.Id);
        //    modelBuilder.Entity<Address>().HasKey(a => a.Id);
        //}

        // Connection to database
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=BoradDB;Trusted_Connection=True;");
        //}
    }
}
