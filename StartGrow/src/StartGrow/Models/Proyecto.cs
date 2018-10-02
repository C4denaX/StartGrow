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
        public virtual IList<Inversion> Inversiones { get; set; }

        [Required]
        public virtual string Nombre { get; set; }
        [Required]
        public virtual Areas Area { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public virtual DateTime FechaExpiracion { get; set; }
        [Required]
        public virtual TiposInversiones TiposDeInversion { get; set; }
        [Required]
        [Range(100, int.MaxValue, ErrorMessage = "La inversión Mínima es 100 euros")]
        public virtual float MinInversion { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public virtual DateTime Plazo { get; set; }
        [Required]
        public virtual float Interes { get; set; }
        [Required]
        public virtual float Importe { get; set; }
        [Required]
        public virtual float Progreso { get; set; }
        [Required]
        public virtual int NumInversores { get; set; }
        



    }
}
