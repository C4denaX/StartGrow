using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models.InversionViewModels
{
    public class InversionesCreateViewModel
    {
        public InversionesCreateViewModel()
        {
            this.inversiones = new List<InversionCreateViewModel>();            
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
        public virtual IList<InversionCreateViewModel> inversiones
        {
            get;
            set;
        }        
    }

    public class InversionCreateViewModel
    {
        public InversionCreateViewModel()
        {
            this.inversion = new Inversion();
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
        public virtual float Total
        {
            get;
            set;
        }

        public TiposInversiones tipoinversion
        {
            get;
            set;
        }
        public virtual Inversion inversion
        {
            get;
            set;
        }
    }


}
