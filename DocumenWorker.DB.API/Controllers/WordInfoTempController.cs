﻿using DocumentWorker.APIDB.DTO.Models;
using DocumenWorker.DB.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumenWorker.DB.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с второстепенной таблицей
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WordInfoTempController : ControllerBase
    {
        private readonly IWordInfoTempService _wordInfoTempService;
        private readonly ILogger<WordInfoTempController> _logger;
        public WordInfoTempController(IWordInfoTempService wordInfoTempService,
            ILogger<WordInfoTempController> logger)
        {
            _wordInfoTempService = wordInfoTempService;
            _logger = logger;
        }

        /// <summary>
        /// Метод контроллера, который должен передать полученный список в сервис
        /// </summary>
        /// <param name="wordInfos"></param>
        /// <returns></returns>
        [Route("AddRange")]
        [HttpPost]
        public IActionResult AddRange(List<WordInfoTempDomain> wordInfos)
        {
            try
            {
                _logger.LogInformation("Пришел запрос в метод AddRange..");
                _wordInfoTempService.AddRange(wordInfos);
                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #region Для будущих целей, сейчас это использовать не надо
        [HttpPost]
        public IActionResult Add(WordInfoTempDomain wordInfo)
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
        public IActionResult Update(WordInfoTempDomain wordInfo)
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
        [Route("UpdateRange")]
        [HttpPut]
        public IActionResult UpdateRange(List<WordInfoTempDomain> wordInfos)
        {
            try
            {
                _wordInfoTempService.UpdateRange(wordInfos);
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
