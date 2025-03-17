using BookStoreSys_API.Common.Helpers;
using BookStoreSys_API.Domain.Services;
using BookStoreSys_API.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreSys_API.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentException(nameof(authorService));
        }

        [HttpGet, Route("get-authors")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var authors = await _authorService.GetAll();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet, Route("get-author-by-id")]
        public async Task<IActionResult> GetByName(int id)
        {
            try
            {
                var author = await _authorService.GetById(id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet, Route("get-author-by-name")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var author = await _authorService.GetByName(name);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("save-author")]
        public async Task<IActionResult> Save([FromBody] AuthorDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return BadRequest($"The '{nameof(dto.Name)}' field is required.");
                }

                if (string.IsNullOrWhiteSpace(dto.LastName))
                {
                    return BadRequest($"The '{nameof(dto.LastName)}' field is required.");
                }

                if (!DateTime.TryParse(dto.DayOfBirth, out DateTime dayOfBirth))
                {
                    return BadRequest($"The '{nameof(dto.DayOfBirth)}' is invalid.");
                }

                var model = await _authorService.Save(ObjectMapperHelper.ToAuthorModel(0, dto));
                return Ok(new { data = model });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("update-author")]
        public async Task<IActionResult> Upate(int id, [FromBody] AuthorDTO dto)
        {
            try
            {
                if (!DateTime.TryParse(dto.DayOfBirth, out DateTime dayOfBirth))
                {
                    return BadRequest($"The '{dto.DayOfBirth}' is invalid.");
                }

                var model = await _authorService.Update(ObjectMapperHelper.ToAuthorModel(id, dto));
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
