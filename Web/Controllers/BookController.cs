using BookStoreSys_API.Common.Helpers;
using BookStoreSys_API.Domain.Services;
using BookStoreSys_API.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreSys_API.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService ?? throw new ArgumentException(nameof(bookService));

        [HttpGet, Route("get-books")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _bookService.GetAll();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("get-book-by-id")]
        public async Task<IActionResult> GetByName(int id)
        {
            try
            {
                var book = await _bookService.GetById(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("get-book-by-title")]
        public async Task<IActionResult> GetByName(string title)
        {
            try
            {
                var book = await _bookService.GetByTitle(title);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("save-book")]
        public async Task<IActionResult> Save([FromBody] BookDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Title))
                {
                    return BadRequest($"The '{nameof(dto.Title)}' field is required.");
                }

                if (string.IsNullOrWhiteSpace(dto.Description))
                {
                    return BadRequest($"The '{nameof(dto.Description)}' field is required.");
                }

                if (!DateOnly.TryParse(dto.PublicationDate, out DateOnly result))
                {
                    return BadRequest($"The '{nameof(dto.PublicationDate)}' is invalid.");
                }

                if (dto.AuthorId <= 0)
                {
                    return BadRequest($"The '{nameof(dto.AuthorId)}' is invalid.");
                }

                if (dto.GenreId <= 0)
                {
                    return BadRequest($"The '{nameof(dto.GenreId)}' is invalid.");
                }

                var model = await _bookService.Save(ObjectMapperHelper.ToBookModel(0, dto));
                return Ok(new { data = model });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("update-book")]
        public async Task<IActionResult> Upate(int id, [FromBody] BookDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.PublicationDate) && !DateOnly.TryParse(dto.PublicationDate, out DateOnly result))
                {
                    return BadRequest($"The '{nameof(dto.PublicationDate)}' is invalid.");
                }

                var model = await _bookService.Update(ObjectMapperHelper.ToBookModel(id, dto));
                return Ok(new { data = model });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
