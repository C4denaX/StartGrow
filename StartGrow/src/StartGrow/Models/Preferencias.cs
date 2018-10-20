using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class Preferencias
    {
        [Key]
        public virtual int PreferenciasId
        {
            get;
            set;
        }

 

        [ForeignKey("TiposInversionesID")]
        public string TiposInversionesID
        {
            get;
            set;
        }
        public virtual TiposInversiones TiposInversiones
        {
            get;
            set;
        }

        [ForeignKey("RatingID")]
        public string RatingID
        {
            get;
            set;
        }
        public virtual Rating Rating
        {
            get;
            set;
        }

        [ForeignKey("AreasID")]
        public string AreasID
        {
            get;
            set;
        }
        public virtual Areas Areas
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

    }
}