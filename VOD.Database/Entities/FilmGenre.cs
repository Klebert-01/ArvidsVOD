namespace VOD.Database.Entities;

public class FilmGenre : IReferenceEntity
{
    public int FilmId { get; set; }
    public int GenreId { get; set; }

    public virtual Film? Film { get; set; }
    public virtual Genre? Genre { get; set; }

}
