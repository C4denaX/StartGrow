using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class EstadosInversiones
    {
        public virtual string EstadosInversionesID { get; set; }

        public virtual IList<Preferencias> Preferencias { get; set; }

        public virtual IList<Inversion> Inversiones { get; set; }

        public virtual string NombreEstado { get; set; }
    }
}
