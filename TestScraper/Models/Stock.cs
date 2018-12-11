using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TestScraper.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string Symbol { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime MarketTime { get; set; }
    }
}
