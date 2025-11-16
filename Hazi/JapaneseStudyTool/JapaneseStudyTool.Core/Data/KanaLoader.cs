using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Model;
using System.Text.Json;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal sealed class KanaLoader
    {
        private static readonly string hiraganaPath = Path.Combine(GetSolutionDirectory(), "JapaneseStudyTool.Data", "hiragana.json");
        private static readonly string katakanaPath = Path.Combine(GetSolutionDirectory(), "JapaneseStudyTool.Data", "katakana.json");
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public static List<KanaEntry> LoadKana(KanaType type)
        {

            switch (type)
            {
                case KanaType.Hiragana:
                    return LoadFromFile(hiraganaPath);

                case KanaType.Katakana:
                    return LoadFromFile(katakanaPath);

                case KanaType.All:
                    var hiragana = LoadFromFile(hiraganaPath);
                    var katakana = LoadFromFile(katakanaPath);
                    return hiragana.Concat(katakana).ToList();

                default:
                    throw new ArgumentException("File does not exist for KanaType: " + type);
            }
        }

        private static List<KanaEntry> LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File " + fileName + " cannot be found.");
            }

            string json = File.ReadAllText(fileName);
            List<KanaEntry>? kanaList = JsonSerializer.Deserialize<List<KanaEntry>>(json, jsonSerializerOptions);

            return kanaList is null ? throw new FileLoadException("Failed to read " + fileName + ".") : kanaList;
        }

        private static string GetSolutionDirectory()
        {
            var directory = Directory.GetParent(AppContext.BaseDirectory)
                ?.Parent
                ?.Parent
                ?.Parent;

            return directory?.FullName ?? throw new InvalidOperationException("Could not find solution directory");
        }
    }
}
