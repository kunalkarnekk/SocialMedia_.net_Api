using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDev {get;set;}
        public string Industry { get; set; } = string.Empty;
        public long MarcketCap { get; set; }

        public List<Comments> Comments { get; set; } = new List<Comments>();
        public List<PortFolio> portFolio { get; set; } = new List<PortFolio>();
    }
}