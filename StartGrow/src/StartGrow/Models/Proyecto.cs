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
        public int IdProyecto
        {
            get;
            set;
        }

        [Required]
        [ForeignKey ("Inversor")]
        public virtual Inversor Inversor
        {
            get;
            set;
        }
        public virtual string IdInversor
        {
            get;
            set;
        }

        [Required]
        public virtual String Nombre
        {
            get;
            set;
        }

        [Required]
        public virtual String Area
        {
            get;
            set;
        }

        [Required]
        public virtual String TiposDeInversion
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

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha expiración")]
        public virtual DateTime FechaExpiracion
        {
            get;
            set;
        }


    }
}
