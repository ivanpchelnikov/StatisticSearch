using System.Threading.Tasks;
using OneApiApp.Models;

namespace OneApiApp.Services.Interface
{
    public interface IGoogleSearchService
    {
        Task<SearchResult> Search(string searchUrl, string url);
    }
}
