using DocumentWorker.DTO.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentWorker.DTO.Model
{
    /// <summary>
    /// Класс содержущий информацию о слове - cамо слово и его количество повторений в текте.
    /// Также класс наследует IModelValidation, который позволяет валедировать модель через валидаторы наследуемые от IModelValidator
    /// </summary>
    public class WordInfo : IModelValidation
    {
        [Required(ErrorMessage = "Слово не может быть пустым.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина слова не менее 3 и не более 20 символов.")]
        [RegularExpression(@"(^[а-яА-ЯеЁ]+$|^[a-zA-Z]+$)", ErrorMessage = "Слово должно быть написано полностью из кириллицы, либо полностью из латиницы.")]
        public string Text { get; private set; }
        public int CountWordInText { get; private set; }
        public WordInfo(string text) 
        {
            Text = text;
            CountWordInText = 1;
        }
        public void IncreaseCount() => CountWordInText++;
        public void ReduceCount() => CountWordInText--;
    }
}
