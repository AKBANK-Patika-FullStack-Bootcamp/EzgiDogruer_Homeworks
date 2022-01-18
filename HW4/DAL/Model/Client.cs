using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Client
    {
        [Key]
        //public int  UserId { get; set; }

        public string Username { get; set; }

        public string Address { get; set; }

        public string BankAccount { get; set; }

        public int? OrderId { get; set; }
    }
}
