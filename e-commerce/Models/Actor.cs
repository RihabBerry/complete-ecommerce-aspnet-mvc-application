using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Actor
    {
        [Key] 
        public int ActorId { get; set; }
        [Display(Name ="Profile Picture Url")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]

        public string FullName{ get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public List<Actor_Movie> Actors_Movies { get; set; }

    }
}
