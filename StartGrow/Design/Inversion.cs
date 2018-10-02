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

        [Required]
        [ForeignKey("InversorId")]
        public virtual Inversor Inversor
        {
            get;
            set;
        }           
        public virtual int InversorId
        {
            get;
            set;
        }
    }
}