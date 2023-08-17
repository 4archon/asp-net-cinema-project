using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace movies.Controllers;

public class BaseController : Controller
{
    public void Render_user()
    {
        bool user_valid = HttpContext.User.Identity.IsAuthenticated;
        if (user_valid)
        {
            ViewData["User_valid"] = user_valid;
            ViewData["User_name"] = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ViewData["User_email"] = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            ViewData["User_role"] = HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        else
        {
            ViewData["User_valid"] = false;
        }
    }
}