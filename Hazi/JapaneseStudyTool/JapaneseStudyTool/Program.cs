using JapaneseStudyTool.JapaneseStudyTool.UI;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;

        MainMenu.RunMainMenu();
    }
}