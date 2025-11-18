using JapaneseStudyTool.JapaneseStudyTool.Core.Enum;

namespace JapaneseStudyTool.JapaneseStudyTool.UI
{
    internal sealed class VocabMenu
    {
        internal static void RunVocabMenu()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Welcome to Vocabulary practice!");
                Console.WriteLine("With this module, you can enter words and quiz yourself!\n");
                Console.WriteLine("[1] Quiz");
                Console.WriteLine("[2] Enter words");
                Console.WriteLine("[3] Exit");

                Console.Write(">");
                string expression = Console.ReadLine() ?? string.Empty;

                if (Int32.TryParse(expression, out int modeInt) && Enum.IsDefined(typeof(VocabMenuMode), modeInt))
                {
                    VocabMenuMode mode = (VocabMenuMode)modeInt;

                    switch (mode)
                    {
                        case VocabMenuMode.Quiz:
                            try
                            {
                                VocabQuizUI.RunVocabQuizUI();
                            }
                            catch(FileNotFoundException)
                            {
                                Console.WriteLine("You don't have anything in your vocabulary.\n");
                            }
                            break;

                        case VocabMenuMode.AddWords:
                            VocabAddUI.RunVocabAddUI();
                            break;

                        case VocabMenuMode.Exit:
                            Console.Clear();
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
