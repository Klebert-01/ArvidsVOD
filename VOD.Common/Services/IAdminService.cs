namespace VOD.Common.Services
{
    public interface IAdminService
    {
        Task<List<TDto>> GetAsync<TDto>(string uri);
        Task<TDto>? SingleAsync<TDto>(string uri);
        Task<HttpResponseMessage> CreateAsync<TDto>(string uri, TDto dto);
        Task EditAsync<TDto>(string uri, TDto dto);
        Task DeleteAsync<TDto>(string uri);
        Task DeleteRefAsync<TRefDto>(string uri, TRefDto dto);
        Task CreateRefAsync<TRefDto>(string uri, TRefDto dto);
    }
}