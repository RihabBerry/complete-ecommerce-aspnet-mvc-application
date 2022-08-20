using e_commerce.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture Url")]
        [Required(ErrorMessage ="Picture Profile is required ")]
        public string ProfilePictureURL { get; set; }
        [Required(ErrorMessage ="Full Name is required ")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        //RelationShip 
        public List<Movie> Movies { get; set; }


    }
}
