using APIassignment.Models;
using Microsoft.EntityFrameworkCore;

namespace APIassignment.DataAccess
{

        public class ApplicationContext : DbContext
        {
            // constructor
            public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
            {

            }

            // mapping Model into Table in database
            // OnModelCreating is a function that's already provided in DbContext
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Product>(entity =>
                {
                    // table
                    // If we don't have, it will gen table with the same name as data set
                    entity.ToTable("Product");

                    // PK
                    entity.HasKey(p => p.Id);

                    // AI
                    entity.Property(p => p.Id).ValueGeneratedOnAdd();
                });

                base.OnModelCreating(modelBuilder);
            }

            //data set
            public DbSet<Product> Products { get; set; }
        }


    }


