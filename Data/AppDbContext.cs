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
            // string connectionString = "DRIVER={Progress OpenEdge 11.7 Driver};HOST=ukdrdev2;PORT=15192;DB=Vision;UID=sysprogress;PWD=sysprogress;DIL=0;AS=50;";
            string connectionString = "DRIVER={DataDirect 7.1 OpenEdge Wire Protocol};HOST=ukdrdev2;PORT=15192;DB=vision;UID=sysprogress;PWD=sysprogress;DIL=0;AS=50;";
           // DRIVER ={ DataDirect 7.1 DB2 Wire Protocol}; IpAddress = 123.456.78.90;            PORT = 5179; DB = DB2DATA; UID = JOHN; PWD = XYZZY
            OdbcCommand command = new OdbcCommand(queryString);
            command.CommandType = System.Data.CommandType.Text;
            

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                //command.Connection = connection;
                connection.Open();
                //command.ExecuteNonQuery();
                command.Connection = connection;
                command.CommandText = queryString;              
                OdbcDataReader dr = command.ExecuteReader();
                while(dr.Read())
                {
                    string s = dr.ToString();
                    Console.WriteLine(s);
                }

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
