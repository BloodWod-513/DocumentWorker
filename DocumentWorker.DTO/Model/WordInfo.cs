using DocumentWorker.DTO.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using DocumentWorker.DTO.Data;
using WordSetting = DocumentWorker.DTO.Data.Const.WordSetting;
namespace DocumentWorker.DTO.Model
{
    /// <summary>
    /// Класс содержущий информацию о слове - cамо слово и его количество повторений в текте.
    /// Также класс наследует IModelValidation, который позволяет валедировать модель через валидаторы наследуемые от IModelValidator
    /// </summary>
    public class WordInfo : IModelValidation
    {
        [Required(ErrorMessage = "Слово не может быть пустым.")]
        [StringLength(WordSetting.MAX_WORD_LEGTH, MinimumLength = WordSetting.MIN_WORD_LENGTH, ErrorMessage = "Длина слова не менее 3 и не более 20 символов.")]
        [RegularExpression(@"(^[а-яА-ЯеЁё-]+$|^[a-zA-Z]+$)", ErrorMessage = "Слово должно быть написано полностью из кириллицы, либо полностью из латиницы.")]
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
