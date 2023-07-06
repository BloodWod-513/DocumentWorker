using DocumentWorker.DTO.Model;
using DocumentWorker.Infrastructure.Services.Interfaces;

namespace DocumentWorker.Infrastructure.Services
{
    /// <summary>
    /// Обработчик, который должен читать файл и парсить прочитанную строку.
    /// После того как основной процессинг прошел, по сути начинается пост процессинг,
    /// который добавляет результаты в словарь и убирает лишнее.
    /// </summary>
    public class WordProcessingService
    {
        private readonly ITxtFileReaderService _txtFileReaderService;
        private readonly IStringParserService _stringParserService;

        public WordProcessingService() 
        {
            _txtFileReaderService = new TxtFileReaderWithValidationService();
            _stringParserService = new WordInfoParserWithValidationService<WordInfo>();
        }

        public Dictionary<string, WordInfo> GetWords(FileInfo fileInfo)
        {
            Dictionary<string, WordInfo> dict = new Dictionary<string, WordInfo>();
            try
            {
                foreach (var line in _txtFileReaderService.ReadLine(fileInfo))
                {
                    foreach (var word in _stringParserService.Parse(line))
                    {
                        AddToDictionary(dict, word);
                    }
                }
                DeleteNotCommonWords(dict);
            }
            //TODO: кастомные екзепшены для валейдаторов
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка: {ex.Message}");
            }
            return dict;
        }

        private void AddToDictionary(Dictionary<string, WordInfo> dict, string word)
        {
            if (dict.ContainsKey(word))
            {
                dict[word].IncreaseCount();
            }
            else
            {
                dict.Add(word, new WordInfo(word));
            }
        }
        private void DeleteNotCommonWords(Dictionary<string, WordInfo> dict)
        {
            foreach (var (word, wordInfo) in dict)
            {
                //TODO: убрать магическое число 4
                if (wordInfo.CountWordInText < 4)
                    dict.Remove(word);
            }
        }
    }
}
