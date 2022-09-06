using e_commerce.Data;
using e_commerce.Data.Services;
using e_commerce.Data.Static;
using e_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _cinemasService;

        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }
        public async Task<IActionResult> Index()
        {


            var data = await _cinemasService.GetAll();
            return View(data);
        }
        //Get:Cinemas/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post : Cinemas/Create/Actor 
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Description,Name")]Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            await _cinemasService.Add(cinema);
            return RedirectToAction(nameof(Index));

        }

        //Get: Cinemas/Details/1
        public async Task<IActionResult> Details (int id )
        {
            var result = await _cinemasService.GetById(id);
            if (result == null) return View("NotFound");
            return View(result);
        }

        //Get : Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _cinemasService.GetById(id);
            if (result == null) return View("NotFound");
            return View(result);
        }

        //Post: Cinemas/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description,Logo")]Cinema cinema,int id)
        {
            if(!ModelState.IsValid)
            {
                View(cinema);
            }
            await _cinemasService.Update(id, cinema);
            return RedirectToAction(nameof(Index));
        }
        //Get : Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cinemasService.GetById(id);
            if (result == null) return View("NotFound");
            return View(result);
        }
        //Post : Cinemas/Delete/1
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var result = await _cinemasService.GetById(id);
            if (result == null) return View("NotFound");
            await _cinemasService.Delete(id);
            return RedirectToAction(nameof(Index));


        }
    }
}
