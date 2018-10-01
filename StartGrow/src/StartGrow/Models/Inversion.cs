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
        public virtual int InversorId { get; set; }
        [ForeignKey("InversorId")]
        public virtual Inversor Inversor { get; set; }

        [Required]
        public virtual int InvProyectoId { get; set; }
        [ForeignKey("InvProyectoId")]
        public virtual InversionProyecto InversionProyecto { get; set; }

        [Required]
        public virtual float Cuota { get; set; }
        [Required]
        public virtual float Intereses { get; set; }
        [Required]
        public virtual float Total { get; set; }
        [Required]
        public virtual IList<TiposInversiones> TiposDeInversion { get; set; }

        

    }
}
