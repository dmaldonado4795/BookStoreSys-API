using BookStoreSys_API.Domain.Model;

namespace BookStoreSys_API.Domain.Services
{
    public interface IBookService
    {
        Task<List<BookModel>> GetAll();

        Task<BookModel?> GetById(int id);

        Task<BookModel?> GetByTitle(string title);

        Task<BookModel> Save(BookModel model);

        Task<BookModel> Update(BookModel model);
    }
}
