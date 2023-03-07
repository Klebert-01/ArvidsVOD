using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmGenresController : ControllerBase
    {
        private readonly IDbService _db;

        public FilmGenresController(IDbService dbService)
        {
            _db = dbService;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                var videoGenres = await _db.GetAsync<FilmGenre, FilmGenreDTO>();
                if (videoGenres is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(videoGenres);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmGenreDTO dto)
        {
            try
            {
                if (dto is null)
                {
                    return Results.BadRequest();
                }
                var similar = await _db.AddAsync<FilmGenre, FilmGenreDTO>(dto);
                var success = await _db.SaveChangesAsync();
                if (success is false)
                {
                    return Results.BadRequest("Could not add the genre connection.");
                }
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IResult> Delete([FromBody] FilmGenreDTO dto)
        {
            try
            {
                var success = _db.Delete<FilmGenre, FilmGenreDTO>(dto);
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
}
