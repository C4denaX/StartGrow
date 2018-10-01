using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StartGrow.Models
{
    public class InversionProyecto
    {
        public virtual float Importe { get; set; }

       public virtual decimal Interes { get; set; }
            [Key]
            public virtual int InvProyectoId { get; set; }
            public virtual int MinInversion { get; set; }
            public virtual int NumInversores { get; set; }
           public virtual DateTime Plazo { get; set; }
           public virtual decimal Progreso { get; set; }
           public virtual Proyecto proyecto { get; set; }
           public virtual int ProyectoId { get; set; }
           public virtual char Rating { get; set; }
           public virtual IList<TiposInversiones> TiposDeInversion { get; set; }


    }
}
