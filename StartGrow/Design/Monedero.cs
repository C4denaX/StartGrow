﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class Monedero
    {
        [Key]
        public virtual int MonederoId { get; set; }

        [Required]
        [ForeignKey("InversorId")]
        public virtual int InversorId { get; set; }
        public virtual Inversor Inversor { get; set; }

        [Required]
        public virtual float Dinero { get; set; }

        [Required]
        public virtual IList<InversionRecuperada> InversionesRecuperadas { get; set; }
    }
}
