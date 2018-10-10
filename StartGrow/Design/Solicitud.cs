using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StartGrow.Models
{
    public class Solicitud
    {
        [Key]
        public virtual int SolicitudID { get; set; }
        [Required]
        [ForeignKey("ProyectoID")]
        public virtual Proyecto Proyecto { get; set; }
        [Required]
        public virtual int Estado { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public virtual DateTime FechaSolicitud { get; set; }

        [ForeignKey("TrabajadorID")]
        public virtual Trabajador Trabajador { get; set; }


    }

}

