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
            var simVids = await _db.GetAsync<SimilarFilm, SimilarFilmDTO>();
            if (simVids is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(simVids);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpGet("{videoId}")]
    public async Task<IResult> Get(int videoId, bool freeOnly = false)
    {
        try
        {
            await _db.Include<Film>();
            await _db.Include<FilmGenre>();
            var videos = freeOnly ? await _db.GetAsync<Film, FilmDTO>(c => c.Free.Equals(freeOnly))
                                                : await _db.GetAsync<Film, FilmDTO>();

            var simVidQuery = from vid in videos
                              from sim in vid.SimilarFilms
                              where sim.FilmId == videoId || sim.SimilarFilmId == videoId
                              select sim;

            var resultVideos = new List<FilmDTO>();
            foreach (var simVideo in simVidQuery)
            {
                resultVideos.AddRange(videos.Where(v => v.Id.Equals(simVideo.FilmId)));
                resultVideos.AddRange(videos.Where(v => v.Id.Equals(simVideo.SimilarFilmId)));
            }

            resultVideos.RemoveAll(v => v.Id.Equals(videoId));
            return Results.Ok(resultVideos.ToHashSet().ToList());
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
