using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace movies.Controllers;

public class UserController : BaseController
{
    int AgeFromDate(DateOnly? value)
    {
        int age;
        DateOnly birth = (DateOnly) value;
        DateTime birth1 = birth.ToDateTime(new TimeOnly(0));
        DateTime now = DateTime.Now;
        age = (now - birth1).Duration().Days;
        age = age / 365;
        return age;
    }

    [Authorize]
    public IActionResult Profile()
    {
        Render_user();
        UserProfileVM profile = new UserProfileVM();
        int user = Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
        using (var context = new Context())
        {
            var result = from t in context.tickets
            join s in context.seance on t.seanceid equals s.id
            join m in context.movies on s.movieid equals m.id where t.userid == user
            select new
            {
                ticketid = t.id,
                chair = t.chair,
                // seanceid = t.seanceid,
                time = s.time,
                title = m.title,
                modieid = m.id
            };

            foreach (var i in result)
            {
                profile.ticketid.Add(i.ticketid);
                profile.chair.Add(i.chair);
                // profile.seanceid.Add(i.seanceid);
                profile.time.Add(i.time.Value.ToLocalTime());
                profile.title.Add(i.title);
                profile.movieid.Add(i.modieid);
            }

            var result2 = from u in context.users where u.id == user select u;

            foreach (var i in result2)
            {
                profile.birthday = i.birthday;
                profile.email = i.email;
                profile.name = i.name;
                profile.surname = i.surname;
                break;
            }
        }
        profile.len = profile.ticketid.Count();
        profile.age = AgeFromDate(profile.birthday);
        return View("UserProfile", profile);
    }   
}