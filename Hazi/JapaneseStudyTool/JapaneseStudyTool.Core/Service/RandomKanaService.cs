using JapaneseStudyTool.JapaneseStudyTool.Core.Data;
using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Service
{
    internal class RandomKanaService
    {
        private readonly KanaType kanaType;
        private readonly DifficultyLevel difficultyLevel;
        private readonly KanaSet kanas;

        public RandomKanaService(DifficultyLevel difficultyLevel, KanaType kanaType)
        {
            this.difficultyLevel = difficultyLevel;
            this.kanaType = kanaType;
            kanas = KanaLoader.LoadKana(kanaType);

            switch (difficultyLevel)
            {
                case DifficultyLevel.Easy:
                    kanas.Hiragana.RemoveAll(k => k.Type != "gojuuon");
                    kanas.Katakana.RemoveAll(k => k.Type != "gojuuon");
                    break;

                case DifficultyLevel.Medium:
                    kanas.Hiragana.RemoveAll(k => k.Type == "youon");
                    kanas.Katakana.RemoveAll(k => k.Type == "youon");
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
                DifficultyLevel.Hard => rnd.Next(5, 9),
                _ => throw new ArgumentException("Invalid DifficultyLevel: " + difficultyLevel)
            };

            List<KanaEntry> randomKanas = kanaType switch
            {
                KanaType.Hiragana => GetRandomKanaList(kanas.Hiragana, count, rnd),
                KanaType.Katakana => GetRandomKanaList(kanas.Katakana, count, rnd),
                KanaType.All => GetRandomKanaList(rnd.NextSingle() <= 0.5 ? kanas.Hiragana : kanas.Katakana, count, rnd),
                _ => throw new ArgumentException("Invalid KanaType: " + kanaType)
            };

            string kana = string.Concat(randomKanas.Select(k => k.Kana));
            string romaji = string.Concat(randomKanas.Select(k => k.Romaji));

            return new Word(kana, romaji);
        }

        private List<KanaEntry> GetRandomKanaList(List<KanaEntry> source, int count, Random rnd)
        {
            return source.OrderBy(k => rnd.Next()).Take(count).ToList();
        }
    }
}
