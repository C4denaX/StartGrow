using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models.InversionViewModels
{
    public class SelectProyectosForInversionViewModel
    {
        //Lista de Proyectos
        public IEnumerable <Proyecto> Proyectos { get; set; }

        //Utilizado para filtrar por Areas
        [Display(Name = "Areas")]
        public string[] areas {get; set;}
        public IEnumerable <Areas> Areas { get; set; }
        
        //Utilizado para filtrar por Rating
        [Display(Name = "Rating")]
        public string[] rating { get; set; }
        public IEnumerable<Rating> Rating{ get; set; }

        //Utilizado para filtrar por TiposInversiones
        [Display(Name = "TiposInversiones")]
        public string [] ids_tiposInversiones { get; set; }
        public IEnumerable<TiposInversiones> TiposInversiones { get; set; }
        

        public IList<int> IdsToAdd { get; set; }
    }
}
