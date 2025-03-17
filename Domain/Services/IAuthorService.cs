using BookStoreSys_API.Domain.Model;

namespace BookStoreSys_API.Domain.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorModel>> GetAll();

        Task<AuthorModel?> GetById(int id);

        Task<AuthorModel?> GetByName(string name);

        Task<AuthorModel> Save(AuthorModel model);

        Task<AuthorModel> Update(AuthorModel model);
    }
}
