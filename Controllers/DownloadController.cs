using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace movies.Controllers;

public class DownloadController : BaseController
{
    [Authorize]
    public IActionResult Ticket(int ticket)
    {
        int user_id = Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
        string user_name = new string("");
        string user_surname = new string("");
        string chair = new string("");
        string movie_title = new string("");
        string price = new string("");
        string time = new string("");
        using (var context = new Context())
        {
            var result = from t in context.tickets where t.id == ticket && t.userid == user_id
            select t;
            if (result.Count() != 1)
            {
                Console.WriteLine(user_id);
                return RedirectToAction("Denied", "Access");
            }
            else
            {
                var result2 = from r in result join s in context.seance
                on r.seanceid equals s.id join m in context.movies
                on s.movieid equals m.id join u in context.users
                on r.userid equals u.id select new
                {
                    user_name = u.name,
                    user_surname = u.surname,
                    chair = r.chair,
                    movie_title = m.title,
                    price = s.price,
                    time = s.time
                };
                foreach(var i in result2)
                {
                    user_name = i.user_name;
                    user_surname = i.user_surname;
                    chair = i.chair.ToString();
                    movie_title = i.movie_title;
                    price = i.price.ToString();
                    time = i.time.Value.ToLocalTime().ToString();
                    break;
                }
            }
        }

        using (MemoryStream ms = new MemoryStream())
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            document.Open();
            Paragraph para1 = new Paragraph("User: " + user_name + " " + user_surname, new Font(Font.FontFamily.TIMES_ROMAN, 20));
            para1.Alignment = Element.ALIGN_LEFT;
            document.Add(para1);
            Paragraph para2 = new Paragraph("Movie: " + movie_title, new Font(Font.FontFamily.TIMES_ROMAN, 20));
            para2.Alignment = Element.ALIGN_LEFT;
            document.Add(para2);
            Paragraph para3 = new Paragraph("Seat: " + chair, new Font(Font.FontFamily.TIMES_ROMAN, 20));
            para3.Alignment = Element.ALIGN_LEFT;
            document.Add(para3);
            Paragraph para4 = new Paragraph("Price: " + price, new Font(Font.FontFamily.TIMES_ROMAN, 20));
            para4.Alignment = Element.ALIGN_LEFT;
            document.Add(para4);
            Paragraph para5 = new Paragraph("Starts at: " + time, new Font(Font.FontFamily.TIMES_ROMAN, 20));
            para5.Alignment = Element.ALIGN_LEFT;
            document.Add(para5);
            var image = iTextSharp.text.Image.GetInstance("wwwroot/files/logo.png");
            image.Alignment = Element.ALIGN_CENTER;
            document.Add(image);
            document.Close();
            writer.Close();
            var constant = ms.ToArray();
            return File(constant, "application/pdf", "ticket.pdf");
        }
    }
}