using JapaneseStudyTool.JapaneseStudyTool.Core.Data;
using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Service
{
    internal class RandomKanaService
    {
        private readonly DifficultyLevel difficultyLevel;
        private readonly List<KanaEntry> kanas;

        public RandomKanaService(KanaType kanaType, DifficultyLevel difficultyLevel)
        {
            kanas = KanaLoader.LoadKana(kanaType);
            this.difficultyLevel = difficultyLevel;

            switch (difficultyLevel)
            {
                case DifficultyLevel.Easy:
                    kanas.RemoveAll(k => k.Type != "gojuuon");
                    break;

                case DifficultyLevel.Medium:
                    kanas.RemoveAll(k => k.Type == "youon");
                    break;
            }
        }

        public Word GenerateRandomWord()
        {
            Random rnd = new Random();
            int count = difficultyLevel switch
            {
                DifficultyLevel.Easy => rnd.Next(1, 3),
                DifficultyLevel.Medium => rnd.Next(3, 5),
                _ => rnd.Next(5, 9)
            };

            var randomKanas = kanas.OrderBy(k => rnd.Next()).Take(count).ToList();

            string kana = string.Concat(randomKanas.Select(k => k.Kana));
            string romaji = string.Concat(randomKanas.Select(k => k.Romaji));

            return new Word(kana, romaji);
        }
    }
}
