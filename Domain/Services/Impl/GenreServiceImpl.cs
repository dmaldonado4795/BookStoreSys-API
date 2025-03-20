using BookStoreSys_API.Domain.Models;
using BookStoreSys_API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreSys_API.Domain.Services.Impl
{
    public class GenreServiceImpl(
        BookstoreContext context,
        ILogger<GenreServiceImpl> logger) : IGenreService
    {
        private readonly BookstoreContext _context = context ?? throw new ArgumentException(nameof(context));
        private readonly ILogger<GenreServiceImpl> _logger = logger ?? throw new ArgumentException(nameof(logger));

        public async Task<List<GenreModel>> GetAll()
        {
            try
            {
                return await _context.Genres.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all genres.");
                throw new Exception("An error occurred while retrieving all genres.", ex);
            }
        }

        public async Task<GenreModel?> GetById(int id)
        {
            try
            {
                return await _context.Genres.AsNoTracking().FirstOrDefaultAsync(options => options.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving genre with ID {id}.");
                throw new Exception($"An error occurred while retrieving genre with ID {id}.", ex);
            }
        }

        public async Task<GenreModel?> GetByName(string name)
        {
            try
            {
                return await _context.Genres.AsNoTracking().FirstOrDefaultAsync(options => options.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving genre with name {name}.");
                throw new Exception($"An error occurred while retrieving genre with name {name}.", ex);
            }
        }

        public async Task<GenreModel> Save(GenreModel model)
        {
            try
            {
                _context.Genres.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving a genre.");
                throw new Exception("An error occurred while saving the genre.", ex);
            }
        }

        public async Task<GenreModel> Update(GenreModel model)
        {
            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(options => options.Id == model.Id)
                    ?? throw new KeyNotFoundException($"Genre with ID {model.Id} was not found.");

                _context.Entry(genre).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
                
                return model;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating genre with ID {Id}", model.Id);
                throw new Exception($"An error occurred while updating genre with ID {model.Id}.", ex);
            }
        }
    }
}
