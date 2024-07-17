using ForceGetCase.Core.Models.ComboBox;
using ForceGetCase.Core.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForceGetCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboBoxController : ControllerBase
    {
        private readonly IComboBoxService _comboBoxService;
        public ComboBoxController(IComboBoxService comboBoxService)
        {
           _comboBoxService = comboBoxService;
        }

        [HttpGet("mode")]
        public async Task<ActionResult<List<ComboBoxBaseModel>>> GetModeOptions()
        {
            var result = await _comboBoxService.GetModeOptions();

            return Ok(result);
        }

        [HttpGet("movementType")]
        public async Task<ActionResult<List<ComboBoxBaseModel>>> GetMovementTypes()
        {
            var result = await _comboBoxService.GetMovementTypes();

            return Ok(result);
        }
        [HttpGet("incoterms")]
        public async Task<ActionResult<List<ComboBoxBaseModel>>> GetIncoterms()
        {
            var result = await _comboBoxService.GetIncoterms();

            return Ok(result);
        }

        [HttpGet("countries")]
        public async Task<ActionResult<List<ComboBoxBaseModel>>> GetCountries()
        {
            var result = await _comboBoxService.GetCountries();

            return Ok(result);
        }
        [HttpGet("packageType")]
        public async Task<ActionResult<List<ComboBoxBaseModel>>> GetPackageTypes()
        {
            var result = await _comboBoxService.GetPackageTypes();

            return Ok(result);
        }

        [HttpGet("unit1")]
        public async Task<ActionResult<List<ComboBoxBaseModel>>> GetUnit1()
        {
            var result = await _comboBoxService.GetUnit1();

            return Ok(result);
        }

        [HttpGet("unit2")]
        public async Task<ActionResult<List<ComboBoxBaseModel>>> GetUnit2()
        {
            var result = await _comboBoxService.GetUnit2();

            return Ok(result);
        }

        [HttpGet("currency")]
        public async Task<ActionResult<List<ComboBoxBaseModel>>> GetCurrency()
        {
            var result = await _comboBoxService.GetCurrency();

            return Ok(result);
        }
    }
}
