using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StartGrow.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual string Nombre
        {
            get;
            set;
        }

        public virtual string Apellido1
        {
            get;
            set;
        } 

        public virtual string Apellido2
        {
            get;
            set;
        }

        public virtual string Nif
        {
            get;
            set;
        }

        public virtual string Nacionalidad
        {
            get;
            set;
        }

        public virtual string PaisResidencia
        {
            get;
            set;
        }

        public virtual string Provincia
        {
            get;
            set;
        }

        public virtual string Municipio
        {
            get;
            set;
        }

        public virtual string Domicilio
        {
            get;
            set;
        }

        public virtual string CodPost
        {
            get;
            set;
        }


    }
}
