using System;
using System.Collections.Generic;
using System.Linq;
using Nevara.ApplicationCore.Extensions;
using Nevara.ApplicationCore.Models;
using Nevara.ApplicationCore.Models.Enum;

namespace Nevara.ApplicationCore.ViewModel
{
    public class CheckoutViewModel : OrderViewModel
    {
        public List<ShoppingCartViewModel> Carts { get; set; }
        public List<EnumModel> PaymentMethods
        {
            get
            {
                return ((PaymentMethod[]) Enum.GetValues(typeof(PaymentMethod)))
                    .Select(c => new EnumModel()
                    {
                        Value = (int) c,
                        Name = c.GetDescription()
                    }).ToList();
            }
        }
    }
}
