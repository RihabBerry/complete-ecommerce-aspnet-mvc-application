using e_commerce.Data;
using e_commerce.Data.Services;
using e_commerce.Data.Static;
using e_commerce.Data.ViewModels;
using e_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {

        private readonly IMoviesService _movieService;

        public MoviesController(IMoviesService movieService)
        {
            _movieService = movieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {


            var data = await _movieService.GetAll(n => n.Cinema);
            return View(data);
        }

        //Get: Movies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
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

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,StartDate,EndDate,Price,ImageURL,CinemaId,MovieCategory,ProducerId,ActorIds,Description")] NewMovieVM movie)
        {

            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            await _movieService.AddMovie(movie);
            return RedirectToAction(nameof(Index));
        }

        //Get : Movies/Edit/1 
        public async Task<ActionResult> Edit(int id)
        {
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            var movieDetails = await _movieService.GetMovieById(id);
            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };
            if (movieDetails == null) return View("NotFound");

            return View(response);

        }

        //Post :Movies/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,StartDate,EndDate,Price,ImageURL,CinemaId,MovieCategory,ProducerId,ActorIds,Description")] NewMovieVM movie, int id)
        {
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            await _movieService.UpdateMovie(id, movie);
            return RedirectToAction(nameof(Index));

        }
        //Get: Movies/Filter 

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _movieService.GetAll(n => n.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filtredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filtredResult);
            }
            return View("Index",allMovies)
          ;
        }
    }
}
