using e_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Actor actor)
        {
            _context.Actors.Add(actor);
           await  _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            var result = await _context.Actors.ToListAsync();
            return result;
        }

        public async Task<Actor> GetById(int id)
        {
            Actor result = await  _context.Actors.FirstOrDefaultAsync(n=>n.ActorId==id);
            return result;
         }
        public async Task<Actor> Update(int id, Actor newActor)
        {
            _context.Actors.Update(newActor);
            await _context.SaveChangesAsync();
            return newActor;
        }

        public async Task Delete (int id)
        {
            _context.Remove(_context.Actors.Single(a => a.ActorId == id));
           await _context.SaveChangesAsync();

            //Actor result = await  _context.Actors.FirstOrDefaultAsync(n=>n.ActorId==id);
            //_context.Remove(result);


        }
    }
}
