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
using System.Linq;
using API_excel.FuncClasses;

namespace API_excel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvController : ControllerBase
    {
        private readonly ICsvService _csvService;
        public CsvController(ICsvService csvService)//creating a CSVservice object
        {
            _csvService = csvService;
        }

        [HttpPost("~/api/PostCsvFile")]//Accepts a file of the form .csv
        public async Task<IActionResult> PostCsvFile(IFormFile file) 
        {
            return await _csvService.PostCsvFile(file, HttpContext);
        }

        [HttpGet("~/api/GetResults")]//Returns Results
        public async Task<IActionResult> GetResults([FromQuery]FilterResultSearchModel filterResultSearch)
        {
           return Ok(await FilterResultSearchClass.GetFilterResult(_csvService, filterResultSearch));                   
        }

        [HttpGet("~/api/GetValues")]//Return Values 
        public async Task<IActionResult> GetValues([FromQuery]string fileName)
        {
            if (await _csvService.GetValues(fileName) != null)
            {
                return Ok(await _csvService.GetValues(fileName));
            }
            return BadRequest();
        }
    }
}
