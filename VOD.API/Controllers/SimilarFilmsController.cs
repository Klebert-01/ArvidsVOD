using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VOD.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SimilarFilmsController : ControllerBase
{
    private readonly IDbService _db;
    public SimilarFilmsController(IDbService dbService)
    {
        _db = dbService;
    }
    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            var simFilms = await _db.GetAsync<SimilarFilm, SimilarFilmDTO>();
            if (simFilms is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(simFilms);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpGet("{filmId}")]
    public async Task<IResult> Get(int filmId)
    {
        try
        {
            await _db.Include<Film>();
            await _db.Include<FilmGenre>();
            var films = await _db.GetAsync<Film, FilmDTO>();
                                                

            var simFilmQuery = from film in films
                              from sim in film.SimilarFilms
                              where sim.ParentFilmId == filmId || sim.SimilarFilmId == filmId
                              select sim;

            var resultfilms = new List<FilmDTO>();
            foreach (var simFilm in simFilmQuery)
            {
                resultfilms.AddRange(films.Where(v => v.Id.Equals(simFilm.ParentFilmId)));
                resultfilms.AddRange(films.Where(v => v.Id.Equals(simFilm.SimilarFilmId)));
            }

            resultfilms.RemoveAll(v => v.Id.Equals(filmId));
            return Results.Ok(resultfilms.ToHashSet().ToList());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] SimilarFilmDTO dto)
    {
        try
        {
            if (dto is null)
            {
                return Results.BadRequest();
            }
            var similar = await _db.AddAsync<SimilarFilm, SimilarFilmDTO>(dto);
            var success = await _db.SaveChangesAsync();
            if (success is false)
            {
                return Results.BadRequest("Could not add the similar video connection.");
            }
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IResult> Delete([FromBody] SimilarFilmDTO dto)
    {
        try
        {
            var success = _db.Delete<SimilarFilm, SimilarFilmDTO>(dto);
            if (success is false)
            {
                return Results.NotFound("Could not find matching Entity");
            }
            success = await _db.SaveChangesAsync();
            if (success is false)
            {
                return Results.BadRequest();
            }
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
