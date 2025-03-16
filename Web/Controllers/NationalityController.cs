using BookStoreSys_API.Common.Helpers;
using BookStoreSys_API.Domain.Services;
using BookStoreSys_API.Web.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
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
            _nationalityService = nationalityService;
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

        [HttpGet, Route("get-nationaly")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var nationalities = await _nationalityService.GetById(id);
                return Ok(nationalities);
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
                    return BadRequest("The 'Country' field is required.");
                }

                if (string.IsNullOrWhiteSpace(dto.Nationality))
                {
                    return BadRequest("The 'Nationality' field is required.");
                }

                var model = await _nationalityService.Save(NationalityHelper.ToNationalityModel(0, dto));
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
                if (string.IsNullOrWhiteSpace(dto.Country))
                {
                    return BadRequest("The 'Country' field is required.");
                }

                if (string.IsNullOrWhiteSpace(dto.Nationality))
                {
                    return BadRequest("The 'Nationality' field is required.");
                }

                var model = await _nationalityService.Update(NationalityHelper.ToNationalityModel(id, dto));
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
