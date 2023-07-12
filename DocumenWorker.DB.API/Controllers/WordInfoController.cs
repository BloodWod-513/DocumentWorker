using DocumentWorker.APIDB.DTO.Models;
using DocumenWorker.DB.API.Services;
using DocumenWorker.DB.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumenWorker.DB.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с основной таблицой
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WordInfoController : ControllerBase
    {
        private readonly IWordInfoService _wordInfoService;
        public WordInfoController(IWordInfoService wordInfoService)
        {
            _wordInfoService = wordInfoService;
        }

        #region Для будущих целей, сейчас это использовать не надо
        [HttpPost]
        public IActionResult Add(WordInfoDomain wordInfo)
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
        [Route("AddRange")]
        [HttpPost]
        public IActionResult AddRange(List<WordInfoDomain> wordInfos)
        {
            try
            {
                _wordInfoService.AddRange(wordInfos);
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
        public IActionResult Update(WordInfoDomain wordInfo)
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
        [Route("UpdateRange")]
        [HttpPut]
        public IActionResult UpdateRange(List<WordInfoDomain> wordInfos)
        {
            try
            {
                _wordInfoService.UpdateRange(wordInfos);
                return Ok(true);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}
