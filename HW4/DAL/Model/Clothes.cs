
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Clothes
    {
        [Key]
        //public int? Id { get; set; }


        public string ClothesName { get; set; }

        public string Material { get; set; }

        public int Price { get; set; }

        


    }
}
