using System.ComponentModel.DataAnnotations.Schema;

[Table("movies")]
class Movies
{
    public int id {get;set;}
    public string? genres {get; set;}
    public string? homepage {get; set;}
    public int? budget {get;set;}
    public string? language {get; set;}
    public string? original_title {get; set;}
    public string? overview {get; set;}
    public string? companies {get; set;}
    public string? countries {get; set;}
    public DateOnly? date {get; set;}
    public long? revenue {get; set;}
    public float? runtime {get; set;}
    public string? status {get; set;}
    public string? tagline {get; set;}
    public string? title {get; set;}
    public float? vote_avg {get; set;}
    public int? vote_count {get; set;}
}

[Table("actors")]
class Actors
{
    public int id {get; set;}
    public string? name {get; set;}
    public int? gender {get; set;}
}

[Table("characters")]
class Characters
{
    public int id {get; set;}
    public int? movieid {get; set;}
    public Movies? movie {get; set;}
    public string? character {get; set;}
    public int? actorid {get; set;}
    public Actors? actor {get; set;}
}

[Table("workers")]
class Workers
{
    public int id {get; set;}
    public int? gender {get; set;}
    public string? name {get; set;}
}

[Table("crew")]
class Crew
{
    public int id {get; set;}
    public int? movieid {get; set;}
    public Movies? movie {get; set;}
    public string? department {get; set;}
    public string? job {get; set;}
    public int? workerid {get; set;}
    public Workers? worker {get; set;}
}

[Table("revenue")]
class Revenue
{
    public int id {get; set;}
    public int movieid {get; set;}
    public Movies? movie {get; set;}
    public long? revenue {get; set;}
}

[Table("seance")]
class Seance
{
    public int id {get; set;}
    public int movieid {get; set;}
    public Movies? movie {get; set;}
    public bool? active {get; set;}
    public DateTime? time {get; set;}
    public int? price {get; set;}
    public int? number_chair {get; set;}
}

[Table("tickets")]
class Tickets
{
    public int id {get; set;}
    public int seanceid {get; set;}
    public Seance? seance {get; set;}
    public int? chair {get; set;}
    public int? userid {get; set;}
    public Users? user {get; set;}
}

[Table("users")]
class Users
{
    public int id {get; set;}
    public int role {get; set;}
    public string? name {get; set;}
    public string? surname {get; set;}
    public DateOnly? birthday {get; set;}
    public string? email {get; set;}
    public string? password {get; set;}
}