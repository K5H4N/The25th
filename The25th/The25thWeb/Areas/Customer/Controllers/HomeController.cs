using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using The25th.Business.Services.IServices;
using The25th.Models;

namespace The25thWeb.Areas.Customer.Controllers;

[Area("Customer")]

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IShoppingCartService _shoppingCartService;

    public HomeController(IProductService productService, IShoppingCartService shoppingCartService)
    {
        _productService = productService;
        _shoppingCartService = shoppingCartService;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync(includeCategory: true);
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId, includeCategory: true);
        if (product == null)
        {
            return NotFound();
        }

        return View(new ShoppingCart
        {
            ProductId = productId,
            Product = product,
            Count = 1
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        shoppingCart.ApplicationUserId = userId;
        await _shoppingCartService.AddToCartAsync(shoppingCart);

        return RedirectToAction("Details", new { productId = shoppingCart.ProductId });
    }
}