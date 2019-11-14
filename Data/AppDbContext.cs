using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Warehouse.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Warehouse.Models.Inventory> Inventory { get; set; }

        public Warehouse.Models.Inventory GetInventory() {

            string queryString = "Select * from vision.pub.pv_inventory where pv_inventory.compnum = 413 and pv_inventory.inventoryref = 'ir001'";
            string connectionString = "";
            OdbcCommand command = new OdbcCommand(queryString);

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();

                // The connection is automatically closed at 
                // the end of the Using block.
            }
            Warehouse.Models.Inventory inventory = new Warehouse.Models.Inventory();
            inventory.Compnum = 2;
            inventory.Inventoryqty = 100;
            inventory.Inventoryref = "IR0001";
            inventory.Whousecode = "WH1";
            return inventory;
        }
    }
}
