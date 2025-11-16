using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class MainMenu
    {
        internal static void RunMainMenu()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Welcome to JapaneseStudyTool! Select your desired mode.");
                Console.WriteLine("[1] Kana practice");
                Console.WriteLine("[2] Vocabulary practice");
                Console.WriteLine("[3] Exit");

                Console.Write(">");
                string expression = Console.ReadLine() ?? string.Empty;

                if (Int32.TryParse(expression, out int modeInt) && Enum.IsDefined(typeof(MainMenuMode), modeInt))
                {
                    MainMenuMode mode = (MainMenuMode)modeInt;

                    switch (mode)
                    {
                        case MainMenuMode.KanaPractice:
                            KanaMenu.RunKanaMenu();
                            break;

                        case MainMenuMode.VocabularyPractice:
                            VocabMenu.RunVocabMenu();
                            break;

                        case MainMenuMode.Exit:
                            Environment.Exit(0);
                            return;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input.\n");
                }
            }
        }
    }
}
