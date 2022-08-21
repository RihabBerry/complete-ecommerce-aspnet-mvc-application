using e_commerce.Data;
using e_commerce.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class MoviesController : Controller
    {

        private readonly IMoviesService _movieService;

        public MoviesController(IMoviesService movieService)
        {
            _movieService = movieService;
        }
        public async Task<IActionResult> Index()
        {


            var data = await _movieService.GetAll(n=>n.Cinema);
            return View(data);
        }

        //Get: Movies/Details/1
        public async Task<IActionResult> Details (int id)
        {
            var result = await _movieService.GetMovieById(id);
            if (result == null) return View("NotFound");
            return View(result);
        }

        //Gte: Movies/Create

        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }
    }
}
