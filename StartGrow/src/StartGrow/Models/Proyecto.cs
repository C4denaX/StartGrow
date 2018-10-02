using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartGrow.Models
{
    public class Proyecto
    {   [Key]
        public virtual int ProyectoID { get; set; }
        [Required]
        public virtual string Nombre { get; set; }
      //  [Required]
    //    public virtual IList<Areas> Area { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public virtual DateTime FechaExpiracion { get; set; }
        
        public virtual IList<Solicitud> Solicitudes { get; set; }

    }
}
