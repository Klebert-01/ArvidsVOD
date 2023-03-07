namespace VOD.Database.Entities;

public class SimilarFilm : IReferenceEntity
{
    public int ParentFilmId { get; set; }
    public int SimilarFilmId { get; set; }
    public virtual Film? Film { get; set; }
    [ForeignKey("SimilarFilmId")]
    public virtual Film? Similar { get; set; }

}
