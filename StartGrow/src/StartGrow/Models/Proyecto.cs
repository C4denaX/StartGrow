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
        public virtual int ProyectoId
        {
            get;
            set;
        }

        [Required]
        [ForeignKey("InvProyectoId")]
        public virtual InversionProyecto InvProyecto
        {
            get;
            set;
        }
        public virtual int InvProyectoId
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
