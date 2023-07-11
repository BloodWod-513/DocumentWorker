using DocumenWorker.DB.API.Models;
using DocumenWorker.DB.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumenWorker.DB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordInfoTempController : ControllerBase
    {
        private readonly IWordInfoTempService _wordInfoTempService;
        public WordInfoTempController(IWordInfoTempService wordInfoTempService)
        {
            _wordInfoTempService = wordInfoTempService;
        }

        [HttpPost]
        public IActionResult Add(WordInfoTemp wordInfo)
        {
            try
            {
                _wordInfoTempService.Add(wordInfo);
                return Ok(true);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _wordInfoTempService.GetById(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var result = _wordInfoTempService.Remove(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpPut]
        public IActionResult Update(WordInfoTemp wordInfo)
        {
            try
            {
                var result = _wordInfoTempService.Update(wordInfo);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
