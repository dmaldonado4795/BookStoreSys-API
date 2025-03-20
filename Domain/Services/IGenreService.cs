using BookStoreSys_API.Domain.Models;

namespace BookStoreSys_API.Domain.Services
{
    public interface IGenreService
    {
        Task<List<GenreModel>> GetAll();

        Task<GenreModel?> GetById(int id);

        Task<GenreModel?> GetByName(string name);

        Task<GenreModel> Save(GenreModel model);

        Task<GenreModel> Update(GenreModel model);
    }
}
