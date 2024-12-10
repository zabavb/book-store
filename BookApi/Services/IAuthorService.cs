namespace BookApi.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(Guid authorId);
        Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto);
        Task<bool> UpdateAuthorAsync(Guid authorId, AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(Guid authorId);
    }
}
