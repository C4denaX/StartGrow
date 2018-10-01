﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class Inversor : ApplicationUser
    {
        [Required]
        public virtual IList<Inversion> Inversiones { get; set; }
    }
}
