using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class Inversor : ApplicationUser
    {
        public virtual IList<Preferencias> Preferencias
        {
            get;
            set;
        }
    }
}