using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nevara.ApplicationCore.Extensions;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.Models;
using Nevara.ApplicationCore.Models.Enum;

namespace Nevara.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderSerivce _orderSerivce;

        public OrderController(IOrderSerivce orderSerivce)
        {
            _orderSerivce = orderSerivce;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Find(int id)
        {
            var model = await _orderSerivce.getOrder(id);
            return new OkObjectResult(model);
        }
        public async Task<IActionResult> UpdateStatus(int orderId,BillStatus status)
        {
            await _orderSerivce.UpdateStatus(orderId, status);
            return new OkObjectResult(orderId);
        }
        public async Task<IActionResult> GetOrders(int page,int pageSize,string keyword)
        {
            var result = await _orderSerivce.GetOrders(page, pageSize,keyword);
            return new OkObjectResult(result);
        }
        [HttpGet]
        public IActionResult GetBillStatus()
        {
            var enums = ((BillStatus[])Enum.GetValues(typeof(BillStatus)))
                .Select(c => new EnumModel
                {
                    Value = (int)c,
                    Name = c.GetDescription()
                }).ToList();
            return new OkObjectResult(enums);
        }
        [HttpGet]
        public IActionResult GetPaymentMethod()
        {
            var enums = ((PaymentMethod[])Enum.GetValues(typeof(PaymentMethod)))
                .Select(c => new EnumModel
                {
                    Value = (int)c,
                    Name = c.GetDescription()
                }).ToList();
            return new OkObjectResult(enums);
        }
    }
}