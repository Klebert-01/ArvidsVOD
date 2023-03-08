namespace VOD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IDbService _db;

        public FilmsController(IDbService dbService)
        {
            _db = dbService;
        }
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                await _db.Include<Film>();
                await _db.Include<FilmGenre>();
                List<FilmDTO>? films = await _db.GetAsync<Film, FilmDTO>();
                return Results.Ok(films);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                await _db.Include<Film>();
                await _db.Include<FilmGenre>();
                var film = await _db.SingleAsync<Film, FilmDTO>(f => f.Id == id);
                return Results.Ok(film);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] CreateFilmDTO dto)
        {
            try
            {
                if (dto is null)
                {
                    return Results.BadRequest();
                }
                var film = await _db.AddAsync<Film, CreateFilmDTO>(dto);
                var success = await _db.SaveChangesAsync();
                if (success is false)
                {
                    return Results.BadRequest();
                }
                return Results.Created(_db.GetURI(film), film);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] EditFilmDTO dto)
        {
            try
            {
                if (dto is null)
                {
                    return Results.BadRequest("No entity provided");
                }
                if (id.Equals(dto.Id) is false)
                {
                    return Results.BadRequest($"URL Id: {id} is not equal to Entity Id: {dto.Id}");
                }
                var exists = await _db.AnyAsync<Film>(f => f.Id.Equals(id));
                if (exists is false)
                {
                    return Results.NotFound("Could not find an Entity with id: " + id);
                }
                var allGenres = await _db.GetAsync<FilmGenre, FilmGenreDTO>();
                _db.ClearTracker();  //kolla på nedan clear tracker

                allGenres.Where(vg => vg.FilmId.Equals(dto.Id)).ToList().ForEach(vg => _db.Delete<FilmGenre, FilmGenreDTO>(vg));   // här är nog referenceentity problem
                await _db.SaveChangesAsync();
                _db.Update<Film, EditFilmDTO>(dto, dto.Id);
                var success = await _db.SaveChangesAsync();
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

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                await _db.Include<Film>();
                var success = await _db.DeleteAsync<Film>(id);
                if (success is false)
                {
                    return Results.NotFound("Could not find an Entity with id: " + id);
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
