
using DAL.Model;
using Microsoft.AspNetCore.Mvc;

using System;
namespace DryCleanerAPI.Log
{
    public class Result
    {
        public int? Status { get; set; }

        public string? Message { get; set; }

        public List<Client>? Clients { get; set; }

        public List<Clothes>? ClothesList { get; set; }
        public List<Order>? Orders { get; set; }

        public Client? Client { get; set; }

        public Clothes? Clothes { get; set; }

    }
}
