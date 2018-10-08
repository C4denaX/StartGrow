using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class TiposInversiones
    {
        [Key]
        public virtual string TiposInversionesID
        {
            get;
            set;
        }

        [Required]
        public virtual string Nombre
        {
            get;
            set;
        }

        public virtual IList<Preferencias> Preferencias
        {
            get;
            set;
        }
    }
}

