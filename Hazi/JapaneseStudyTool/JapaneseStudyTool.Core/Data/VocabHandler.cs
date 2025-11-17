using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal sealed class VocabHandler : FileHandler
    {
        private static readonly string _vocabPath = Path.Combine(GetSolutionDirectory(), "JapaneseStudyTool.Data", "vocab.json");

        internal static List<VocabWord> LoadVocab()
        {
            return LoadFromFile<VocabWord>(_vocabPath);
        }

        internal static void SaveVocab<VocabWord>(List<VocabWord> newItems)
        {
            AppendToJsonFile<VocabWord>(_vocabPath, newItems);
        }
    }
}