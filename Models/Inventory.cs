using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Compnum { get; set; }
        public string Inventoryref { get; set; }
        public string Whousecode { get; set; }
        public decimal Inventoryqty { get; set; }
    }
}
