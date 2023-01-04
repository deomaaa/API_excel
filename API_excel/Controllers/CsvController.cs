using API_excel.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using API_excel.CsvService;

namespace API_excel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvController : ControllerBase
    {
        private readonly ICsvService _csvService;
        public CsvController(ICsvService csvService)
        {
            _csvService = csvService;
        }

        [HttpPost("~/api/PostCsvFile")]
        public async Task<IActionResult> PostCsvFile(IFormFile file) // Request.Form.Files
        {
            return await _csvService.PostCsvFile(file, HttpContext);
        }

        [HttpGet("~/api/GetResults_FileName")]
        public async Task<IActionResult> GetResults_FileName([FromQuery]string fileName)
        {
            return Ok(await _csvService.GetResults_FileName(fileName));
        }

        [HttpPost("~/api/GetResults_MiddleIndicator")]
        public async Task<IActionResult> GetResults_MiddleIndicator([FromBody] MiddleIndicatorInterval middleIndicatorInterval)
        {
            return Ok(await _csvService.GetResults_MiddleIndicator(middleIndicatorInterval));
        }

        [HttpPost("~/api/GetResults_MiddleTime")]
        public async Task<IActionResult> GetResults_MiddleTime([FromBody] MiddleTimeInterval middleTimeInterval)
        {
            return Ok(await _csvService.GetResults_MiddleTime(middleTimeInterval));
        }

        [HttpPost("~/api/GetResults_TimeReceipt")]
        public async Task<IActionResult> GetResults_TimeReceipt([FromBody]TimeInterval timeInterval)
        {
            return Ok(await _csvService.GetResults_TimeReceipt(timeInterval));
        }

        [HttpGet("~/api/GetValues")]
        public async Task<IActionResult> GetValues([FromQuery]string fileName)
        {

            return Ok(await _csvService.GetValues(fileName));        
        }
    }
}
