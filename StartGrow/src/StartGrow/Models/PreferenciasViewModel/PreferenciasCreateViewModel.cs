using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StartGrow.Models.PreferenciasViewModel
{
    public class PreferenciasCreateViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir el Nombre")]
        public virtual string Nombre
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir el primer Apellido")]
        [Display(Name = "Primer apellido")]
        public virtual string Apellido1
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir el segundo Apellido")]
        [Display(Name = "Segundo apellido")]
        public virtual string Apellido2
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir el NIF")]
        public virtual string NIF
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir la Nacionalidad")]
        public virtual string Nacionalidad
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir el Pais de Residencia")]
        public virtual string PaisDeResidencia
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir la Provincia")]
        public virtual string Provincia
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir el Municipio")]
        public virtual string Municipio
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir el Domicilio")]
        public virtual string Domicilio
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe introducir el Codigo Postal")]
        public virtual int CodPost
        {
            get;
            set;
        }
    }
}
