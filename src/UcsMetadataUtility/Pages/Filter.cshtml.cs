using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UcsMetadataUtility.Application.Services;

namespace UcsMetadataUtility.Pages
{
    public class FilterModel : PageModel
    {
        private readonly FeedDataService _feedDataService;

        public FilterModel(FeedDataService feedDataService)
        {
            _feedDataService = feedDataService;
        }

        public JsonResult OnGet(string id, string byId)
        {
            // All Categories
            if (id == "AllCategories")
                return new JsonResult(_feedDataService.Categories);

            // Subcategories By CategoryId
            if (id == "CategoryBy")
                return new JsonResult(_feedDataService.Subcategories.Where(x => x.CategoryId == byId));

            // General data from a subcategory
            if (id == "SubcategoryBy")
                return new JsonResult(_feedDataService.Subcategories.Where(x => x.Id == byId));

            return new JsonResult("");
        }
    }
}