using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Statuses> Statuses { get; set;}
        public DbSet<Transports> Transports { get; set; }

        public DbSet<Privilleges> Privilleges { get; set; }
        public DbSet<Workers> Workers { get; set; }

        public DbSet<Users> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }

        public DbSet<Repairs> Repairs { get; set; }

        public DbSet<Maintenances> Maintenances { get; set; }

        public DbSet<Transactions> Transactions { get; set; }


        //Foreign Key Relationships

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //transport can have one category

            //modelBuilder.Entity<Transports>()
            //    //Transport can have one category
            //    .HasOne(t => t.Categories)
            //    //Category can have many transports
            //    .WithMany(c => c.Transports)
            //    //Joined By category_id Column on <Transports>
            //    .HasForeignKey(t => t.category_id);

            // tas pati galima parasyti kitaip:

            //Transports Table Relationships

            // kategorija gali tureti daug transportu
            modelBuilder.Entity<Categories>()
            //Transport can have one category
            .HasMany(t => t.Transports)
            //Category can have many transports
            .WithOne(c => c.Categories)
            //Joined By category_id Column on <Transports>
            .HasForeignKey(t => t.category_id);


            modelBuilder.Entity<Transports>()
                .HasOne(t => t.Locations)
                .WithMany(l => l.Transports)
                .HasForeignKey(t => t.location_id);

            modelBuilder.Entity<Transports>()
                .HasOne(t => t.Statuses)
                .WithMany(s => s.Transports)
                .HasForeignKey(t => t.status_id);

            // Workers Table Relationships:

            modelBuilder.Entity<Workers>()
                //worker can have one location
                .HasOne(t => t.Location)
                //location can have many workers/employees
                .WithMany(c => c.Workers)
                //Joined By location_id Column on <Workers> table
                .HasForeignKey(t => t.location_id);





            //Workers - Privilleges relationship

            modelBuilder.Entity<Workers>()
                    //worker can have one privillege
                    .HasOne(t => t.Privillege)
                    //privillege can belong to multiple workers/employees
                    .WithMany(c => c.Workers)
                    //Joined By privillege_id Column on <Workers> table
                    .HasForeignKey(t => t.privillege_id);

            //Orders Table Relationships

            //Orders - User relationship

                modelBuilder.Entity<Orders>()
                .HasOne(t => t.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(t => t.user_id);

            //Orders - Transport relationship

                modelBuilder.Entity<Orders>()
                .HasOne(t => t.Transport)
                .WithMany(o => o.Orders)
                .HasForeignKey(t => t.transport_id);

            //Orders - Status relationship

                modelBuilder.Entity<Orders>()
                .HasOne(t => t.Status)
                .WithMany(o => o.Orders)
                .HasForeignKey(t => t.status_id);


            //Maintenances Table Relationships

            //Maintenances - Workers relationship
                modelBuilder.Entity<Maintenances>()
                .HasOne(t => t.Worker)
                .WithMany(o => o.Maintenances)
                .HasForeignKey(t => t.worker_id);

            //Maintenances - Transports relationship

                modelBuilder.Entity<Maintenances>()
                .HasOne(t => t.Transport)
                .WithMany(o => o.Maintenances)
                .HasForeignKey(t => t.transport_id);

            //Maintenances - Repairs relationship

                modelBuilder.Entity<Maintenances>()
                .HasOne(t => t.Repair)
                .WithMany(o => o.Maintenances)
                .HasForeignKey(t => t.repair_id);

            //Transactions relationships

            //Transactions - users table relationship

                modelBuilder.Entity<Transactions>()
                .HasOne(t => t.User)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.user_id);

            //Transactions - workers table relationship
                modelBuilder.Entity<Transactions>()
                .HasOne(t => t.Worker)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.worker_id);
        }


    }
}
