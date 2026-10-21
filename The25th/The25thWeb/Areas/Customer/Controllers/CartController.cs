using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using The25th.Business.Services.IServices;
using The25th.Models;

namespace The25thWeb.Areas.Customer.Controllers;

[Area("Customer")]

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly IShoppingCartService _shoppingCartService;

    public CartController(IProductService productService, IShoppingCartService shoppingCartService)
    {
        _productService = productService;
        _shoppingCartService = shoppingCartService;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
}