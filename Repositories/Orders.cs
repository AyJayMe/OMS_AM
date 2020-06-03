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
     * Function: Data access runs defined functions against the database
     * 
     * Comments: 
     * 
     * Created: 17/05/2020 \\AM
     * 
     * Updated: 23/05/2020 \\AM
     * ---------------------------*/
    public class Orders : Entity
    {
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_DeleteOrderHeaderandOrderItems @orderHeaderId", connection))
                {
                    command.Parameters.Add(new SqlParameter("orderHeaderId", orderHeaderId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_DeleteOrderItem @orderHeaderId @stockItemId", connection))
                {
                    command.Parameters.Add(new SqlParameter("orderHeaderId", orderHeaderId));
                    command.Parameters.Add(new SqlParameter("stockItemIdHeaderId", stockItemId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<OrderHeader> GetOrderHeaders()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var orderHeaders = new List<OrderHeader>();
                //Instiate orderHeader(oh) command object
                using (var ohCommand = new SqlCommand("sp_SelectOrderHeaders", connection))
                {
                    using (var ohReader = ohCommand.ExecuteReader())
                    {
                        while (ohReader.Read())
                        {
                            
                            int Id = ohReader.GetInt32(0);
                            int OrderState = ohReader.GetInt32(1);
                            DateTime OrderDate = ohReader.GetDateTime(2);

                            var orderItems = new List<OrderItem>();

                            //Instatiate orderItem(oi) command object
                            using (var oiCommand = new SqlCommand("SELECT * FROM OrderItems WHERE @OrderHeaderId = OrderHeaderId", connection))
                            {
                                oiCommand.Parameters.Add(new SqlParameter("@orderHeaderId", Id));
                                using (var oiReader = oiCommand.ExecuteReader())
                                {
                                    while (oiReader.Read())
                                    {
                                        int OrderHeaderId = oiReader.GetInt32(0);
                                        int StockItemId = oiReader.GetInt32(1);
                                        string Description = oiReader.GetString(2);
                                        decimal Price = oiReader.GetDecimal(3);
                                        int Quantity = oiReader.GetInt32(4);

                                        OrderItem orderItem = new OrderItem(OrderHeaderId, StockItemId, Description, Quantity, Price);
                                        orderItems.Add(orderItem);
                                    }
                                    oiReader.Close();
                                }
                            }
                            var orderHeader = new OrderHeader(Id, OrderState, OrderDate, orderItems);

                            orderHeaders.Add(orderHeader);
                        }
                        ohReader.Close();
                    }
                    return orderHeaders;
                }
            }
        }

        public OrderHeader InsertOrderHeader()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                OrderHeader o = new OrderHeader();
                int id = 0;
                using (var command = new SqlCommand("sp_InsertOrderHeader", connection))
                {
                    id = Convert.ToInt32(command.ExecuteScalar());
                }

                using (var command = new SqlCommand("sp_SelectOrderHeaderById @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int OrderHeaderId = reader.GetInt32(0);
                            DateTime OrderDate = reader.GetDateTime(2);

                            o = new OrderHeader(OrderHeaderId, OrderDate);
                        }
                        reader.Close();
                    }
                }
                return o;
            }
        }

        public void UpsertOrderItem(int orderHeaderId, StockItem stockItem, int quantity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();


                using (var command = new SqlCommand("sp_UpsertOrderItem @orderHeaderId, @stockItemId, @description, @price, @quantity", connection))
                {
                    command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeaderId));
                    command.Parameters.Add(new SqlParameter("@stockItemId", stockItem.StockId));
                    command.Parameters.Add(new SqlParameter("@description", stockItem.Name));
                    command.Parameters.Add(new SqlParameter("@price", stockItem.Price));
                    command.Parameters.Add(new SqlParameter("@quantity", quantity));

                    int success = command.ExecuteNonQuery();
                }
            }
        }
    

        public OrderHeader UpdateOrderState(int orderHeaderId, int stateId)
        {
            OrderHeader o = new OrderHeader();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_UpdateOrderState @orderHeaderId @stateId", connection))
                {
                    command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeaderId));
                    command.Parameters.Add(new SqlParameter("@stateId", stateId));

                    command.ExecuteNonQuery();

                }

                using (var command = new SqlCommand("sp_SelectOrderHeaderById @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", orderHeaderId));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int OrderHeaderId = reader.GetInt32(0);
                            DateTime OrderDate = reader.GetDateTime(2);

                            o = new OrderHeader(OrderHeaderId, OrderDate);
                        }
                        reader.Close();
                    }
                }
                return o;
            }

        }
    }
}
