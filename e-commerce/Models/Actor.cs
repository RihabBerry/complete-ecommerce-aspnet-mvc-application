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
        [Required(ErrorMessage ="Profile Picture is required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Full Name mst be between 3 and 50 chars")]
        [Required(ErrorMessage = "Full name is required")]

        public string FullName{ get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]

        public string Bio { get; set; }

        public List<Actor_Movie> Actors_Movies { get; set; }

    }
}
