using System;
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
        public virtual int MonederoId
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
        public virtual string Id
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Currency)]
        public virtual decimal Dinero { get; set; }       
    }
}