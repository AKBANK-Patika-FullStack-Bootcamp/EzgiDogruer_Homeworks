using Microsoft.AspNetCore.Mvc;
using DryCleanerAPI.Model;
using DryCleanerAPI.Log;

namespace DryCleanerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        Result result = new Result();
        List<Order> Orders = new List<Order>();
        Logger logger = new Logger();
        Clothes clothes = new Clothes();    


        [HttpGet]
        public List<Order> GetOrders()
        {
            Orders.Add(new Order { ClientId = 1, ClothesId = 1, DateOfIssue = DateTime.Now.ToString("yyyyMMdd"), Id = 1, Number = 5,Price= 100 });
            return Orders;

        }


        [HttpGet("{OrderId}")]
        public Order GetOrder(int OrderId)
        {


            Order resultObj = new Order();
            resultObj = Orders.FirstOrDefault(x => x.Id == OrderId);

            return resultObj;
        }


        [Route("controller/PostOrder")]
        [HttpPost]
        public Result PostOrder(Order order)
        {


            // yeni eklenen listede var mı kontrolu 

            bool clothesCheck = Orders.Select(x => order.Id == x.Id).FirstOrDefault();
            if (!clothesCheck)
            {
                // yoksa ekle 
                Orders.Add(order);
                result.Status = 1;
                result.Message = "New Order is added!";
                
            }
            else
            {
                result.Status = 0;
                result.Message = "This clothes already in our list!";
            }
            return result;

        }


        [HttpPut("{OrderId}")]
        public Result UpdateOrder(int OrderId, Order newOrder)
        {

            Order? oldOrder = Orders.Find(x => x.Id == OrderId);

            if (oldOrder != null)
            {
                Orders.Remove(oldOrder);
                oldOrder = newOrder;
                Orders.Add(oldOrder);


                result.Status = 1;
                result.Message = "Update successful";
                result.Order = Orders;
            }
            else
            {
                result.Status = 0;
                result.Message = "Order not found";
            }

            return result;
        }



        [HttpDelete("{OrderId}")]
        public Result DeleteOrder(int OrderId)
        {
            try
            {


                Order? oldOrder = Orders.Find(x => x.Id == OrderId);
                if (oldOrder != null)
                {
                    Orders.Remove(oldOrder);
                    result.Status = 1;
                    result.Message = "Order deleted!";
                    result.Order = Orders;
                    logger.createLog(OrderId.ToString() + "  " + result.Message);
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Order  not found !";
                    result.Order = Orders;
                    logger.createLog(OrderId.ToString() + "  " + result.Message);
                }
            }
            catch (Exception ex)
            {
                logger.createLog(OrderId.ToString() + "  " + ex.StackTrace);

            }

            return result;
        }
    }
}