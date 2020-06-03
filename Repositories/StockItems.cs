using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccess
{
    /*---------------------------
     * Function: Data access assigns values from the database to the variables in Domain
     * 
     * Comments: N/A
     * 
     * Created: 17/05/2020 \\AM
     * ---------------------------*/

    public class StockItems : Entity
    {
        public StockItem GetStockItem(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                StockItem stockItem = null;

                using (var command = new SqlCommand(("SELECT * FROM StockItems WHERE id = " + id), connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int StockId = reader.GetInt32(0);
                            string Name = reader.GetString(1);
                            decimal Price = reader.GetDecimal(2);
                            int InStock = reader.GetInt32(3);

                            stockItem = new StockItem(StockId, Name, Price, InStock);
                        }
                        reader.Close();
                    }
                }
                return stockItem;
            }
        }
        public bool UpdateStockItemAmount(OrderHeader order)
        {
            bool StockUpdatedCorrectly = false;
            return StockUpdatedCorrectly;
        }

        public IEnumerable<StockItem> GetStockItems()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var stockItems = new List<StockItem>();

                using (var command = new SqlCommand("SELECT * FROM StockItems", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int StockItemId = reader.GetInt32(0);
                            string Name = reader.GetString(1);
                            decimal Price = reader.GetDecimal(2);
                            int InStock = reader.GetInt32(3);

                            var stockItem = new StockItem(StockItemId, Name, Price, InStock);

                            stockItems.Add(stockItem);
                        }
                        reader.Close();
                    }
                    return stockItems;
                }
            }
        }

    }
}
