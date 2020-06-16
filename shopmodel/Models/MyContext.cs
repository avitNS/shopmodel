using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace shopmodel.Models
{
    public class MyContext : DbContext
    {

        public MyContext() : base("name=dbConn")
        {
            
        }

        public DbSet<Genre> Genres { get; set; }
        
        public DbSet<MediaType> MediaTypes  {get;set;}
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<EnableStatuses> EnableStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<EmpFunction> EmpFunctions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SaleReceipt> SalesReceipts { get; set; }
        public DbSet<Sale> Sales { get; set; }

        



    }
}
