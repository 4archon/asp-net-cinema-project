using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace movies.Controllers;

public class AccessController : BaseController
{
    public IActionResult Denied()
    {
        Render_user();
        return View("DeniedView");
    }
}