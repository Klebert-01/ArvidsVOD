
namespace VOD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDbService _db;

        public DirectorsController(IDbService dbService)
        {
            _db = dbService;
        }
        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {

                var directors = await _db.GetAsync<Director, DirectorDTO>();
                if (directors is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(directors);

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
                var director = await _db.SingleAsync<Director, DirectorDTO>(d => d.Id == id);
                if (director is null)
                {
                    return Results.NotFound("No matching id found.");
                }
                return Results.Ok(director);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] CreateDirectorDTO dto)
        {
            try
            {
                if (dto is null)
                {
                    return Results.BadRequest();
                }
                var director = await _db.AddAsync<Director, CreateDirectorDTO>(dto);
                var success = await _db.SaveChangesAsync();
                if (success is false)
                {
                    return Results.BadRequest();
                }
                return Results.Created(_db.GetURI(director), director);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] DirectorDTO dto)
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
                var exists = await _db.AnyAsync<Director>(v => v.Id.Equals(id));
                if (exists is false)
                {
                    return Results.NotFound("Could not find an Entity with id: " + id);
                }
                _db.Update<Director, DirectorDTO>(dto, dto.Id);
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
                var success = await _db.DeleteAsync<Director>(id);
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
