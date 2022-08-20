﻿using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Data.Services
{
   public  interface IActorsService
    {
        public Task<IEnumerable<Actor>> GetAll();
        Task<Actor> GetById(int id);
        Task Add(Actor actor);
        Task<Actor> Update(int id, Actor newActor);
        Task Delete(int id);

         
    }
}
