using DocumenWorker.DB.API.Models;
using DocumenWorker.DB.API.Repository.Interfaces;
using DocumenWorker.DB.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumenWorker.DB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordInfoController : ControllerBase
    {
        private readonly IWordInfoService _wordInfoService;
        public WordInfoController(IWordInfoService wordInfoService)
        {
            _wordInfoService = wordInfoService;
        }

        [HttpPost]
        public IActionResult Add(WordInfo wordInfo)
        {
            try
            {
                _wordInfoService.Add(wordInfo);
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
                var result = _wordInfoService.GetById(id);
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
                var result = _wordInfoService.Remove(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpPut]
        public IActionResult Update(WordInfo wordInfo)
        {
            try
            {
                var result = _wordInfoService.Update(wordInfo);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
