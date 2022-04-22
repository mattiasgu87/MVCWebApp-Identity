using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.City
{
    public class EditCityViewModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public SelectList CountryList { get; set; }
    }
}
