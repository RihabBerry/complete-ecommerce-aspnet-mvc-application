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
    [Authorize(Roles=UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _producerService.GetAll();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var producer = await _producerService.GetById(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                View(producer);
            }
            await _producerService.Add(producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _producerService.GetById(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer, int id)
        {
            if (!ModelState.IsValid)
            {
                View(producer);
            }
            await _producerService.Update(id, producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            var producer = await _producerService.GetById(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _producerService.GetById(id);
            if (producer == null) return View("NotFound");
            await _producerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
