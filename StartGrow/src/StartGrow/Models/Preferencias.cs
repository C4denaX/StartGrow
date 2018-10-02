using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models
{
    public class Preferencias
    {
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

        public int ApplicationUserId
        {
            get;
            set;
        }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser
        {
            get;
            set;
        }
    }
}
