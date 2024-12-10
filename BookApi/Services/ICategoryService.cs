namespace BookApi.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto CategoryDto);
        Task<CategoryDto> UpdateCategoryAsync(Guid id, CategoryDto CategoryDto);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
