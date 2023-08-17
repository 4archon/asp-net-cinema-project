using Microsoft.EntityFrameworkCore;

class Context: DbContext
{
    public Context(bool create)
    {
        if(create)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }

    public Context(){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=movies;username=yourusername;password=1");
    }

    public DbSet<Movies>? movies {get; set;}
    public DbSet<Actors>? actors {get; set;}
    public DbSet<Characters>? characters {get; set;}
    public DbSet<Workers>? workers {get; set;}
    public DbSet<Crew>? crew {get; set;}
    public DbSet<Revenue>? revenue {get; set;}
    public DbSet<Seance>? seance {get; set;}
    public DbSet<Tickets>? tickets {get; set;}
    public DbSet<Users>? users {get; set;}
}