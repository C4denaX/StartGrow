using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class Proyecto
    {
        [Key]
        public virtual int ProyectoId { get; set; }

        [Required]
        public virtual int InvProyectoId { get; set; }
        [ForeignKey("InvProyectoId")]
        public virtual InversionProyecto InvProyecto { get; set; }

        [Required]
        public virtual string Nombre { get; set; }
        [Required]
        public virtual Areas Area { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public virtual DateTime FechaExpiracion { get; set; }



    }
}
