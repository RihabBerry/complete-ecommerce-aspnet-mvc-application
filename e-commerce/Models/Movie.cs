﻿using e_commerce.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime  EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        public List <Actor_Movie> Actors_Movies { get; set; }

        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        public int ProducerId { get; set;  }
        public Producer Producer { get; set;  }



    }
}
