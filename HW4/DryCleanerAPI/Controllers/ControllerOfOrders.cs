using Microsoft.AspNetCore.Mvc;
using DAL.Model;
using DryCleanerAPI.Log;
using Entities;

namespace DryCleanerAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ControllerOfOrders : ControllerBase
    {
       
        AdminOperations DbOperations = new AdminOperations();

        [HttpGet]
        public List<Order> GetOrderList()
        {
            return DbOperations.GetOrderList();

        }


        [HttpGet("{ClothesName}")]
        public List<Order> GetOrder(string ClothesName)
        {
            return DbOperations.GetOrder(ClothesName);
        }
    }
}
