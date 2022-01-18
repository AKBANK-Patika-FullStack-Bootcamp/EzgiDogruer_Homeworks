using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Order
    {
        [Key]
        public int? Id { get; set; }

        public int ClientId { get; set; }

        public string  ClientName { get; set; }

        public int ClothesId { get; set; }

        public string ClothesName { get; set; }

        public int  NumberOfClothes { get; set; }

        public DateTime? DateOfRented { get; set; }

        public int? TotalPayment   { get; set; }
    }
}
