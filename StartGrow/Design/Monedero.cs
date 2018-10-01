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
        public virtual Inversor Inversor { get; set; }
        public virtual int InversorId { get; set; }                

        [Required]
        [DataType (DataType.Currency)]
        public virtual decimal Dinero { get; set; }
    }
}
