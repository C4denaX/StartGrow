using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class Inversion
    {
        [Key]
        public virtual int InversionId { get; set; }

        [Required]
        [ForeignKey("InversorId")]
        public virtual int InversorId { get; set; }
        public virtual Inversor Inversor { get; set; }

        [Required]
        [ForeignKey("ProyectoId")]
        public virtual int ProyectoId { get; set; }
        public virtual Proyecto Proyecto { get; set; }

        [Required]
        public virtual float Cuota { get; set; }
        [Required]
        public virtual float Intereses { get; set; }
        [Required]
        public virtual float Total { get; set; }
        //[Required]
        //public virtual IList<TiposInversiones> TiposDeInversion { get; set; }

        [Required]
        public virtual IList<InversionRecuperada> InversionesRecuperadas { get; set; }

    }
}
