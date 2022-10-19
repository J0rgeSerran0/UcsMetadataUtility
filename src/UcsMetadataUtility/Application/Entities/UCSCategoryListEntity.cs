namespace UcsMetadataUtility.Application.Entities
{
    /// <summary>
    /// Entity to get the raw data from the CSV file
    /// </summary>
    public class UCSCategoryListEntity
    {
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string CatId { get; set; }
        public string CatShort { get; set; }
        public string Explanations { get; set; }
        public string Synonyms { get; set; }
    }
}