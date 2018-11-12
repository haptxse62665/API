using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class CountryViewModel
    {

        public string ImageURL { get; set; }
        public int NumberOfStudent { get; set; }
        public string CountryName { get; set; }
        public int ID { get; set; }
        public string NumberArrivalPerTotal { get; set; }
    }
}