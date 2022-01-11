using Microsoft.AspNetCore.Mvc;
using DryCleanerAPI.Model;
using DryCleanerAPI.Log;

namespace DryCleanerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminControllerForClients : ControllerBase
    {
        List<Client> clientList = new List<Client>();
        Result result = new Result();
        Logger logger = new Logger();
       


        //////////// Admin  Operations For Clients  //////////////////////////////

        [Route("controller/AddClient")]
        [HttpPost]
        public List<Client> AddClient()
        {
            clientList.Add(new Client { Id = 1, Name = "Mustafa Yılmaz", Address = "Üsküdar İstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });
            clientList.Add(new Client { Id = 2, Name = "Burçin Öztürk", Address = "Ümraniye İstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });
            clientList.Add(new Client { Id = 3, Name = "İsra Nur Alperen", Address = "Esenyurt  İstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });
            clientList.Add(new Client { Id = 4, Name = "Elif Gökpınar", Address = "Ataşehir İstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });
            clientList.Add(new Client { Id = 5, Name = "Berra Mercan", Address = "Beykoz İstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });

            return clientList;


        }



        [HttpGet]
        public List<Client> GetClientList()
        {
            clientList = AddClient();
            return clientList;

        }



        [HttpGet("{ClientId}")]
        public Client GetClient(int ClientId)
        {
            clientList = AddClient();

            Client resultObj = new Client();
            resultObj = clientList.FirstOrDefault(x => x.Id == ClientId);

            return resultObj;
        }


        [Route("controller/PostClient")]
        [HttpPost]
        public Result PostClient(Client client)
        {

            //liste doluyor
            clientList = AddClient();

            // yeni eklenen listede var mı kontrolu 

            bool clientCheck = clientList.Select(x => client.Id == x.Id || client.Name == x.Name).FirstOrDefault();
            if (!clientCheck)
            {
                // yoksa ekle 
                clientList.Add(client);
                result.Status = 1;
                result.Message = "New Client is added!";
                logger.createLog( result.Message);
            }
            else
            {
                result.Status = 0;
                result.Message = "This client already in our clients!";
                logger.createLog( result.Message);
            }
            return result;

        }



        [HttpPut("{clientId}")]
        public Result Update(int clientId, Client newClient)
        {
            clientList = AddClient();

            Client? oldClient = clientList.Find(x => x.Id == clientId);

            if (oldClient != null)
            {
                clientList.Remove(oldClient);
                oldClient = newClient;
                clientList.Add(oldClient);


                result.Status = 1;
                result.Message = "Update successful";
                result.Clients = clientList;
                logger.createLog(clientId.ToString() + "  " + result.Message);
            }
            else
            {
                result.Status = 0;
                result.Message = "Client not found";
                logger.createLog(clientId.ToString() + "  " + result.Message);
            }

            return result;
        }



        [HttpDelete("{clientId}")]
        public Result Delete(int clientId)
        {
            try
            {
                clientList = AddClient();

                Client? oldClient = clientList.Find(x => x.Id == clientId);
                if (oldClient != null)
                {
                    clientList.Remove(oldClient);
                    result.Status = 1;
                    result.Message = "Client deleted!";
                    result.Clients = clientList;
                    logger.createLog(clientId.ToString() + "  " + result.Message);

                }
                else
                {
                    result.Status = 0;
                    result.Message = "Client  not found !";
                    result.Clients = clientList;
                    logger.createLog(clientId.ToString() + "  " + result.Message);
                }
            }
            catch (Exception ex)
            {
                logger.createLog(clientId.ToString() + "  " + ex.StackTrace);

            }

            return result;
        }


    }
}