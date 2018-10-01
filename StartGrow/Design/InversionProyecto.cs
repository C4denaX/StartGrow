using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class InversionProyecto
    {
        [Key]
        public virtual int InvProyectoId
        {
            get;
            set;
        }

        [Required]
        [ForeignKey("ProyectoId")]
        public virtual Proyecto Proyecto
        {
            get;
            set;
        }
        public virtual int ProyectoId
        {
            get;
            set;
        }

        public virtual IList<Inversion> Inversiones
        {
            get;
            set;
        }

        [Required]
        [Range(100, int.MaxValue, ErrorMessage = "La inversión Mínima es 100 euros")]
        public virtual int MinInversion
        {
            get;
            set;
        }

        [Required]
        public virtual int Plazo
        {
            get;
            set;
        }

        [Required]
        public virtual char Rating
        {
            get;
            set;
        }

        [Required]
        public virtual double Interes
        {
            get;
            set;
        }

        public virtual IList <TiposInversiones> TiposDeInversion
        {
            get;
            set;
        }

        [Required]
        public virtual double Importe
        {
            get;
            set;
        }

        [Required]
        public virtual int Progreso
        {
            get;
            set;
        }

        [Required]
        public virtual int NumInversores
        {
            get;
            set;
        }
    }
}
