using System.Threading.Tasks;
using OneApiApp.Models;

namespace OneApiApp.Services.Interface
{
    public interface IBingSearchService 
    {
        Task<SearchResult> Search(string keywords, string url);
    }
}
