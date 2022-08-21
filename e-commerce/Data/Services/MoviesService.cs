using e_commerce.Data.Base;
using e_commerce.Data.ViewModels;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Movie> GetMovieById(int id)
        {
            var moviesDetails = _context.Movies
                 .Include(c => c.Cinema)
                 .Include(p => p.Producer)
                 .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                 .FirstOrDefaultAsync(n => n.Id == id);
            return moviesDetails;
        }

        public async Task<NewMovieDropDownVM> GetNewMovieDropdownsValues()
        {

            var response = new NewMovieDropDownVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }
    }
}
