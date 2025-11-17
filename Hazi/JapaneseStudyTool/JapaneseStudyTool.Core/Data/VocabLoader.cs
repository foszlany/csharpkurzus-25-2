using JapaneseStudyTool.JapaneseStudyTool.Core.Model;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal sealed class VocabLoader : FileLoader
    {
        private static readonly string vocabPath = Path.Combine(GetSolutionDirectory(), "JapaneseStudyTool.Data", "vocab.json");

        public static List<VocabWord> LoadVocab()
        {
            return LoadFromFile<VocabWord>(vocabPath);
        }
    }
}