using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CityController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CombinedCityViewModel model = new CombinedCityViewModel();
            model.CityList = _context.Cities.OrderBy(c => c.Country).ToList();
            model.CountryList = new SelectList(_context.Countries, "CountryName", "CountryName");

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateCityViewModel CreateCityViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_context.Countries.Find(CreateCityViewModel.CountryName).Cities.Find(c => c.CityName == CreateCityViewModel.CityName) == null)
                {
                    City city = new City();
                    city.CityName = CreateCityViewModel.CityName;
                    Country country = _context.Countries.Find(CreateCityViewModel.CountryName);

                    city.Country = country;
                    country.Cities.Add(city);

                    _context.Update(country);
                    _context.Add(city);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //todo - improve in Identity assigment
                    MessageViewModel messageModel = new MessageViewModel();
                    messageModel.Message = "City already exists!";

                    return View("MessageView", messageModel);
                }
            }

            CombinedCityViewModel model = new CombinedCityViewModel();
            model.CityList = _context.Cities.OrderBy(c => c.Country).ToList();
            model.CountryList = new SelectList(_context.Countries, "CountryName", "CountryName");

            return View(nameof(Index), model);
        }

        public IActionResult Delete(int id)
        {
            City cityToDelete = _context.Cities.Find(id);

            if (cityToDelete != null)
            {
                    foreach (Person person in cityToDelete.People)
                    {
                        _context.People.Remove(person);
                    }
               
                _context.Cities.Remove(cityToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            City city = _context.Cities.Find(id);

            if (city == null)
            {
                return NotFound();
            }
            else
            {
                EditCityViewModel editCityViewModel = new EditCityViewModel { CityId = city.ID, CityName = city.CityName, CountryName = city.Country.CountryName, CountryList = new SelectList(_context.Countries, "CountryName", "CountryName", city.Country.CountryName) };
                return View(editCityViewModel);
            }
        }

        [HttpPost]
        public IActionResult Edit(EditCityViewModel editCityViewModel)
        {
            //måste ha riktigt id på countries annars går det inte att ändra namnet..
            if (editCityViewModel != null)
            {
                City city = _context.Cities.Find(editCityViewModel.CityId);

                if (city != null)
                {
                    city.CityName = editCityViewModel.CityName;
                    city.Country = _context.Countries.Find(editCityViewModel.CountryName);

                    _context.Entry(city).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
