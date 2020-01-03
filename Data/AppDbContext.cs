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

        public List<Inventory> GetInventory() {

            string queryString = "Select * from vision.pub.pv_inventory where pv_inventory.compnum = 413";
 
            string connectionString = "DRIVER={Progress OpenEdge 11.7 Driver};HOST=ukdrdev2;PORT=15192;DB=vision;UID=sysprogress;PWD=sysprogress;DIL=0;AS=50;";
            // string connectionString = "DRIVER={DataDirect 7.1 OpenEdge Wire Protocol};HOST=ukdrdev2;PORT=15192;DB=vision;UID=sysprogress;PWD=sysprogress;DIL=0;AS=50;";
            // DRIVER ={ DataDirect 7.1 DB2 Wire Protocol}; IpAddress = 123.456.78.90;            PORT = 5179; DB = DB2DATA; UID = JOHN; PWD = XYZZY
            OdbcCommand command = new OdbcCommand(queryString);
            command.CommandType = System.Data.CommandType.Text;


            List<Inventory> inventorylist = new List<Inventory>();


            // this "using" will automatically close the connection
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                command.Connection = connection;
                connection.Open();

                // System.Data.CommandBehavior cmdBehaviour = System.Data.CommandBehavior.CloseConnection & System.Data.CommandBehavior.SequentialAccess ;
                OdbcDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection & System.Data.CommandBehavior.SequentialAccess);

                if (reader.HasRows)
                {
                    string fields = "";

                    for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
                    {
                        // You can acess full table metatdata
                        fields += "field:" + reader.GetName(ordinal) + "; type:" + reader.GetFieldType(ordinal) + "; /n";
                    }

                    // Populate the Model data   : 

                    while (reader.Read())
                    {
                        Inventory inventory1 = new Inventory();

                        inventory1.Id = reader.GetInt32(reader.GetOrdinal("TableRecId")); // get the colum number by name

                        inventory1.Compnum = reader.GetInt32(reader.GetOrdinal("CompNum")); // get the colum number by name

                        try
                        {
                            inventory1.Inventoryref = reader.GetString(reader.GetOrdinal("InventoryRef")); // get the colum number by name
                        }
                        catch
                        {
                            inventory1.Inventoryref = "";
                        }

                        inventory1.Whousecode = reader.GetString(reader.GetOrdinal("Whousecode")); // get the colum number by name

                        //inventory1.Inventoryqty = reader.GetDecimal(reader.GetOrdinal("Inventoryqty")); // get the colum number by name

                        try
                        {
                            inventory1.Inventoryqty = reader.GetDecimal(reader.GetOrdinal("Inventoryqty"));
                        } 
                        catch
                        {
                            inventory1.Inventoryqty = 0;
                        }



                        //if (reader.GetDecimal(reader.GetOrdinal("Inventoryqty")) isoftype DBNull)
                        //    inventory1.Inventoryqty = 0;
                        //else
                        //    inventory1.Inventoryqty = reader.GetDecimal(reader.GetOrdinal("Inventoryqty")); // get the colum number by name
                         

                        //comp.id = reader.GetInt32(reader.GetOrdinal("TableRecId")); // get the colum number by name

                        //// lots of ways to access fields
                        //comp.Name = reader.GetString(0);
                        //comp.Name = reader["CompName"].ToString();

                        //comp.CompanyNumber = reader.GetInt32(reader.GetOrdinal("CompNum"));
                        //comp.GroupCode = reader["GroupCode"].ToString();

                        inventorylist.Add(inventory1);
                    }
                }

                //This frees up the connection as required by the cmd Behaviour setting
                reader.Close();

                // The connection is automatically closed at 
                // the end of the Using block.
            }

            return inventorylist;
                

            //using (odbcconnection connection = new odbcconnection(connectionstring))
            //{
            //    //command.connection = connection;
            //    connection.open();
            //    //command.executenonquery();
            //    command.connection = connection;
            //    command.commandtext = querystring;              
            //    odbcdatareader dr = command.executereader();
            //    while(dr.read())
            //    {
            //        string s = dr.tostring();
            //        console.writeline(s);
            //    }

            //    // the connection is automatically closed at 
            //    // the end of the using block.
            //}
            //Warehouse.Models.Inventory inventory = new Warehouse.Models.Inventory();
            //inventory.compnum = 2;
            //inventory.inventoryqty = 100;
            //inventory.inventoryref = "ir0001";
            //inventory.whousecode = "wh1";
            //return inventory;
        }
    }
}
