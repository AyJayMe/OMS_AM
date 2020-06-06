using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataAccess;

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
        public void DeleteOrderHeaderAndOrderItems(OrderHeader orderHeader)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_DeleteOrderHeaderandOrderItems @orderHeaderId", connection))
                {
                    command.Parameters.Add(new SqlParameter("orderHeaderId", orderHeader.Id));
                    command.ExecuteNonQuery();
                }
            }
            orderHeader = null;
        }

        public void DeleteOrderItem(OrderHeader orderHeader, int stockItemId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_DeleteOrderItem @orderHeaderId, @stockItemId", connection))
                {
                    command.Parameters.Add(new SqlParameter("orderHeaderId", orderHeader.Id));
                    command.Parameters.Add(new SqlParameter("stockItemId", stockItemId));
                    command.ExecuteNonQuery();
                }
            }
            orderHeader.RemoveOrderItem(stockItemId);
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

                            var orderItems = new ObservableCollection<OrderItem>();

                            var orderHeader = new OrderHeader(Id, OrderDate, OrderState);

                            //If order State = 1, delete the order and its items as it was never processed.
                            if (OrderState == 1)
                            {
                                DeleteOrderHeaderAndOrderItems(orderHeader);
                            }
                            else
                            {
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

                                            orderHeader.AddOrderItem(orderItem);
                                        }
                                        oiReader.Close();
                                    }
                                }
                            }
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
                int orderHeaderId = 0;
                using (var command = new SqlCommand("sp_InsertOrderHeader", connection))
                {
                    orderHeaderId = Convert.ToInt32(command.ExecuteScalar());
                }

                using (var command = new SqlCommand("sp_SelectOrderHeaderById @id", connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", orderHeaderId));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = reader.GetInt32(0);
                            int StateId = reader.GetInt32(1);
                            DateTime OrderDate = reader.GetDateTime(2);

                            o = new OrderHeader(Id, OrderDate, StateId);
                        }
                        reader.Close();
                    }
                }
                return o;
            }
        }

        public void ProcessOrder(OrderHeader orderHeader)
        {
            
        }

        public void UpsertOrderItem(OrderHeader orderHeader, OrderItem orderItem)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_UpsertOrderItem @orderHeaderId, @stockItemId, @description, @price, @quantity", connection))
                {
                    command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeader.Id));
                    command.Parameters.Add(new SqlParameter("@stockItemId", orderItem.StockItemId));
                    command.Parameters.Add(new SqlParameter("@description", orderItem.Description));
                    command.Parameters.Add(new SqlParameter("@price", orderItem.Price));
                    command.Parameters.Add(new SqlParameter("@quantity", orderItem.Quantity));

                    try
                    {
                        command.ExecuteNonQuery();
                        orderHeader.AddOrderItem(orderItem);
                    }
                    catch(Exception ex)
                    {
                        throw new Exception($"Error upserting item. {ex}");
                    }
                }
            }
        }
    

        public void UpdateOrderState(OrderHeader orderHeader, int stateId)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_UpdateOrderState @orderHeaderId, @stateId", connection))
                {
                    command.Parameters.Add(new SqlParameter("@orderHeaderId", orderHeader.Id));
                    command.Parameters.Add(new SqlParameter("@stateId", stateId));

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error updating order state. {ex}");
                    }
                }
                orderHeader.setState(stateId);
            }
        }
    }
}
