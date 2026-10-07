using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using The25th.Business.Services.IServices;

namespace The25thWeb.Areas.Customer.Controllers;

[Area("Customer")]

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync(includeCategory: true);
        return View(products);
    }

    public async Task<IActionResult> Details(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId, includeCategory: true);
        return View(product);
    }
    public IActionResult Privacy()
    {
        return View();
    }


}