using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetStore.WEB.Models
{
    public class CheckOutUserModel
    {
        public CheckOutGuestModel GuestModel { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public string DeliveryMethod { get; set; }

        public string PaymentMethod { get; set; }
    }
}