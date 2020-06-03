using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class StockItem
    {
        public int StockId { get; set; }
        public int InStock { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public StockItem(int stockId, string name, decimal price, int inStock)
        {
            StockId = stockId;
            Name = name;
            Price = price;
            InStock = inStock;
        }

        public StockItem()
        {
        }
    }
}
