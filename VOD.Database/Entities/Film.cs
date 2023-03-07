namespace VOD.Database.Entities;

public class Film : IEntity
{
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    [Required]
    public DateTime Released { get; set; }
    [Required]
    public bool Free { get; set; } = true;
    [Required, MaxLength(200)]
    public string Description { get; set; } = string.Empty;
    [Required,MaxLength(1024)]
    public string FilmUrl { get; set; } = string.Empty;

    public int DirectorId { get; set; }
    public Director? Director { get; set; }
    public ICollection<Genre>? Genres { get; set; }
    public ICollection<SimilarFilm>? SimilarFilms { get; set; }

}
