using BookStoreSys_API.Domain.Models;
using BookStoreSys_API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreSys_API.Domain.Services.Impl
{
    public class BookServiceImpl(
        BookstoreContext context,
        ILogger<BookServiceImpl> logger) : IBookService
    {
        private readonly BookstoreContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly ILogger<BookServiceImpl> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<List<BookModel>> GetAll()
        {
            try
            {
                return await _context.Books.AsNoTracking()
                    .Include(options => options.Author)
                    .ThenInclude(options => options!.Nationality)
                    .Include(options => options.Genre)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all books.");
                throw new Exception("An error occurred while retrieving all books.", ex);
            }
        }

        public async Task<BookModel?> GetById(int id)
        {
            try
            {
                return await _context.Books.AsNoTracking()
                    .Include(options => options.Author)
                    .ThenInclude(options => options!.Nationality)
                    .Include(options => options.Genre)
                    .FirstOrDefaultAsync(options => options.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving book with ID {id}.");
                throw new Exception($"An error occurred while retrieving book with ID {id}.", ex);
            }
        }

        public async Task<BookModel?> GetByTitle(string title)
        {
            try
            {
                return await _context.Books.AsNoTracking()
                    .Include(options => options.Author)
                    .ThenInclude(options => options!.Nationality)
                    .Include(options => options.Genre)
                    .FirstOrDefaultAsync(options => options.Title == options.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving author with title {title}.");
                throw new Exception($"An error occurred while retrieving author with title {title}.", ex);
            }
        }

        public async Task<BookModel> Save(BookModel model)
        {
            try
            {
                _context.Books.Add(model);
                await _context.SaveChangesAsync();

                var savedBook = await _context.Books
                    .Include(options => options.Author)
                    .ThenInclude(options => options!.Nationality)
                    .Include(options => options.Genre)
                    .FirstOrDefaultAsync(options => options.Id == model.Id);

                return savedBook!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving a book.");
                throw new Exception("An error occurred while saving the book.", ex);
            }
        }

        public async Task<BookModel> Update(BookModel model)
        {
            try
            {
                var book = await _context.Books
                    .Include(options => options.Author)
                    .ThenInclude(options => options!.Nationality)
                    .Include(options => options.Genre)
                    .FirstOrDefaultAsync(options => options.Id == model.Id)
                    ?? throw new KeyNotFoundException($"Book with ID {model.Id} was not found.");

                _context.Entry(book).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating book with ID {Id}", model.Id);
                throw new Exception($"An error occurred while updating book with ID {model.Id}.", ex);
            }
        }
    }
}
