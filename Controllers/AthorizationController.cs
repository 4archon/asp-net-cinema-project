using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace movies.Controllers;

public class AuthorizationController : BaseController
{
    [HttpGet]
    public IActionResult Autho()
    {
        Render_user();
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        AuthorizationVM enter = new AuthorizationVM();
        enter.invalid=false;
        return View("Autho", enter);
    }

    [HttpPost]
    public IActionResult Autho(AuthorizationVM enter)
    {
        Render_user();
        if (ModelState.IsValid)
        { 
            using (var context = new Context())
            {
                var result = from u in context.users where u.email == enter.email &&
                u.password == enter.password select new
                {
                    id = u.id,
                    email = u.email,
                    role = u.role
                };
                if (result.Count() != 0)
                {
                    int id;
                    string email;
                    string role;
                    id = result.FirstOrDefault().id;
                    email = result.FirstOrDefault().email;
                    role = result.FirstOrDefault().role.ToString();
                    var claims = new List<Claim> {new Claim(ClaimTypes.Name, id.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)};
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(claimsPrincipal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    enter.exists=false;
                    return View("Autho", enter);
                }
            }
        }
        else
        {
            enter.invalid = true;
            return View("Autho", enter);
        }
    }
    [Authorize]
    public IActionResult Signout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Test()
    {
        ClaimsPrincipal User = HttpContext.User;
        Console.WriteLine(User.Identity.IsAuthenticated); 
        return RedirectToAction("Test", "Home");
    }
}