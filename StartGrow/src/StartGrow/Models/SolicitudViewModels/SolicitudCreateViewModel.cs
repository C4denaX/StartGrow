 using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartGrow.Models.SolicitudViewModels
{
    public class SolicitudCreateViewModel
    {

        public virtual string Name
        {
            get;
            set;
        }

        [Display(Name = "Primer Apellido")]
        public virtual string FirstSurname
        {
            get;
            set;
        }

        [Display(Name = "Segundo Apellido")]
        public virtual string SecondSurname
        {
            get;
            set;
        }

        //It will be necessary whenever we need a relationship with ApplicationUser or any child class
        public string TrabajadorId
        {
            get;
            set;
        }

        public String rating
        {
            get;
            set;
        }
        public double? interes
        {
            get;
            set;
        }
        public int? plazo
        {
            get;
            set;
        }

        public Estados estados
        {
            get;
            set;
        }
        public DateTime FechaSolicitud
        {
            get;
            set;
        }

        public virtual IList<Solicitud> Solicitudes
        {
            get;
            set;
        }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]

        public String DeliveryAddress
        {
            get;
            set;
        }

        [Display(Name = "Payment Method")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, select your payment method for delivery")]
        public String PaymentMethod
        {
            get;
            set;
        }

        public SolicitudCreateViewModel()
        {
            this.Solicitudes = new List<Solicitud>();
        }




    }
}