using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace movies.Controllers;

public class SeancesController : BaseController
{
    string Runtime_string(float? value)
    {
        string result = "";
        int hours = ((int) value / 60);
        result += hours.ToString() + "h ";
        value = value - hours * 60;
        int min = (int) value;
        result += min.ToString() + "m";
        return result;
    }

    public IActionResult SeancesList()
    {
        Render_user();
        SeancesListVM seances = new SeancesListVM();
        using (var context = new Context())
        {
            var result = from s in context.seance join m in context.movies
            on s.movieid equals m.id where s.active == true
            select new
            {
                title = m.title,
                runtime = m.runtime,
                movieid = s.movieid,
                seanceid = s.id,
                time = s.time,
                price = s.price,
                number_chair = s.number_chair
            };
            foreach (var i in result)
            {
                seances.title.Add(i.title);
                seances.runtime.Add(Runtime_string(i.runtime));
                seances.movieid.Add(i.movieid);
                seances.seanceid.Add(i.seanceid);
                seances.time.Add(i.time.Value.ToLocalTime());
                seances.price.Add(i.price);
                seances.number_chair.Add(i.number_chair);
            }
        }
        seances.len = seances.seanceid.Count();
        return View("SeancesList", seances);
    }

    public IActionResult Tickets(int seance)
    {
        Render_user();
        TicketsVM tickets = new TicketsVM();
        using (var context = new Context())
        {
            var result = from t in context.tickets where t.seanceid == seance
            select new
            {
                id = t.id,
                chair = t.chair,
                userid = t.userid,
            };
            foreach (var i in result)
            {
                tickets.id.Add(i.id);
                tickets.chair.Add(i.chair);
                tickets.userid.Add(i.userid);
            }
        }
        tickets.len = tickets.id.Count();
        return View("Tickets", tickets);
    }

    [Authorize]
    [HttpPost]
    public IActionResult BuyTickets(int[] tickets)
    {
        int user = Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
        using (var context = new Context())
        {
            var result = from t in context.tickets where tickets.Contains(t.id) select t;
            foreach(var i in result)
            {
                i.userid = user;
            }
            context.SaveChanges();
        }
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public IActionResult ReturnTicket(int ticket)
    {
        int user = Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
        using (var context = new Context())
        {
            var result = from t in context.tickets where t.id == ticket && t.userid == user select t;
            if (result.Count() != 0)
            {
                foreach(var i in result)
                {
                    i.userid = null;
                }
                context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Denied", "Access");
            }
        }
        return RedirectToAction("Profile", "User");
    }
}