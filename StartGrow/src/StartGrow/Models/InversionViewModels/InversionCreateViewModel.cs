using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models.InversionViewModels
{
    public class InversionCreateViewModel
    {
        public InversionCreateViewModel()
        {
            Inversiones= new List<Inversion>();
        }

        public virtual string Name
        {
            get;
            set;
        }
        [Display(Name = "Primer Apellido")]
        public virtual string FirstSurname
        {
            get;
            set;
        }
        [Display(Name = "Segundo Apellido")]
        public virtual string SecondSurname
        {
            get;
            set;
        }

        //It will be necessary whenever we need a relationship with ApplicationUser or any child class
        public string InversorId
        {
            get;
            set;
        }
        [Required]
        public virtual float Cuota
        {
            get;
            set;
        }
        [Required]
        public virtual float Intereses
        {
            get;
            set;
        }        
        public virtual string EstadosInversiones
        {
            get;
            set;
        }        
        public virtual float Total
        {
            get;
            set;
        }
        public virtual IList<Inversion> Inversiones
        {
            get;
            set;
        }        
    }
}
