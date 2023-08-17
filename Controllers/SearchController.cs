using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using Microsoft.EntityFrameworkCore;

namespace movies.Controllers;

public class SearchController : BaseController
{
    public IActionResult Search(string search_value, string search_what)
    {
        Render_user();
        if (search_what == "actor")
        {
            SearchActorVM search = new SearchActorVM();
            using (var context = new Context())
            {
                var result = from a in context.actors 
                where EF.Functions.Like(a.name.ToLower(), "%"+search_value.ToLower()+"%") select a;
                foreach (var i in result)
                {
                    search.name.Add(i.name);
                    if(i.gender == 1) search.gender.Add("Female");
                    else search.gender.Add("Male");
                    search.id.Add(i.id);
                }
            }
            search.len = search.id.Count();
            
            return View("SearchActor", search);
        }
        else if(search_what == "worker")
        {
            SearchWorkerVM search = new SearchWorkerVM();
            using (var context = new Context())
            {
                var result = from a in context.workers 
                where EF.Functions.Like(a.name.ToLower(), "%"+search_value.ToLower()+"%") select a;
                foreach (var i in result)
                {
                    search.name.Add(i.name);
                    if(i.gender == 1) search.gender.Add("Female");
                    else search.gender.Add("Male");
                    search.id.Add(i.id);
                }
            }
            search.len = search.id.Count();
            
            return View("SearchWorker", search);
        }
        else if(search_what == "movie")
        {
            SearchMovieVM search = new SearchMovieVM();
            using (var context = new Context())
            {
                var result = from a in context.movies 
                where EF.Functions.Like(a.title.ToLower(), "%"+search_value.ToLower()+"%") select a;
                foreach (var i in result)
                {
                    search.title.Add(i.title);
                    search.genres.Add(i.genres);
                    search.date.Add(i.date);
                    search.vote_avg.Add(i.vote_avg);
                    search.id.Add(i.id);
                }
            }
            search.len = search.id.Count();
            
            return View("SearchMovie", search);
        }
        else 
        {
            return View("SearchAll");
        }
    }

    [HttpGet]
    public IActionResult ExSearch()
    {
        Render_user();
        return View();
    }

    [HttpPost]
    public IActionResult ExSearch(ExSearchVM search)
    {
        if (search.radio_str is null)
        {
            return RedirectToAction("Search", "Search", new {search_value = search.search_value});    
        }
        else
        {
            return RedirectToAction("Search", "Search", new {search_value = search.search_value,
            search_what = search.radio_str});   
        }
    }
}