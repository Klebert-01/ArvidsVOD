namespace VOD.Database.Services;

// här ligger dom generiska(oberoende av entitetsklass osv) som vi sen kallar på i våra controllers
public class DbService : IDbService
{
    private readonly VODContext _db;
    private readonly IMapper _mapper;
    public DbService(VODContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    //get all
    public async Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class where TDto : class
    {
        var entities = await _db.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }
    //get one
    public async Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
    {
        var entities = await _db.Set<TEntity>().Where(expression).ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }

    public async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity
    {
        return await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
    }

    public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
    {
        var entity = await SingleAsync(expression);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity
    {
        return await _db.Set<TEntity>().AnyAsync(expression);
    }

    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _db.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public void Update<TEntity, TDto>(TDto dto, int id) where TEntity : class, IEntity where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity.Id = id;
        _db.Set<TEntity>().Update(entity);
    }

    public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        try
        {
            var entity = await SingleAsync<TEntity>(e => e.Id == id);
            if (entity is null)
            {
                return false;
            }
            _db.Remove(entity);
        }
        catch (Exception)
        {
            throw;
        }
        return true;
    }

    public bool Delete<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class, IReferenceEntity where TDto : class
    {
        try
        {
            var entity = _mapper.Map<TReferenceEntity>(dto);
            if (entity is null)
            {
                return false;
            }
            _db.Remove(entity);
        }
        catch (Exception)
        {
            throw;
        }
        return true;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }
   
    public async Task Include<TEntity>() where TEntity : class

    {
        var propertyNames = _db.Model.FindEntityType(typeof(TEntity))?.GetNavigations().Select(e => e.Name);

        if (propertyNames is null) return;

        foreach (var name in propertyNames)
            _db.Set<TEntity>().Include(name).Load();

    }

    public string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        return $"/{typeof(TEntity).Name.ToLower()}s/{entity.Id}";
    }

    public void ClearTracker()
    {
        _db.ChangeTracker.Clear();
    }
}
