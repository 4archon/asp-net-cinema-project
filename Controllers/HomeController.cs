using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace movies.Controllers;

public class HomeController : BaseController
{
    private readonly IConfiguration _config;
    // private readonly ILogger<HomeController> _logger;

    public HomeController(IConfiguration config/*, ILogger<HomeController> logger*/)
    {
        // _logger = logger;
        _config = config;
    }

    public IActionResult Index()
    {
        Render_user();
        return View("Index");
    }

    [Authorize(Policy="Admin")]
    public IActionResult Privacy()
    {
        Render_user();
        return View();
    }

    public IActionResult Test()
    {
        Render_user();
        return View("Test");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        Render_user();
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
