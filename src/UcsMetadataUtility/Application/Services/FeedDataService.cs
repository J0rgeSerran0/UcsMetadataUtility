using Microsoft.VisualBasic.FileIO;
using UcsMetadataUtility.Application.Entities;

namespace UcsMetadataUtility.Application.Services
{
    public class FeedDataService
    {
        private readonly string _fileName = "UCSCategoryList.csv";

        public List<CategoryEntity> Categories { get => _categories; }
        public List<SubcategoryEntity> Subcategories { get => _subcategories; }

        private List<CategoryEntity> _categories = null;
        private List<SubcategoryEntity> _subcategories = null;

        public FeedDataService()
        {
            FeedFile();
        }

        private void FeedFile()
        {
            // Get RAW Data
            var ucsCategoriesList = new List<UCSCategoryListEntity>();

            using (var textFieldParser = new TextFieldParser($"Data/{_fileName}"))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(";");
                while (!textFieldParser.EndOfData)
                {
                    string[]? fields = textFieldParser.ReadFields();
                    if (fields != null)
                    {
                        var ucsCategoryList = new UCSCategoryListEntity()
                        { 
                            Category = fields[0],
                            Subcategory = fields[1],
                            CatId = fields[2],
                            CatShort = fields[3],
                            Explanations = fields[4],
                            Synonyms = fields[5]
                        }; 
                        ucsCategoriesList.Add(ucsCategoryList);
                    }
                }
            }

            // Prepare Data for the Web
            _categories = new List<CategoryEntity>();
            _subcategories = new List<SubcategoryEntity>();

            // Category
            foreach (var item in ucsCategoriesList)
            {
                var exists = _categories.Where(x => x.Name == item.Category).SingleOrDefault();

                // CategoryId
                var categoryId = String.Empty;

                if (exists == null)
                {
                    categoryId = Guid.NewGuid().ToString("N");
                    var category = new CategoryEntity()
                    {
                        Id = categoryId, 
                        Name = item.Category.Trim()
                    };

                    _categories.Add(category);
                }
                else
                {
                    categoryId = exists.Id;
                }

                // SubcategoryId
                var subcategoryId = Guid.NewGuid().ToString("N");

                //var subcategory = new SubcategoryEntity(subcategoryId, categoryId, item.Subcategory.Trim().TrimEnd('.'), item.CatId.Trim().TrimEnd('.'), item.CatShort.Trim().TrimEnd('.'), item.Explanations.Trim().TrimEnd('.'), item.Synonyms.Trim().TrimEnd('.'));
                var subcategory = new SubcategoryEntity()
                {
                    Id = subcategoryId,
                    CategoryId = categoryId,
                    Name = item.Subcategory.Trim().TrimEnd('.'),
                    CatId = item.CatId.Trim().TrimEnd('.'),
                    CatShort = item.CatShort.Trim().TrimEnd('.'),
                    Explanations = item.Explanations.Trim().TrimEnd('.'),
                    Synonyms = item.Synonyms.Trim().TrimEnd('.')
                }; 
                _subcategories.Add(subcategory);
            }
        }
    }
}