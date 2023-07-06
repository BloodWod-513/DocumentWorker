using DocumentWorker.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentWorker.Infrastructure.Services
{
    /// <summary>
    /// Класс для парсинга строки через регулярки на отдельные слова
    /// </summary>
    public class StringIntoSingleWordParserService : IStringParserService
    {
        private readonly string _seporatorPattern = @"\p{P}+";
        private readonly string _dashPattern = @"((?<=[\s\.])\-+)|(\-+(?=[\s\.]))";
        public virtual IEnumerable<string> Parse(string str)
        {
            foreach (var word in str.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries))
            {
                var separator = Regex.Match(word, _seporatorPattern).Value;
                var tempWord = Regex.Replace(word, _seporatorPattern, "").ToLower();

                if (separator == "-")
                    tempWord = Regex.Replace(word, _dashPattern, "").ToLower();

                yield return tempWord;
            }
        }
    }
}
