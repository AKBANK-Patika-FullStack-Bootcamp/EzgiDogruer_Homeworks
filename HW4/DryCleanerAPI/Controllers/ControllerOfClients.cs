using Microsoft.AspNetCore.Mvc;
using DAL.Model;
using DryCleanerAPI.Log;
using Entities;

namespace DryCleanerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerOfClients : ControllerBase
    {
        List<Client> clientList = new List<Client>();
        Result result = new Result();
        Logger logger = new Logger();
        //DatabaseContext dbOperation = new DatabaseContext();
        AdminOperations DbOperations = new AdminOperations();


        //////////// Admin  Operations For Clients  //////////////////////////////




        [HttpGet]
        public List<Client> GetClientList()
        {
            return DbOperations.GetClients();

        }


        [HttpGet("{ClientName}")]
        public Client GetClient(string ClientName)
        {
            return DbOperations.GetClient(ClientName);
        }


        
        [HttpPost]
        public Result AddClient(Client client)
        {
            if (DbOperations.GetClient(client.Username) == null)
            {
                if (DbOperations.AddClient(client))
                {
                    result.Status = 1;
                    result.Message = "New Client is added!";
                    result.Client = GetClient(client.Username);
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Error! "+ client.Username+" couldn't be added!";
                    DbOperations.Log("AddClient", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    result.Client = GetClient(client.Username);
                }
            }
            else
            {
                result.Status = 0;
                result.Message = "Warning! "+ client.Username+" already exists";
                DbOperations.Log("AddClient", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                result.Client = GetClient(client.Username);
            }
            return result;


        }




        [HttpPut]
        public Result Update(string clientName, Client newClient)
        {
            Client? oldClient = DbOperations.GetClient(newClient.Username);
            if (oldClient != null)
            {
                if (DbOperations.UpdateClient(newClient))
                {
                    result.Status = 1;
                    result.Message = "Client is updated!";
                    result.Client = GetClient(newClient.Username);
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Error! "+ newClient.Username+" couldn't be updated!";
                    DbOperations.Log("Update", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    result.Client = GetClient(clientName);
                }
            }
            else
            {
                result.Status = 0;
                result.Message = "Warning! "+ newClient.Username + " doesn't exist!";
                DbOperations.Log("Update", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                result.Client = GetClient(clientName);
            }
            return result;
        }


       
        [HttpDelete("{clientName}")]
        public Result Delete(string clientName)
        {
            if (DbOperations.DeleteClient(clientName))
            {
                result.Status = 1;
                result.Message = "Client  deleted!";
                logger.createLog(result.Message);
                result.Clients = GetClientList();
            }
            else
            {
                result.Status = 0;
                result.Message = clientName +" is not found or already deleted!";
                DbOperations.Log("Delete", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                result.Clients = GetClientList();
            }
            return result;
        }


    }



}