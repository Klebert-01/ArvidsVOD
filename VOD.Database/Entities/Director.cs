namespace VOD.Database.Entities;

public class Director : IEntity
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Film>? Films { get; set; }

}
