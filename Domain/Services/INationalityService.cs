using BookStoreSys_API.Domain.Models;

namespace BookStoreSys_API.Domain.Services
{
    public interface INationalityService
    {
        Task<List<NationalityModel>> GetAll();

        Task<NationalityModel?> GetById(int id);

        Task<NationalityModel> Save(NationalityModel model);

        Task<NationalityModel> Update(NationalityModel model);
    }
}
