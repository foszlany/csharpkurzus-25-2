using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;
using JapaneseStudyTool.JapaneseStudyTool.Core.Service;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class KanaMenu
    {
        public static void RunKanaMenu()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Welcome to Kana practice!");
                Console.WriteLine("With this module, you can practice reading hiragana, katakana and both!\n");

                Console.WriteLine("To start, enter the desired difficulty and the mode using the following keywords:");
                Console.WriteLine(DifficultyLevel.Easy + " | " + DifficultyLevel.Medium + " | " + DifficultyLevel.Hard);
                Console.WriteLine(KanaType.Hiragana + " | " + KanaType.Katakana + " | " + KanaType.All + "\n");

                Console.WriteLine("Example: 'easy hiragana'");
                Console.WriteLine("Alternatively, type 'exit' to go back!");

                Console.Write(">");
                string expression = Console.ReadLine() ?? string.Empty;

                throw new NotImplementedException();
            }
        }
    }
}
