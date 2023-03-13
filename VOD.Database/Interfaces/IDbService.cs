namespace VOD.Database.Interfaces;

public interface IDbService
{
    Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class where TDto : class;

    Task<List<TDto>> GetAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class;

    Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class;

    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity;

    Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class;

    void Update<TEntity, TDto>(TDto dto, int id) where TEntity : class, IEntity where TDto : class;

    Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;

    bool Delete<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class, IReferenceEntity where TDto : class;

    Task Include<TEntity>() where TEntity : class;

    string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity;

    Task<bool> SaveChangesAsync();

    void ClearTracker();
}
