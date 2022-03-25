﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person
{
    public class Person
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual City.City City { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public virtual List<PersonLanguage> PersonLanguages { get; set; }

        public Person()
        {
            PersonLanguages = new List<PersonLanguage>();
        }
    }
}
