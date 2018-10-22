﻿using System;
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
        public virtual int InversionId
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




        public virtual string Id
        {
            get;
            set;
        }
        [Required]
        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser
        {
            get;
            set;
        }


        public virtual string TipoInversionesId { get; set; }
        [Required]
        [ForeignKey("TipoInversionesId")]
        public virtual TiposInversiones TipoInversiones
        {
            get;
            set;
        }

        [Required]
        public virtual string EstadosInversiones { get; set; }

        public virtual string TipoInversionesId
        {
            get;
            set;
        }

        [Required]
        public virtual float Cuota
        {
            get;
            set;
        }
        [Required]
        public virtual float Intereses
        {
            get;
            set;
        }
        [Required]
        public virtual float Total
        {
            get;
            set;
        }



    }
}