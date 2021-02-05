using System.Threading.Tasks;
using OneApiApp.Models;

namespace OneApiApp.Services.Interface
{
    public interface IStatisticSearchService
    {
        Task<string> SearchByUrl(string searchUrl, string tagUrl);
    }
}
