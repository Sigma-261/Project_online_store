using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFants.Entities
{
    public class FBEntities : DbContext
	{
		public DbSet<User> USERS { get; set; }
        public DbSet<Order> ORDERS { get; set; }

        //-----------------------------МОИ-----------------------------

        public class User
        {
			public int ID { get; set; }
			public string LOGIN { get; set; }
			public string PASSWORD { get; set; }
			public bool IS_ADMIN { get; set; }
		}

        public class Order
        {
            public int ID { get; set; }
            public int PRICE { get; set; }
            public DateTime DATETIME { get; set; }
            public int ID_USER { get; set; }
        }

        public class OrderWithUser
        {
            public int ID { get; set; }
            public int PRICE { get; set; }
            public DateTime DATETIME { get; set; }
            public int ID_USER { get; set; }
            public string LOGIN_USER { get; set; }
        }

        //-----------------------------Конец-----------------------------
        public FBEntities(DbContextOptions<FBEntities> dboe)
            : base(dboe)
        { }

        protected override void OnModelCreating(ModelBuilder modelo)
        {
            //Fluent Api
            modelo.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.ID)
                    .HasName("ID")
                    .IsUnique();
            });
        }
    }
}
