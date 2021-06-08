using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyFants.Entities
{
    public class PGEntities : DbContext
    {
        //-----------------------------СУЩНОСТИ-----------------------------

        public class Product
        {
            public int id { get; set; }
            public string name { get; set; }
            public int price { get; set; }
            public DateTime date_time { get; set; }
            public string description { get; set; }
            public int sellerid { get; set; }

            public Seller Seller { get; set; }
        }

        public class Seller
        {
            public int id { get; set; }
            public string passport { get; set; }

            public List<Product> products { get; set; } = new List<Product>();
        }

        public class Category
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Category_Product
        {
            public int id { get; set; }
            public int categoryid { get; set; }
            public int productid { get; set; }
        }

        public class ProductWithCategorySeller
            {
            public int productid { get; set; }
            public string name { get; set; }
            public int price { get; set; }
            public DateTime date_time { get; set; }
            public string description { get; set; }
            public string category { get; set; }
            public int sellerid { get; set; }
            public string passport { get; set; }

            public Seller Seller { get; set; }
        }

        //-----------------------------Конец-----------------------------

        public DbSet<Product> products { get; set; }
        public DbSet<Seller> sellers { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Category_Product> category_product { get; set; }

        public PGEntities(DbContextOptions<PGEntities> dboe)
            : base(dboe)
        {}
    }
}
