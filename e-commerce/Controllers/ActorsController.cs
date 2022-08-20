using e_commerce.Data;
using e_commerce.Data.Services;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorService;

        public ActorsController(IActorsService actorService)
        {
            _actorService = actorService;
        }

        public async Task<IActionResult> Index()
        {


            var data = await _actorService.GetAll();
            return View(data);
        }

        //Get :Actors/Create
        public IActionResult Create()
       
        {
           
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
           await  _actorService.Add(actor);
            return RedirectToAction(nameof(Index));

        }
        //Get:Actors/Details/id
        public async Task<ActionResult> Details(int id) {
            

            Actor actorDetails = await _actorService.GetById(id);
            if (actorDetails == null) return View("Empty");
            return View(actorDetails);
        }
        //Get : Actors/Edit/id
        public async Task<ActionResult> Edit (int id)
        {
            Actor actorDetails = await _actorService.GetById(id);
            if (actorDetails == null) return View("Empty");
            return View(actorDetails);
        }
        //Post: Actors/Edit/Id
        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ActorId,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorService.Update(id, actor);
            return RedirectToAction(nameof(Index));
        }
        
        //Get: Actors/Delete/1
        public async Task<ActionResult> Delete(int id )
        {
            Actor actorDetails = await _actorService.GetById(id);
            if (actorDetails == null) return View("Empty");
            return View(actorDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Actor actorDetails = await _actorService.GetById(id);
            if (actorDetails == null) return View("Empty");
            await _actorService.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
