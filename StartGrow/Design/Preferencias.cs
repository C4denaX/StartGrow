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


        public string TiposInversionesID
        {
            get;
            set;
        }
        [ForeignKey("TiposInversionesID")]
        public virtual TiposInversiones TiposInversiones
        {
            get;
            set;
        }


        public string RatingID
        {
            get;
            set;
        }
        [ForeignKey("RatingID")]
        public virtual Rating Rating
        {
            get;
            set;
        }


        public string AreasID
        {
            get;
            set;
        }
        [ForeignKey("AreasID")]
        public virtual Areas Areas
        {
            get;
            set;
        }


        public virtual ApplicationUser ApplicationUser
        {
            get;
            set;
        }
        [Required]
        [ForeignKey("Id")]
        public virtual string Id
        {
            get;
            set;
        }

    }
}