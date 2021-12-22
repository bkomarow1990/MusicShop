using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AspNet_MVC_App.Models
{
    public class Music
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You must enter a model")]
        [MinLength(3, ErrorMessage = "Must have at least three symbols")]
        public string Name { get; set; }
        public string Performer { get; set; }
        public int PublishYear { get; set; }
        public string Text { get; set; }
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
