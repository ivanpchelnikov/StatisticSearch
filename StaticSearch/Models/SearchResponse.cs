
using System.Text.Json.Serialization;

namespace OneApiApp.Models
{
    public class SearchResponse
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("google")]
        public string Google { get; set; }
        [JsonPropertyName("bing")]
        public string Bing { get; set; }
        [JsonPropertyName("keywords")]
        public string Keywords { get; set; }
    }
}
