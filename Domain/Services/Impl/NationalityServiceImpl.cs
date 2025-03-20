using BookStoreSys_API.Domain.Models;
using BookStoreSys_API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStoreSys_API.Domain.Services.Impl
{
    public class NationalityServiceImpl(
        BookstoreContext context,
        ILogger<NationalityServiceImpl> logger) : INationalityService
    {
        private readonly BookstoreContext _context = context ?? throw new ArgumentException(nameof(context));
        private readonly ILogger<NationalityServiceImpl> _logger = logger ?? throw new ArgumentException(nameof(logger));

        public async Task<List<NationalityModel>> GetAll()
        {
            try
            {
                return await _context.Nationalities.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all nationalities.");
                throw new Exception("An error occurred while retrieving all nationalities.", ex);
            }
        }

        public async Task<NationalityModel?> GetById(int id)
        {
            try
            {
                return await _context.Nationalities.FirstOrDefaultAsync(options => options.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving nationality with ID {id}.");
                throw new Exception($"An error occurred while retrieving nationality with ID {id}.", ex);
            }
        }

        public async Task<NationalityModel> Save(NationalityModel model)
        {
            try
            {
                _context.Nationalities.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving a nationality.");
                throw new Exception("An error occurred while saving the nationality.", ex);
            }
        }

        public async Task<NationalityModel> Update(NationalityModel model)
        {
            try
            {
                var nationality = await _context.Nationalities.FirstOrDefaultAsync(options => options.Id == model.Id)
                    ?? throw new KeyNotFoundException($"Nationality with ID {model.Id} was not found.");

                _context.Entry(nationality).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
                
                return model;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating nationality with ID {Id}", model.Id);
                throw new Exception($"An error occurred while updating nationality with ID {model.Id}.", ex);
            }
        }
    }
}
