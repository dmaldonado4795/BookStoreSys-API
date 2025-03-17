    using BookStoreSys_API.Domain.Model;
using BookStoreSys_API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreSys_API.Domain.Services.Impl
{
    public class AuthorServiceImpl : IAuthorService
    {
        private readonly BookstoreContext _context;
        private readonly ILogger<AuthorServiceImpl> _logger;

        public AuthorServiceImpl(
            BookstoreContext context,
            ILogger<AuthorServiceImpl> logger)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public async Task<List<AuthorModel>> GetAll()
        {
            try
            {
                return await _context.Authors.AsNoTracking()
                    .Include(options => options.Nationality)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all authors.");
                throw new Exception("An error occurred while retrieving all author.", ex);
            }
        }

        public async Task<AuthorModel?> GetById(int id)
        {
            try
            {
                return await _context.Authors.AsNoTracking()
                    .Include(options => options.Nationality)
                    .FirstOrDefaultAsync(options => options.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving author with ID {id}.");
                throw new Exception($"An error occurred while retrieving author with ID {id}.", ex);
            }
        }

        public async Task<AuthorModel?> GetByName(string name)
        {
            try
            {
                return await _context.Authors.AsNoTracking()
                    .Include(options => options.Nationality)
                    .FirstOrDefaultAsync(options => options.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving author with name {name}.");
                throw new Exception($"An error occurred while retrieving author with name {name}.", ex);
            }
        }

        public async Task<AuthorModel> Save(AuthorModel model)
        {
            try
            {
                _context.Authors.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving a author.");
                throw new Exception("An error occurred while saving the author.", ex);
            }
        }

        public async Task<AuthorModel> Update(AuthorModel model)
        {
            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(options => options.Id == model.Id);

                if (author == null)
                {
                    throw new KeyNotFoundException($"Author with ID {model.Id} was not found.");
                }

                _context.Entry(author).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating author with ID {Id}", model.Id);
                throw new Exception($"An error occurred while updating author with ID {model.Id}.", ex);
            }
        }
    }
}
