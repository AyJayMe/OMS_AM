using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Controllers
{
    /*---------------------------
     * Function: Data access assigns values from the database to the variables in Domain
     * 
     * Comments: N/A
     * 
     * Created: 17/05/2020 \\AM
     * ---------------------------*/

    public class StockController
    {
        //Create instance of repository to access.
        private readonly StockItems repository = new StockItems();


        //Instansiate instance of StockController
        private static StockController instance;

        //If no instance of StockItems exists, instantiate StockItems.
        public static StockController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StockController();
                };
                return instance;
            }
        }

        //Returns a list of all the stock items
        public IEnumerable<StockItem> GetStockItems()
        {
            return repository.GetStockItems();
        }

        //Gets a specific stock item by its id number
        public StockItem GetStockItem(int id)
        {
            return repository.GetStockItem(id);
        }

        //Updates the in stock amount when an order is placed
        public bool UpdateStockItemAmount(OrderHeader order)
        {
            return repository.UpdateStockItemAmount(order);
        }
    }
}
