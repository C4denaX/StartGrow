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
        public SelectList Areas;
        
        //Utilizado para filtrar por Rating
        [Display(Name = "Rating")]
        public SelectList Rating;

        //Utilizado para filtrar por TiposInversiones
        [Display(Name = "TiposInversiones")]
        public SelectList TiposInversiones;
        

        public IList<int> IdsToAdd { get; set; }
    }
}
