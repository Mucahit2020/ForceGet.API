using ForceGetCase.Core.Dtos;
using ForceGetCase.Core.Models.ComboBox;
using ForceGetCase.Core.Services.Abstracts;
using ForceGetCase.DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForceGetCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OfferDto offerDto)
        {
            if (offerDto == null)
            {
                return BadRequest("OfferDto cannot be null.");
            }

            try
            {
                var result = await _offerService.AddOfferAsync(offerDto);

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the offer.");
                }

                return Ok(result); // 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> GetOffers()
        {
            var offers = await _offerService.GetOffersAsync();
            return Ok(offers);
        }
    }
}