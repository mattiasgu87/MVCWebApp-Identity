using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.City
{
    public class EditCityViewModel
    {
        public int CityId { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        [Display(Name = "City Name")]
        public string CityName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A country must be selected!")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public SelectList CountryList { get; set; }
    }
}
