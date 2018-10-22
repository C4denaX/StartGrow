using StartGrow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Design
{
    public class ProyectoAreas
    {

        [Key]
        public virtual int ID
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
        [ForeignKey("ProyectoId")]
        public virtual Proyecto Proyecto
        {
            get;
            set;
        }



        public virtual string AreasId
        {
            get;
            set;
        }
        [Required]
        [ForeignKey("AreasId")]
        public virtual Areas Areas
        {
            get;
            set;
        }

    }
}