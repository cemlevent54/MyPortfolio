using api.Models;

namespace api.Interfaces
{
    public interface ILanguageInterface
    {
        Task<IEnumerable<ForeignLanguages>> GetLanguages();
        Task<ForeignLanguages> GetLanguageById(int id);
        Task<ForeignLanguages> AddLanguage(ForeignLanguages language);
        Task<ForeignLanguages> UpdateLanguage(ForeignLanguages language);
        Task<ForeignLanguages> DeleteLanguage(ForeignLanguages language);
        Task<ForeignLanguages> AddUserToLanguage(int lang_id, string user_id);
        Task<ForeignLanguages> RemoveUserFromLanguage(int lang_id, string user_id);
        Task<IEnumerable<AppUsers>> GetUsersOfLanguages(int lang_id);
        Task<IEnumerable<ForeignLanguages>> GetLanguagesOfUser(string user_id);
    }
}
