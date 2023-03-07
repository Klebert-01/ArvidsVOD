namespace VOD.Database.Entities;

public class Genre : IEntity
{
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Film>? Films { get; set; }
}
