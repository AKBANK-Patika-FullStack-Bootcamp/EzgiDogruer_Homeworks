
using DryCleanerAPI.Model;
using Microsoft.AspNetCore.Mvc;

using System;
namespace DryCleanerAPI.Log
{
    public class Result
    {
        public int? Status { get; set; }

        public string? Message { get; set; }

        public List<Client>? Clients { get; set; }

        public List<Clothes>? Clothes { get; set; }
        public List<Order>? Order { get; set; }

    }
}
