using Microsoft.AspNetCore.Mvc;
using DryCleanerAPI.Model;
using DryCleanerAPI.Log;

namespace DryCleanerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        List<Client> clientList = new List<Client>();
        Result result = new Result();
        Logger logger = new Logger();
        List<Clothes> ClothesList = new List<Clothes>();
        //////////// Admin  Operations For Clients  //////////////////////////////

        [Route("controller/AddClient")]
        [HttpPost]
        public List<Client> AddClient()
        {
            clientList.Add(new Client { Id = 1, Name = "Mustafa Yýlmaz", Address = "Üsküdar Ýstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });
            clientList.Add(new Client { Id = 2, Name = "Burçin Öztürk", Address = "Ümraniye Ýstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });
            clientList.Add(new Client { Id = 3, Name = "Ýsra Nur Alperen", Address = "Esenyurt  Ýstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });
            clientList.Add(new Client { Id = 4, Name = "Elif Gökpýnar", Address = "Ataþehir Ýstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });
            clientList.Add(new Client { Id = 5, Name = "Berra Mercan", Address = "Beykoz Ýstanbul", BankAccount = "TR33 0006 1005 1978 6457 8413 26 " });

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

            // yeni eklenen listede var mý kontrolu 

            bool clientCheck = clientList.Select(x => client.Id == x.Id || client.Name == x.Name).FirstOrDefault();
            if (!clientCheck)
            {
                // yoksa ekle 
                clientList.Add(client);
                result.Status = 1;
                result.Message = "New Client is added!";
            }
            else
            {
                result.Status = 0;
                result.Message = "This client already in our clients!";
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
            }
            else
            {
                result.Status = 0;
                result.Message = "Client not found";
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


        ////////////////// Admin  Operations For Clothes  //////////////////////////////

        [Route("controller/AddClothes")]
        [HttpPost]
        public List<Clothes> AddClothes()
        {
            ClothesList.Add(new Clothes { Id = 1, Name = "Jean", Material = "Jean" });
            ClothesList.Add(new Clothes { Id = 2, Name = "Skirt", Material = "leather" });
            ClothesList.Add(new Clothes { Id = 3, Name = "Scarf", Material = "Silk" });
            ClothesList.Add(new Clothes { Id = 4, Name = "T-shirt", Material = "Cotton" });
            ClothesList.Add(new Clothes { Id = 5, Name = "Sweater", Material = "wool" });

            return ClothesList;

        }


        [Route("controller/GetClothesList")]
        [HttpGet]
        public List<Clothes> GetClothesList()
        {
            ClothesList = AddClothes().OrderBy(x => x.Id).ToList();
            return ClothesList;

        }


        //[Route("controller/GetClothes")]
        [HttpGet("{ClothesId}")]
        public Clothes GetClothes(int ClothesId)
        {
            ClothesList = AddClothes();

            Clothes resultObj = new Clothes();
            resultObj = ClothesList.FirstOrDefault(x => x.Id == ClothesId);

            return resultObj;
        }


        [Route("controller/PostClothes")]
        [HttpPost]
        public Result PostClothes(Clothes clothes)
        {

            //liste doluyor
            ClothesList = AddClothes();

            // yeni eklenen listede var mý kontrolu 

            bool clothesCheck = ClothesList.Select(x => clothes.Id == x.Id || clothes.Name == x.Name).FirstOrDefault();
            if (!clothesCheck)
            {
                // yoksa ekle 
                ClothesList.Add(clothes);
                result.Status = 1;
                result.Message = "New Clothes is added!";
            }
            else
            {
                result.Status = 0;
                result.Message = "This clothes already in our list!";
            }
            return result;

        }



        [HttpPut("{ClothesId}")]
        public Result UpdateClothes(int ClothesId, Clothes newClothes)
        {
            ClothesList = AddClothes();

            Clothes? oldClothes = ClothesList.Find(x => x.Id == ClothesId);

            if (oldClothes != null)
            {
                ClothesList.Remove(oldClothes);
                oldClothes = newClothes;
                ClothesList.Add(oldClothes);


                result.Status = 1;
                result.Message = "Update successful";
                result.Clothes = ClothesList;
            }
            else
            {
                result.Status = 0;
                result.Message = "Clothes not found";
            }

            return result;
        }



        [HttpDelete("{ClothesId}")]
        public Result DeleteClothes(int ClothesId)
        {
            try
            {
                ClothesList = AddClothes();

                Clothes? oldClothes = ClothesList.Find(x => x.Id == ClothesId);
                if (oldClothes != null)
                {
                    ClothesList.Remove(oldClothes);
                    result.Status = 1;
                    result.Message = "Clothes deleted!";
                    result.Clothes = ClothesList;
                    logger.createLog(ClothesId.ToString() + "  " + result.Message);
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Clothes  not found !";
                    result.Clothes = ClothesList;
                    logger.createLog(ClothesId.ToString() + "  " + result.Message);
                }
            }
            catch (Exception ex)
            {


            }

            return result;
        }
    }
}