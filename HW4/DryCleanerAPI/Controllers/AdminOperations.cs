
using DAL.Model;
using DryCleanerAPI.Log;
using Entities;

namespace DryCleanerAPI.Controllers
{
    public class AdminOperations
    {
        Result result = new Result();
        Logger logger = new Logger();
        DBLog log = new DBLog();
        private DatabaseContext context = new DatabaseContext();

        
        
        public void Log(String MethodName, String message,String date)
        {
            log.Message = message;
            log.MethodName = MethodName;
            log.Date = date;

            context.Log.Add(log);
            context.SaveChanges();
        }


        ///////////////////////////////////////////////  OPERATIONS OF CLIENTS  ////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets the client list for the admin
        /// </summary>
        /// <returns></returns>
        public List<Client> GetClients()
        {
            try
            {
                var clientList = context.Client.ToList(); ;
                if (clientList != null)
                {
                    return clientList;
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Client list is empty!";
                    logger.createLog(result.Message);
                    Log("GetClients", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    return null;
                }

            }
            catch(Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("GetClients", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return null;
            }
           
        }


        /// <summary>
        /// Returns the user matching the requested name
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Client GetClient( String name)
        {
            
            try
            {
                var client= context.Client.FirstOrDefault(p => p.Username == name);
                if (client != null)
                {
                    return client;
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Client not found!";
                    logger.createLog(name.ToString() + "  " + result.Message);
                    Log("GetClient", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    return null;
                }
                
            }catch(Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("GetClient", result.Message, DateTime.Now.ToString("yyyy-MMd-d"));
                return null;
            }

        }


        /// <summary>
        /// Adds new customers
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        public bool AddClient(Client Client)
        {
            try
            {
                context.Client.Add(Client);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("AddClient", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }


        /// <summary>
        /// Updates customer information
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool UpdateClient(Client client)
        {
            try
            {
                Client exist = context.Client.Find(client.Username);
                context.Entry(exist).CurrentValues.SetValues(client);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("UpdateClient", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }


        /// <summary>
        /// Deletes the requested customer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteClient(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name) && GetClient(name) != null)
                {
                    context.Client.Remove(GetClient(name));
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Client  not found !";
                    logger.createLog( result.Message);
                    Log("DeleteClient", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    return false;
                }

            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("DeleteClient", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }



        ///////////////////////////////////////////////  OPERATIONS OF CLOTHES  ////////////////////////////////////////////////////////////


        /// <summary>
        /// Gets the clothes list for the admin
        /// </summary>
        /// <returns></returns>
        public List<Clothes> GetClothesList()
        {
            try
            {
                var clothesList = context.Clothes.ToList(); ;
                if (clothesList != null)
                {
                    return clothesList;
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Clothes list is empty!";
                    logger.createLog(result.Message);
                    Log("GetClothesList", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    return null;
                }

            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("GetClothesList", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return null;
            }

        }


        /// <summary>
        /// Returns the clothes matching the requested name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Clothes GetClothes(String name)
        {

            try
            {
                var clothes = context.Clothes.FirstOrDefault(p => p.ClothesName == name);
                if (clothes != null)
                {
                    return clothes;
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Clothes not found!";
                    logger.createLog(name.ToString() + "  " + result.Message);
                    Log("GetClothes", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    return null;
                }

            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("GetClothes", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return null;
            }

        }


        /// <summary>
        /// Adds new clothes
        /// </summary>
        /// <param name="Clothes"></param>
        /// <returns></returns>
        public bool AddClothes(Clothes clothes)
        {
            try
            {
                context.Clothes.Add(clothes);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("AddClothes", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }


        /// <summary>
        /// Updates clothes information
        /// </summary>
        /// <param name="clothes"></param>
        /// <returns></returns>
        public bool UpdateClothes(Clothes clothes)
        {
            try
            {
                Clothes exist = context.Clothes.Find(clothes.ClothesName);
                context.Entry(exist).CurrentValues.SetValues(clothes);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("UpdateClothes", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }


        /// <summary>
        /// Deletes the requested clothes
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteClothes(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name) && GetClothes(name) != null)
                {
                    context.Clothes.Remove(GetClothes(name));
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Clothes  not found !";
                    logger.createLog(result.Message);
                    Log("DeleteClothes", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    return false;
                }

            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("DeleteClothes", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }


        ///////////////////////////////////////////////  OPERATIONS OF ORDERS  ////////////////////////////////////////////////////////////


        /// <summary>
        /// Gets the orders for the admin
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrderList()
        {
            try
            {
                var orderList = context.Order.ToList(); ;
                if (orderList != null)
                {
                    return orderList;
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Sorry, There is no order!";
                    logger.createLog(result.Message);
                    Log("GetOrderList", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    return null;
                }

            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("GetOrderList", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return null;
            }

        }


        /// <summary>
        /// Returns the order matching the requested name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public List<Order> GetOrder(string name)
        {

            try
            {
                var order = context.Order.Where(p=> p.ClothesName== name).ToList();
                if (order != null)
                {
                    return order;
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Order is not found!";
                    logger.createLog(result.Message);
                    Log("GetOrder", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    return null;
                }

            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Message = ex.ToString();
                logger.createLog(result.Message);
                Log("GetOrder", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                return null;
            }

        }

    }
}
