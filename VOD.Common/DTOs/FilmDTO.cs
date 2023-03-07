namespace VOD.Common.DTOs;

public class FilmDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? FilmUrl { get; set; }
    public DateTime Released { get; set; }
    public bool Free { get; set; } = true;
    public int DirectorId { get; set; }
    public virtual DirectorDTO? Director { get; set; }
    public virtual List<GenreDTO> Genres { get; set; } = new();
    public virtual List<SimilarFilmDTO> SimilarFilms { get; set; } = new();
}
public class CreateFilmDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? FilmUrl { get; set; }
    public DateTime Released { get; set; }
    public bool Free { get; set; } = true;
    public int? DirectorId { get; set; }
}
public class EditFilmDTO : CreateFilmDTO
{
    public int Id { get; set; }
    public virtual List<GenreDTO> Genres { get; set; } = new();
}
