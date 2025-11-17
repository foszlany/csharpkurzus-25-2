using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal sealed class KanaLoader : FileLoader
    {
        private static readonly string hiraganaPath = Path.Combine(GetSolutionDirectory(), "JapaneseStudyTool.Data", "hiragana.json");
        private static readonly string katakanaPath = Path.Combine(GetSolutionDirectory(), "JapaneseStudyTool.Data", "katakana.json");

        public static KanaSet LoadKana(KanaType type)
        {
            return type switch
            {
                KanaType.Hiragana => new KanaSet { Hiragana = LoadFromFile<KanaEntry>(hiraganaPath) },
                KanaType.Katakana => new KanaSet { Katakana = LoadFromFile<KanaEntry>(katakanaPath) },
                KanaType.All => new KanaSet
                {
                    Hiragana = LoadFromFile<KanaEntry>(hiraganaPath),
                    Katakana = LoadFromFile<KanaEntry>(katakanaPath)
                },
                _ => throw new ArgumentException("File does not exist for KanaType: " + type),
            };
        }
    }
}
