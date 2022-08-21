using e_commerce.Data.Base;
using e_commerce.Data.ViewModels;
using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>

    {
        Task<Movie> GetMovieById(int id);
        Task<NewMovieDropDownVM> GetNewMovieDropdownsValues();
    }

}
