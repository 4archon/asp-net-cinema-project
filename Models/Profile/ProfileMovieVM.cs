public class ProfileMovieVM
{
    public string? genres;
    public string? homepage;
    public int? budget;
    public string? language;
    public string? overview;
    public string? companies;
    public string? countries;
    public DateOnly? date;
    public long? revenue;
    public float? runtime;
    public string? status;
    public string? tagline;
    public string? title;
    public float? vote_avg;
    public int? vote_count;
    public List<int> actor_id = new List<int>();
    public List<string?> name_actor = new List<string?>();
    public List<string?> character = new List<string?>();
    public int fisrt_len;
    public List<int> worker_id = new List<int>();
    public List<string?> name_worker = new List<string?>();
    public List<string?> job = new List<string?>();
    public List<string?> department = new List<string?>();
    public int second_len;
}