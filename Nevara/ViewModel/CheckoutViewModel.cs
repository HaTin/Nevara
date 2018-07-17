using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Extensions;
using Nevara.Models;
using Nevara.Models.Enum;

namespace Nevara.ViewModel
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
