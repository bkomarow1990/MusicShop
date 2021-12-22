using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Models.ViewModels
{
    public class MusicVM
    {
        public Music Music { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
        public bool IsEdit { get; set; }
    }
}
