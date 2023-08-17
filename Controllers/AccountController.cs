using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace movies.Controllers;

public class AccountController : BaseController
{
    [Authorize(Policy="Admin")]
    public IActionResult Revenue()
    {
        Render_user();
        RevenueVM revenue = new RevenueVM();
        using (var context = new Context())
        {
            var result = from r in context.revenue join m in context.movies
            on r.movieid equals m.id select new
            {
                movieid = r.movieid,
                title = m.title,
                revenue = r.revenue
            };
            foreach (var i in result)
            {
                revenue.movieid.Add(i.movieid);
                revenue.title.Add(i.title);
                revenue.revenue.Add(i.revenue);
            }
            revenue.len = revenue.movieid.Count();
            for(int i=0; i < revenue.len; i++)
            {
                var result2 = from s in context.seance where s.movieid == revenue.movieid[i]
                select s;
                var total = result2.Sum(r => r.price * r.number_chair);
                long sum = total.Value;
                revenue.potential_rev.Add(sum);
            }
            revenue.sum_rev = 0;
            revenue.sum_pot = 0;
            revenue.sum_sub = 0;
            for(int i=0; i < revenue.len; i++)
            {
                revenue.sum_rev += (long) revenue.revenue[i];
                revenue.sum_pot += revenue.potential_rev[i];
                revenue.sub.Add(revenue.potential_rev[i] - (long) revenue.revenue[i]);
                revenue.sum_sub += revenue.sub[i];
            }
        }
        return View("Revenue", revenue);
    }
}