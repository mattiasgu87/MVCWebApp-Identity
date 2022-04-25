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
            model.CountryList = new SelectList(_context.Countries, "CountryId", "CountryName");

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateCityViewModel CreateCityViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_context.Countries.Find(CreateCityViewModel.CountryId).Cities.Find(c => c.CityName == CreateCityViewModel.CityName) == null)
                {
                    City city = new City();
                    city.CityName = CreateCityViewModel.CityName;
                    Country country = _context.Countries.Find(CreateCityViewModel.CountryId);

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
                EditCityViewModel editCityViewModel = new EditCityViewModel { CityId = city.ID, CityName = city.CityName, CountryId = city.Country.CountryId, CountryList = new SelectList(_context.Countries, "CountryId", "CountryName", city.Country.CountryId) };
                return View(editCityViewModel);
            }
        }

        [HttpPost]
        public IActionResult Edit(EditCityViewModel editCityViewModel)
        {
            if (editCityViewModel != null)
            {
                if (ModelState.IsValid)
                {

                    City city = _context.Cities.Find(editCityViewModel.CityId);

                    if (city != null)
                    {
                        city.CityName = editCityViewModel.CityName;
                        city.Country = _context.Countries.Find(editCityViewModel.CountryId);

                        _context.Entry(city).State = EntityState.Modified;
                        _context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
                else
                    return View(editCityViewModel);
            }
            return NotFound();
        }
    }
}
