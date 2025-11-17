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

        protected static void AppendToJsonFile<T>(string fileName, List<T> newItems)
        {
            List<T> itemList = [];

            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                if(!string.IsNullOrWhiteSpace(json))
                {
                    List<T>? loaded = JsonSerializer.Deserialize<List<T>>(json, jsonSerializerOptions);

                    if (loaded != null)
                    {
                        itemList = loaded;
                    }
                }
            }

            itemList.AddRange(newItems);

            string updatedJson = JsonSerializer.Serialize(itemList, jsonSerializerOptions);
            File.WriteAllText(fileName, updatedJson);
        }
    }
}
