using AspNet_MVC_App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using AspNet_MVC_App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNet_MVC_App.Models.ViewModels;

namespace AspNet_MVC_App.Controllers
{
    public class MusicController : Controller
    {
        private SalonDbContext _context;

        public MusicController(SalonDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Musics.Include(nameof(Music.Genre)));
        }
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> categories = _context.Categories.Select(i => new SelectListItem()
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            // ViewData
            //ViewData["CategoryList"] = categories;

            // ViewBag
            //ViewBag.CategoryList = categories;

            MusicVM viewModel = new MusicVM()
            {
                Genres = _context.Genres.Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(viewModel);
        }

        public IActionResult Upsert()
        {
            MusicVM viewModel = new MusicVM()
            {
                Genres = _context.Genres.Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                //Music = _context.Musics.First((el)=>el.);
            };

            return View(viewModel);
        }

        //public IActionResult Upsert()
        //{
        //    //IEnumerable<SelectListItem> categories = _context.Categories.Select(i => new SelectListItem()
        //    //{
        //    //    Text = i.Name,
        //    //    Value = i.Id.ToString()
        //    //});

        //    // ViewData
        //    //ViewData["CategoryList"] = categories;

        //    // ViewBag
        //    //ViewBag.CategoryList = categories;

        //    MusicVM viewModel = new MusicVM()
        //    {
        //        Genres = _context.Genres.Select(i => new SelectListItem()
        //        {
        //            Text = i.Name,
        //            Value = i.Id.ToString()
        //        })
        //    };

        //    return View(viewModel);
        //}

        [HttpPost]
        public IActionResult Upsert(MusicVM model)
        {
            if (!ModelState.IsValid) return NotFound();

            if (model.Music.Id == 0)
            {
                _context.Musics.Add(model.Music);
                _context.SaveChanges();
            }
            else
            {
                _context.Musics.Update(model.Music);
                _context.SaveChanges();
            }
            

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Create(MusicVM model)
        {
            if (!ModelState.IsValid) return View();

            _context.Musics.Add(model.Music);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            var carToRemove = _context.Musics.Find(id);

            if (carToRemove == null) return NotFound();

            _context.Musics.Remove(carToRemove);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            var car = _context.Musics.Find(id);

            if (car == null) return NotFound();

            IEnumerable<SelectListItem> categories = _context.Genres.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.CategoryList = categories;

            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(Music obj)
        {
            if (!ModelState.IsValid) return View();

            _context.Musics.Update(obj);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
