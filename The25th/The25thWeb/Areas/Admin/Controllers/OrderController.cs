using The25th.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using The25th.Models;
using The25th.Business.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using The25th.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using The25th.Utility;
using System.Security.Claims;

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
            var orderHeader = await _orderService.GetOrderByIdAsync(orderId, includeDetail: true, includeUser: true);
            if (orderHeader == null)
            {
                return NotFound();
            }

            return View(orderHeader);
        }
        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin + "," + SD.RoleEmployee)]
        public async Task<IActionResult> UpdateOrderDetails()
        {
            var orderHeaderFormDb = await _orderService.GetOrderByIdAsync(OrderHeader.Id);
            orderHeaderFormDb.Name = OrderHeader.Name;
            orderHeaderFormDb.PhoneNumber = OrderHeader.PhoneNumber;
            orderHeaderFormDb.StreetAddress = OrderHeader.StreetAddress;
            orderHeaderFormDb.City = OrderHeader.City;
            orderHeaderFormDb.State = OrderHeader.State;
            orderHeaderFormDb.PostalCode = OrderHeader.PostalCode;
            if (!string.IsNullOrEmpty(OrderHeader.Carrier) && orderHeaderFormDb.OrderStatus == SD.StatusShipped)
            {
                orderHeaderFormDb.Carrier = OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(OrderHeader.TrackingNumber) && orderHeaderFormDb.OrderStatus == SD.StatusShipped)
            {
                orderHeaderFormDb.TrackingNumber = OrderHeader.TrackingNumber;
            }
            await _orderService.UpdateOrderAsync(orderHeaderFormDb);

            TempData["Success"] = "Order Details Updated Successfully";
            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFormDb.Id });
        }
        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin + "," + SD.RoleEmployee)]
        public async Task<IActionResult> UpdateOrderStatus(string status)
        {
            var orderHeader = await _orderService.GetOrderByIdAsync(OrderHeader.Id);
            if (orderHeader == null)
            {
                TempData["error"] = "Order not found";
                return RedirectToAction(nameof(Index));
            }
            string successMessage;
            switch (status)
            {
                case SD.StatusInProcess:
                    await _orderService.UpdateOrderStatusAsync(OrderHeader.Id, status);
                    successMessage = "Order processing started sucessfully.";
                    break;
                case SD.StatusCancelled:
                    await _orderService.UpdateOrderStatusAsync(OrderHeader.Id, status);
                    successMessage = "Order cancelled sucessfully.";
                    break;
                case SD.StatusRefunded:
                    await _orderService.UpdateOrderStatusAsync(OrderHeader.Id, status);
                    successMessage = "Order refunded sucessfully.";
                    break;
                case SD.StatusShipped:

                    if (string.IsNullOrEmpty(OrderHeader.Carrier) || string.IsNullOrEmpty(OrderHeader.TrackingNumber))
                    {
                        TempData["error"] = "Please provided both carrier and tracking number.";
                        return RedirectToAction(nameof(Details), new { orderId = OrderHeader.Id });
                    }

                    await _orderService.UpdateOrderStatusAsync(
                        OrderHeader.Id, SD.StatusShipped, OrderHeader.Carrier, OrderHeader.TrackingNumber
                    );
                    successMessage = "Order shipped sucessfully.";
                    break;

                default:
                    TempData["error"] = "Invalid Status Update";
                    return RedirectToAction(nameof(Details), new { orderId = OrderHeader.Id });
            }

            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(Details), new { orderId = OrderHeader.Id });
        }

        #region API CALLS
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string status)
        {
            string? userId = null;

            if (!User.IsInRole(SD.RoleAdmin) && !User.IsInRole(SD.RoleEmployee))
            {
                var claimsIdentidy = (ClaimsIdentity)User.Identity;
                userId = claimsIdentidy?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
            }

            var orders = await _orderService.GetAllOrderAsync(userId, status);
            return Json(new { data = orders });
        }

        #endregion
    }
}