namespace VOD.Database.Contexts;

public class VODContext : DbContext
{
    //Dbsets
    public virtual DbSet<Film> Films { get; set; } = null!;
    public virtual DbSet<SimilarFilm> SimilarFilms { get; set; } = null!;
    public virtual DbSet<Director> Directors { get; set; } = null!;
    public virtual DbSet<Genre> Genres { get; set; } = null!;
    public virtual DbSet<FilmGenre> FilmGenres { get; set; } = null!;

    // konstruktor
    public VODContext(DbContextOptions<VODContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Composit Keys */
        modelBuilder.Entity<SimilarFilm>().HasKey(ci => new { ci.ParentFilmId, ci.SimilarFilmId });
        modelBuilder.Entity<FilmGenre>().HasKey(ci => new { ci.FilmId, ci.GenreId });

        /* Configuring related tables for the Film table*/
        modelBuilder.Entity<Film>(entity =>
        {
            entity
                // For each film in the Film Entity,
                // reference relatred films in the SimilarFilms entity
                // with the ICollection<SimilarFilms>
                .HasMany(d => d.SimilarFilms)
                .WithOne(p => p.Film)
                .HasForeignKey(d => d.ParentFilmId)
                // To prevent cycles or multiple cascade paths.
                // Neded when seeding similar films data.
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Configure a many-to-many realtionship between genres
            // and films using the FilmGenre connection entity.
            entity.HasMany(d => d.Genres)
                  .WithMany(p => p.Films)
                  .UsingEntity<FilmGenre>()
                  // Specify the table name for the connection table
                  // to avoid duplicate tables (FilmGenre and FilmGenres)
                  // in the database.
                  .ToTable("FilmGenres");
        });
    }

}
