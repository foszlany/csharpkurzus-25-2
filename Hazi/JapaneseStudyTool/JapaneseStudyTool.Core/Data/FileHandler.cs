using System.Text.Json;

namespace JapaneseStudyTool.JapaneseStudyTool.Core.Data
{
    internal abstract class FileHandler
    {
        protected static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        protected static string GetSolutionDirectory()
        {
            var directory = Directory.GetParent(AppContext.BaseDirectory)
                ?.Parent
                ?.Parent
                ?.Parent;

            return directory?.FullName ?? throw new InvalidOperationException("Could not find solution directory");
        }

        protected static List<T> LoadFromFile<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File " + fileName + " cannot be found.");
            }

            string json = File.ReadAllText(fileName);
            List<T>? itemList = JsonSerializer.Deserialize<List<T>>(json, jsonSerializerOptions);

            return itemList is null ? throw new FileLoadException("Failed to read " + fileName + ".") : itemList;
        }
    }
}
