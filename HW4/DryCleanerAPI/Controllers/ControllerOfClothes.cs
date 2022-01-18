using Microsoft.AspNetCore.Mvc;
using DryCleanerAPI.Log;
using DAL.Model;

namespace DryCleanerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerOfClothes : ControllerBase
    {
       
        Result result = new Result();
        Logger logger = new Logger();
       
        AdminOperations DbOperations = new AdminOperations();

        //////////// Admin  Operations For Clothess  //////////////////////////////




        [HttpGet]
        public List<Clothes> GetClothesList()
        {
            return DbOperations.GetClothesList();

        }


        [HttpGet("{ClothesName}")]
        public Clothes GetClothes(string ClothesName)
        {
            return DbOperations.GetClothes(ClothesName);
        }



        [HttpPost]
        public Result AddClothes(Clothes Clothes)
        {
            if (DbOperations.GetClothes(Clothes.ClothesName) == null)
            {
                if (DbOperations.AddClothes(Clothes))
                {
                    result.Status = 1;
                    result.Message = "New Clothes is added!";
                    result.Clothes = GetClothes(Clothes.ClothesName);
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Error!"+ Clothes .ClothesName+ " couldn't be added!";
                    DbOperations.Log("AddClothes", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    result.Clothes = GetClothes(Clothes.ClothesName);
                }
            }
            else
            {
                result.Status = 0;
                result.Message = "Warning! "+ Clothes.ClothesName + " already exists";
                DbOperations.Log("AddClothes", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                result.Clothes = GetClothes(Clothes.ClothesName);
            }
            return result;


        }




        [HttpPut]
        public Result Update(string ClothesName, Clothes newClothes)
        {
            Clothes? oldClothes = DbOperations.GetClothes(newClothes.ClothesName);
            if (oldClothes != null)
            {
                if (DbOperations.UpdateClothes(newClothes))
                {
                    result.Status = 1;
                    result.Message = "Clothes is updated!";
                    result.Clothes = GetClothes(newClothes.ClothesName);
                }
                else
                {
                    result.Status = 0;
                    result.Message = "Error!" + newClothes+" couldn't be updated!";
                    DbOperations.Log("Update", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                    result.Clothes = GetClothes(ClothesName);
                }
            }
            else
            {
                result.Status = 0;
                result.Message = "Warning!"+ newClothes +" doesn't exist!";
                DbOperations.Log("Update", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                result.Clothes = GetClothes(ClothesName);
            }
            return result;
        }



        [HttpDelete("{ClothesName}")]
        public Result Delete(string ClothesName)
        {
            if (DbOperations.DeleteClothes(ClothesName))
            {
                result.Status = 1;
                result.Message = "Clothes  deleted!";
                logger.createLog(result.Message);
                result.ClothesList = GetClothesList();
            }
            else
            {
                result.Status = 0;
                result.Message = ClothesName + " is  not found or already deleted!";
                DbOperations.Log("Delete", result.Message, DateTime.Now.ToString("yyyyMMdd"));
                result.ClothesList = GetClothesList();
            }
            return result;
        }


    }
}