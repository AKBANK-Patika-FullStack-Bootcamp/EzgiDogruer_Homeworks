namespace DryCleanerAPI.Model
{
    public class Order
    {

        public int Id { get; set; }

        public int  ClientId { get; set; }

        public int  ClothesId { get; set; }

        public int  Number { get; set; }

        public string DateOfIssue { get; set; }

        public double Price   { get; set; }
    }
}
