using BookStoreSys_API.Domain.BO;
using BookStoreSys_API.Web.DTOs;

namespace BookStoreSys_API.Common.Helpers
{
    public static class NationalityHelper
    {
        public static NationalityModel ToNationalityModel(int id, NationalityDTO dto)
        {
            return new NationalityModel()
            {
                Id = id,
                Country = dto.Country,
                Nationality = dto.Nationality
            };
        }
    }
}
