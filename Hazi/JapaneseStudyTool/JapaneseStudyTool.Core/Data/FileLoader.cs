using JapaneseStudyTool.JapaneseStudyTool.Core.Model;
using System.Text.Json;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal class FileLoader
    {
        internal static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        internal static List<T> LoadFromFile<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File " + fileName + " cannot be found.");
            }

            string json = File.ReadAllText(fileName);
            List<T>? kanaList = JsonSerializer.Deserialize<List<T>>(json, jsonSerializerOptions);

            return kanaList is null ? throw new FileLoadException("Failed to read " + fileName + ".") : kanaList;
        }

        internal static string GetSolutionDirectory()
        {
            var directory = Directory.GetParent(AppContext.BaseDirectory)
                ?.Parent
                ?.Parent
                ?.Parent;

            return directory?.FullName ?? throw new InvalidOperationException("Could not find solution directory");
        }
    }
}
