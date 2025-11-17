using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal sealed class VocabHandler : FileHandler
    {
        private static readonly string vocabPath = Path.Combine(GetSolutionDirectory(), "JapaneseStudyTool.Data", "vocab.json");

        internal static List<VocabWord> LoadVocab()
        {
            return LoadFromFile<VocabWord>(vocabPath);
        }

        internal static void SaveVocab<VocabWord>(List<VocabWord> newItems)
        {
            AppendToJsonFile<VocabWord>(vocabPath, newItems);
        }
    }
}