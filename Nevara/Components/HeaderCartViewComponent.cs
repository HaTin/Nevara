using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nevara.Extensions;
using Nevara.ViewModel;

namespace Nevara.Components
{
    public class HeaderCartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
            if(cart == null) cart = new List<ShoppingCartViewModel>();
            return View(cart);
        }
    }
}
