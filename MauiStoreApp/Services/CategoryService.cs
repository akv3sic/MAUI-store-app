using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    public class CategoryService : BaseService
    {
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await GetAsync<IEnumerable<string>>("products/categories");

            // Mapping each string to a Category object
            var categoryList = new List<Category>();
            foreach (var category in categories)
            {
                categoryList.Add(new Category { Name = category });
            }

            return categoryList;
        }
    }
}
