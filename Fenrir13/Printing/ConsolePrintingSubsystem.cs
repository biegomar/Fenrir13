using System.Text;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Resources;
using Heretic.InteractiveFiction.Subsystems;

namespace Fenrir13.Printing;

internal class ConsolePrintingSubsystem : BaseConsolePrintingSubsystem
{
    public ConsolePrintingSubsystem(int consoleWidth = 0)
    {
        Console.OutputEncoding = Encoding.UTF8;
        this.ConsoleWidth = consoleWidth;
    }
    
    public override bool Opening()
    {
        if (!string.IsNullOrEmpty(MetaData.ASCII_TITLE))
        {
            Console.WriteLine(MetaData.ASCII_TITLE);
        }
        var (version, productName) = GetMetaInfo();
        Console.WriteLine($@"{productName} - {version}");
        this.ForegroundColor = TextColor.DarkCyan;
        this.Resource(BaseDescriptions.CHANGE_NAME);
        this.ResetColors();
        Console.Write(WordWrap(BaseDescriptions.HELLO_STRANGER, this.ConsoleWidth));
        Console.Write(WordWrap(Descriptions.OPENING, this.ConsoleWidth));
        Console.WriteLine();

        return true;
    }
    
    public override bool Closing()
    {
        Console.Write(WordWrap(Descriptions.CLOSING, this.ConsoleWidth));
        Console.WriteLine();
        this.ForegroundColor = TextColor.DarkCyan;
        Console.WriteLine(WordWrap(Descriptions.GOODBYE, this.ConsoleWidth));
        this.ResetColors();
        return true;
    }

    public override bool TitleAndScore(int score, int maxScore)
    {
        var (version, productName) = GetMetaInfo();
        Console.Title = $@"{productName} - {version} ({string.Format(BaseDescriptions.ACTUAL_SCORE, score, maxScore)} )";
        return true;
    }

    private static (string version, string productName) GetMetaInfo()
    {
        return (MetaData.VERSION, MetaData.DESCRIPTION);
    }
    
    public override bool Credits()
    {
        if (!string.IsNullOrEmpty(MetaData.ASCII_TITLE))
        {
            Console.WriteLine(MetaData.ASCII_TITLE);
        }
        Console.WriteLine($@"{MetaData.DESCRIPTION} - {MetaData.VERSION}");
        Console.WriteLine($@"Written by {MetaData.AUTHOR}");
        Console.WriteLine(MetaData.COPYRIGHT);
        Console.WriteLine();
        Console.WriteLine(MetaData.BETA_TESTER);
        Console.WriteLine();
        Console.WriteLine(GetVersionNumber());
        Console.WriteLine();

        return true;
    }
}