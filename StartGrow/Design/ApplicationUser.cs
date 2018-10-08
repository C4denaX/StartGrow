using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StartGrow.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public virtual string Nombre
        {
            get;
            set;
        }

        [Required]
        public virtual string Apellido1
        {
            get;
            set;
        }

        [Required]
        public virtual string Apellido2
        {
            get;
            set;
        }

        [Required]
        public virtual string Nif
        {
            get;
            set;
        }

        [Required]
        public virtual string Nacionalidad
        {
            get;
            set;
        }

        [Required]
        public virtual string PaisResidencia
        {
            get;
            set;
        }

        [Required]
        public virtual string Provincia
        {
            get;
            set;
        }

        [Required]
        public virtual string Municipio
        {
            get;
            set;
        }

        [Required]
        public virtual string Domicilio
        {
            get;
            set;
        }

        [Required]
        public virtual string CodPost
        {
            get;
            set;
        }

        public virtual IList<Preferencias> Preferencias
        {
            get;
            set;
        }

    }
}
