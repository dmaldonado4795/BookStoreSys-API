using BookStoreSys_API.Domain.Models;
using BookStoreSys_API.Web.DTOs;

namespace BookStoreSys_API.Common.Helpers
{
    public static class ObjectMapperHelper
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

        public static AuthorModel ToAuthorModel(int id, AuthorDTO dto)
        {
            return new AuthorModel()
            {
                Id = id,
                Name = dto.Name,
                LastName = dto.LastName,
                NationalityId = dto.NationalityId,
                DayOfBirth = DateOnly.Parse(dto.DayOfBirth)
            };
        }

        public static GenreModel ToGenreModel(int id, GenreDTO dto)
        {
            return new GenreModel()
            {
                Id = id,
                Name = dto.Name
            };
        }

        public static BookModel ToBookModel(int id, BookDTO dto)
        {
            return new BookModel()
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                AuthorId = dto.AuthorId,
                GenreId = dto.GenreId
            };
        }
    }
}
