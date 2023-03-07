namespace VOD.Common.DTOs;

public class GenreDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
public class CreateGenreDTO
{
    public string? Name { get; set; }
}

public class FullGenreDTO : GenreDTO
{
    public virtual List<FilmDTO> Films { get; set; } = new();
}
