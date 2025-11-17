using JapaneseStudyTool.JapaneseStudyTool.Core.Data;
using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Service
{
    internal class RandomKanaService
    {
        private readonly KanaType _kanaType;
        private readonly DifficultyLevel _difficultyLevel;
        private readonly KanaSet _kanas;

        public RandomKanaService(DifficultyLevel difficultyLevel, KanaType kanaType)
        {
            _difficultyLevel = difficultyLevel;
            _kanaType = kanaType;
            _kanas = KanaLoader.LoadKana(kanaType);

            switch (difficultyLevel)
            {
                case DifficultyLevel.Easy:
                    _kanas.Hiragana.RemoveAll(k => k.Type != "gojuuon");
                    _kanas.Katakana.RemoveAll(k => k.Type != "gojuuon");
                    break;

                case DifficultyLevel.Medium:
                    _kanas.Hiragana.RemoveAll(k => k.Type == "youon");
                    _kanas.Katakana.RemoveAll(k => k.Type == "youon");
                    break;
            }
        }

        public Word GenerateRandomWord()
        {
            int count = _difficultyLevel switch
            {
                DifficultyLevel.Easy => Random.Shared.Next(1, 3),
                DifficultyLevel.Medium => Random.Shared.Next(3, 5),
                DifficultyLevel.Hard => Random.Shared.Next(5, 9),
                _ => throw new ArgumentException("Invalid DifficultyLevel: " + _difficultyLevel)
            };

            List<KanaEntry> randomKanas = _kanaType switch
            {
                KanaType.Hiragana => GetRandomKanaList(_kanas.Hiragana, count, Random.Shared),
                KanaType.Katakana => GetRandomKanaList(_kanas.Katakana, count, Random.Shared),
                KanaType.All => GetRandomKanaList(Random.Shared.NextSingle() <= 0.5 ? _kanas.Hiragana : _kanas.Katakana, count, Random.Shared),
                _ => throw new ArgumentException("Invalid KanaType: " + _kanaType)
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
