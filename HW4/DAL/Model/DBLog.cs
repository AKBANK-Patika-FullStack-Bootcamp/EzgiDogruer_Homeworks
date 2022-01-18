using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class DBLog
    {
        [Key]
       // public int? Id { get; set; }
        public String MethodName { get; set; }
        public String Date { get; set; }

        public String? Message { get; set; }
    }
}
