using BookStoreSys_API.Common.Helpers;
using BookStoreSys_API.Domain.Services;
using BookStoreSys_API.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreSys_API.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NationalityController : ControllerBase
    {
        private readonly INationalityService _nationalityService;

        public NationalityController(INationalityService nationalityService)
        {
            _nationalityService = nationalityService ?? throw new ArgumentException(nameof(nationalityService));
        }

        [HttpGet, Route("get-nationalities")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var nationalities = await _nationalityService.GetAll();
                return Ok(nationalities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("get-nationaly-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var nationality = await _nationalityService.GetById(id);
                return Ok(nationality);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("save-nationaly")]
        public async Task<IActionResult> Save([FromBody] NationalityDTO dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Country))
                {
                    return BadRequest($"The '{nameof(dto.Country)}' field is required.");
                }

                if (string.IsNullOrWhiteSpace(dto.Nationality))
                {
                    return BadRequest($"The '{nameof(dto.Nationality)}' field is required.");
                }

                var model = await _nationalityService.Save(ObjectMapperHelper.ToNationalityModel(0, dto));
                return Ok(new { data = model });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("update-nationaly")]
        public async Task<IActionResult> Upate(int id, [FromBody] NationalityDTO dto)
        {
            try
            {
                var model = await _nationalityService.Update(ObjectMapperHelper.ToNationalityModel(id, dto));
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
