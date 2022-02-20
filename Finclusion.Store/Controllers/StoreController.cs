using Microsoft.AspNetCore.Mvc;
using Finclusion.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Finclusion.Store.Services;
using System.Security.Claims;

namespace Finclusion.Identity.Controllers;

[Authorize]
[Route("api/[controller]")]
public class StoreController : Controller
{
    private readonly IProductService _productService;
    public StoreController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var results = await _productService.GetProducts();
        return Ok(results);
    }

    [HttpPost]
    [Route("buy/{productId}")]
    public async Task<IActionResult> BuyProduct(int productId)
    {
        var identity = this.User.Identity as ClaimsIdentity;
        var claimUsername = identity?.FindFirst(ClaimTypes.Name).Value;   
        var result = await _productService.BuyProduct(claimUsername, productId);
        if(result == null) { return NotFound(); }
        return Ok(result);
    }
}