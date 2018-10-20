using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models.InversionRecuperadaViewModels
{
    public class SelectInversionForRecuperarInversionViewModel
    {
        //Lista de Inversiones
        public IEnumerable<Inversion> Inversiones { get; set; }
        public IList<int> IdsToAdd { get; set; }

        //Utilizado para filtrar por Rating
        public SelectList Ratings;
        [Display(Name = "Rating")]
        public string inversionRatingSeleccionado { get; set; }

        //Utilizado para filtrar por el nombre de la inversión
        [Display(Name = "Nombre")]
        public string nombreInv{ get; set; }


        
    }
}
