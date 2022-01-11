using Microsoft.AspNetCore.Mvc;

namespace DryCleanerAPI.Model
{
    public class Client
    {
        public int  Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string BankAccount { get; set; }


    }
}
