using e_commerce.Data.Base;
using e_commerce.Data.ViewModels;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task AddMovie(NewMovieVM data)
        {
            //Add movie to Movie table
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                ProducerId = data.ProducerId,
                EndDate = data.EndDate,
                StartDate = data.StartDate,
                MovieCategory = data.MovieCategory
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            // add actors to Movies_Actors juncture table 

            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie() { ActorId = actorId, MovieId = newMovie.Id };
                await _context.Actors_Movies.AddAsync(newActorMovie);
                await _context.SaveChangesAsync();
            }


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

        public async Task UpdateMovie(int id, NewMovieVM data)
        {
            //le retour cest les element de la base des données tnajme ta3mel 3lihom modification 
            var existedMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (existedMovie != null)
            {

                {
                    existedMovie.Name = data.Name;
                    existedMovie.Description = data.Description;
                    existedMovie.Price = data.Price;
                    existedMovie.ImageURL = data.ImageURL;
                    existedMovie.CinemaId = data.CinemaId;
                    existedMovie.ProducerId = data.ProducerId;
                    existedMovie.EndDate = data.EndDate;
                    existedMovie.StartDate = data.StartDate;
                    existedMovie.MovieCategory = data.MovieCategory;
                    await _context.SaveChangesAsync();

                };
                //Absolutly you nedd to remove existed actors and add the new actors when you are updating 
                // add actors to Movies_Actors juncture table 

                var existedActors = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
                _context.Actors_Movies.RemoveRange(existedActors);

                foreach (var actorId in data.ActorIds)
                {
                    var newActorMovie = new Actor_Movie() { ActorId = actorId, MovieId = data.Id };
                    await _context.Actors_Movies.AddAsync(newActorMovie);
                    await _context.SaveChangesAsync();
                }


            }



        }
    }
}
