using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UcsMetadataUtility.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string FileNameText { get; set; }
        public string DescriptionText { get; set; }
        public string SeparatorBetweenFields { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;

            FileNameText = config["UCSMetadata:FileNameText"];
            DescriptionText = config["UCSMetadata:DescriptionText"];
            SeparatorBetweenFields = config["UCSMetadata:SeparatorBetweenFields"];
        }

        public void OnGet()
        {

        }
    }
}