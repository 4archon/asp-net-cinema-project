using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using movies.Models;
using Microsoft.EntityFrameworkCore;

namespace movies.Controllers;

public class ProfileController : BaseController
{
    public IActionResult Actor(int id)
    {
        Render_user();
        ProfileActorVM profile = new ProfileActorVM();
        using (var context = new Context())
        {
            var result = from c in context.characters join m in context.movies
            on c.movieid equals m.id where c.actorid == id 
            select new 
            {
                title = m.title,
                movie_id = c.movieid,
                character = c.character
            };
            foreach(var i  in result)
            {
                profile.title.Add(i.title);
                profile.movie_id.Add(i.movie_id);
                profile.character.Add(i.character);
            }
            var result2 = from a in context.actors where a.id == id select a;
            foreach (var i in result2)
            {
                profile.name = i.name;
                int? gender = i.gender;
                if (gender == 1) profile.gender = "Female";
                else profile.gender = "Male";
                break;    
            }
            // profile.name = result2.First().name;
            // int? gender = result2.First().gender;
            // if (gender == 1) profile.gender = "Female";
            // else profile.gender = "Male";
        }
        profile.len = profile.movie_id.Count();
        return View("Actor", profile);
    }

    public IActionResult Worker(int id)
    {
        Render_user();
        ProfileWorkerVM profile = new ProfileWorkerVM();
        using (var context = new Context())
        {
            var result = from c in context.crew join m in context.movies
            on c.movieid equals m.id where c.workerid == id
            select new
            {
                title = m.title,
                movie_id = c.movieid,
                department = c.department,
                job = c.job
            };
            foreach (var i in result)
            {
                profile.title.Add(i.title);
                profile.movie_id.Add(i.movie_id);
                profile.department.Add(i.department);
                profile.job.Add(i.job);
            }
            var result2 = from w in context.workers where w.id == id select w;
            foreach (var i in result2)
            {
                profile.name = i.name;
                int? gender = i.gender;
                if (gender == 1) profile.gender = "Female";
                else profile.gender = "Male";
                break;
            }
        }
        profile.len = profile.movie_id.Count();
        return View("Worker", profile);
    }

    public IActionResult Movie(int id)
    {
        Render_user();
        ProfileMovieVM profile = new ProfileMovieVM();
        using (var context = new Context())
        {
            var result = from m in context.movies where m.id == id select m;
            foreach (var i in result)
            {
                profile.budget = i.budget;
                profile.companies = i.companies;
                profile.countries = i.countries;
                profile.date = i.date;
                profile.genres = i.genres;
                profile.homepage = i.homepage;
                profile.language = i.language;
                profile.overview = i.overview;
                profile.revenue = i.revenue;
                profile.runtime = i.runtime;
                profile.status = i.status;
                profile.tagline = i.tagline;
                profile.title = i.title;
                profile.vote_avg = i.vote_avg;
                profile.vote_count = i.vote_count;
            }
            var result2 = from a in context.actors join c in context.characters
            on a.id equals c.actorid join m in context.movies on c.movieid equals m.id
            where m.id == id select new
            {
                actor_id = a.id,
                name = a.name,
                character = c.character
            };
            foreach (var i in result2)
            {
                profile.actor_id.Add(i.actor_id);
                profile.name_actor.Add(i.name);
                profile.character.Add(i.character);
            }
            var result3 = from w in context.workers join c in context.crew
            on w.id equals c.workerid join m in context.movies on c.movieid equals m.id
            where m.id == id select new
            {
                worker_id = w.id,
                name = w.name,
                job = c.job,
                department = c.department
            };
            foreach (var i in result3)
            {
                profile.worker_id.Add(i.worker_id);
                profile.name_worker.Add(i.name);
                profile.job.Add(i.job);
                profile.department.Add(i.department);
            }
        }
        profile.fisrt_len = profile.actor_id.Count();
        profile.second_len = profile.worker_id.Count();
        return View("Movie", profile);
    }   
}