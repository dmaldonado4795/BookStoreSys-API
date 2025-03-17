using BookStoreSys_API.Common.Helpers;
using BookStoreSys_API.Domain.Services;
using BookStoreSys_API.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreSys_API.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService ?? throw new Exception(nameof(genreService));
        }

        [HttpGet, Route("get-genres")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var genres = await _genreService.GetAll();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("get-genre-by-id")]
        public async Task<IActionResult> GetByName(int id)
        {
            try
            {
                var genre = await _genreService.GetById(id);
                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("get-genre-by-name")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var genre = await _genreService.GetByName(name);
                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("save-genre")]
        public async Task<IActionResult> Save([FromBody] GenreDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return BadRequest($"The '{nameof(dto.Name)}' field is required.");
                }

                var model = await _genreService.Save(ObjectMapperHelper.ToGenreModel(0, dto));
                return Ok(new { data = model });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("update-genre")]
        public async Task<IActionResult> Upate(int id, [FromBody] GenreDTO dto)
        {
            try
            {
                var model = await _genreService.Update(ObjectMapperHelper.ToGenreModel(id, dto));
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
