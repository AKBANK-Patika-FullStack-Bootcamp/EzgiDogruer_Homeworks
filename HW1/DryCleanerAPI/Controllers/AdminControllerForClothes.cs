using Microsoft.AspNetCore.Mvc;
using DryCleanerAPI.Model;
using DryCleanerAPI.Log;

namespace DryCleanerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminControllerForClothes : ControllerBase
    {
       
        Result result = new Result();
        Logger logger = new Logger();
        List<Clothes> ClothesList = new List<Clothes>();

       
        ////////////////// Admin  Operations For Clothes  //////////////////////////////

        [Route("controller/AddClothes")]
        [HttpPost]
        public List<Clothes> AddClothes()
        {
            ClothesList.Add(new Clothes { Id = 1, Name = "Jean", Material = "Jean" ,Price=50});
            ClothesList.Add(new Clothes { Id = 2, Name = "Skirt", Material = "leather" , Price = 40 });
            ClothesList.Add(new Clothes { Id = 3, Name = "Scarf", Material = "Silk" , Price = 30 });
            ClothesList.Add(new Clothes { Id = 4, Name = "T-shirt", Material = "Cotton", Price = 30 });
            ClothesList.Add(new Clothes { Id = 5, Name = "Sweater", Material = "wool" , Price = 60 });

            return ClothesList;

        }


      
        [HttpGet]
        public List<Clothes> GetClothesList()
        {
            ClothesList = AddClothes().OrderBy(x => x.Id).ToList();
            return ClothesList;

        }


        
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

            // yeni eklenen listede var mı kontrolu 

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