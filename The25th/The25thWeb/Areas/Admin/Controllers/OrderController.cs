using The25th.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using The25th.Models;
using The25th.Business.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using The25th.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using The25th.Utility;

namespace The25thWeb.Areas.Customer.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.RoleAdmin)]

    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        [BindProperty]
        public OrderHeader OrderHeader { get; set; }
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int orderId)
        {
            OrderHeader = await _orderService.GetOrderByIdAsync(orderId, includeDetail: true, includeUser: true);
            return View(OrderHeader);
        }

        #region API CALLS
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrderAsync();
            return Json(new { data = orders });
        }

        #endregion
    }
}