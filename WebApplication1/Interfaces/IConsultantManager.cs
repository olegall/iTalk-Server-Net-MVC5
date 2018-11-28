using System.Collections.Specialized;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.ViewModels;
using System.Web;

namespace WebApplication1.Interfaces
{
    public interface IConsultantManager
    {
        IEnumerable<PrivateConsultantVM> GetPrivateVMs(int offset,
                                                       int limit,
                                                       long subcategoryId,
                                                       bool free,
                                                       bool onlyFavorite,
                                                       string filter);

        IEnumerable<JuridicConsultantVM> GetJuridicsVMs(int offset,
                                                        int limit,
                                                        long subcategoryId,
                                                        bool free,
                                                        bool onlyFavorite,
                                                        string filter);
        Task<PrivateConsultant> GetPrivateAsync(long id);
        Task<JuridicConsultant> GetJuridicAsync(long id);
        IList<long> GetAvailableTimes(long time);
        long CreatePrivateAsync(string name,
                                string surname,
                                string patronymic,
                                string phone,
                                string email);
        Task CreatePrivateImagesAsync(HttpFileCollection files, long id);
        Task CreateJuridicImagesAsync(HttpFileCollection files, long id);
        long CreateJuridic(string ltdtitle,
                                  string ogrn,
                                  string inn,
                                  string phone,
                                  string siteurl);

        Task UpdatePrivateFieldsAsync(long id, NameValueCollection formData);
        Task UpdateJuridicFieldsAsync(long id, NameValueCollection formData);
        Task CreateImageWhenUpdateAsync(HttpRequest request, long consId);
        Task DeleteImageAsync(long id);
        Task UpdateRatingAsync(long id, decimal rating);
    }
}