using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person.ViewModels
{
    public class EditPersonViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"[\d]+\-?[\d]+",
            ErrorMessage = "Please enter a valid phone number! Only numbers and at most one '-'")]
        [StringLength(15, MinimumLength = 7)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A city must be selected!")]
        public int City { get; set; }
        public SelectList CityList { get; set; }
        
    }
}
