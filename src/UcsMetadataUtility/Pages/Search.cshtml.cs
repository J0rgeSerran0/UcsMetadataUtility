using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;
using UcsMetadataUtility.Application.Services;

namespace UcsMetadataUtility.Pages
{
    public class SearchModel : PageModel
    {
        private readonly FeedDataService _feedDataService;

        public SearchModel(FeedDataService feedDataService)
        {
            _feedDataService = feedDataService;
        }

        public void OnGet()
        {
        }

        public JsonResult OnGetData(string by)
        {
            var term = by.Trim();

            if (String.IsNullOrWhiteSpace(term))
                return new JsonResult("");

            var collection = _feedDataService.Subcategories
                .Where(x => x.Name.IndexOf(by, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            x.CatId.IndexOf(by, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            x.CatShort.IndexOf(by, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            x.Explanations.IndexOf(by, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            x.Synonyms.IndexOf(by, StringComparison.OrdinalIgnoreCase) >= 0).ToList().Distinct();

            var result = new List<object>();

            foreach (var item in collection)
            {
                var categoryName = _feedDataService.Categories.Where(x => x.Id == item.CategoryId).SingleOrDefault()?.Name;

                dynamic data = new ExpandoObject();
                data.CategoryName = categoryName;
                data.SubcategoryName = item.Name;
                data.CatId = item.CatId;
                data.CatShort = item.CatShort;
                data.Explanations = item.Explanations;
                data.Synonyms = item.Synonyms;

                result.Add(data);
            }

            return new JsonResult(result.ToList());
        }
    }
}