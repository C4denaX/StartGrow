using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class Cesta
    {
        [Key]
        public virtual int IdCesta
        {
            get;
            set;
        }

        [Required]
        [ForeignKey ("CestaId")]
        public virtual Inversor Inversor
        {
            get;
            set;
        }
        public virtual string CestaId
        {
            get;
            set;
        }

        

    }
}
