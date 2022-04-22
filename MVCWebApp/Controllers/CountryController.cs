using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebApp.Data;
using MVCWebApp.Models;
using MVCWebApp.Models.City;
using MVCWebApp.Models.Country;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class CountryController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CombinedCountryViewModel model = new CombinedCountryViewModel();
            model.CountryList = _context.Countries.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CountryViewModel CreateViewModel)
        {
            if (ModelState.IsValid)
            {
                if(_context.Countries.Find(CreateViewModel.CountryName) == null)
                {
                    Country country = new Country();
                    country.CountryName = CreateViewModel.CountryName;

                    _context.Countries.Add(country);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //todo - improve in Identity assigment
                    MessageViewModel messageModel = new MessageViewModel();
                    messageModel.Message = "Country already exists!";

                    return View("MessageView", messageModel);
                }
            }

            CombinedCountryViewModel model = new CombinedCountryViewModel();
            model.CountryList = _context.Countries.ToList();

            return View(nameof(Index), model);
        }

        public IActionResult Delete(string id)
        {
            Country countryToDelete = _context.Countries.Find(id);

            if(countryToDelete != null)
            {
                foreach (City city in countryToDelete.Cities)
                {
                    foreach (Person person in city.People)
                    {
                        _context.People.Remove(person);
                    }

                    _context.Cities.Remove(city);
                }

                _context.Countries.Remove(countryToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Country country = _context.Countries.Find(id);

            if(country == null)
            {
                return NotFound();
            }
            else
            {
                EditCountryViewModel editCountryViewModel = new EditCountryViewModel { CountryName = country.CountryName };
                return View(editCountryViewModel);
            }
        }

        [HttpPost]
        public IActionResult Edit(string id, EditCountryViewModel editCountryViewModel)
        {
            //måste ha riktigt id på countries annars går det inte att ändra namnet..
            if (editCountryViewModel != null)
            {
                Country country = _context.Countries.Find(id);

                if (country != null)
                {
                    country.CountryName = editCountryViewModel.CountryName;

                    _context.Entry(country).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();          
        }
    }
}
