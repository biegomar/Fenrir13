using System.Text;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Resources;
using Heretic.InteractiveFiction.Subsystems;

namespace Fenrir13.Printing;

internal class ConsolePrintingSubsystem : BaseConsolePrintingSubsystem
{
    public ConsolePrintingSubsystem()
    {
        Console.OutputEncoding = Encoding.UTF8;
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
        this.Resource(BaseDescriptions.HELP_WANTED);
        this.ResetColors();
        Console.Write(WordWrap(BaseDescriptions.HELLO_STRANGER, this.ConsoleWidth));
        Console.Write(WordWrap(Descriptions.OPENING, this.ConsoleWidth));
        Console.WriteLine();

        return true;
    }

    public override bool TitleAndScore(int score, int maxScore)
    {
        var (version, productName) = GetMetaInfo();
        Console.Title = $@"{productName} - {version} ({string.Format(BaseDescriptions.SCORE, score, maxScore)} )";
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

        return true;
    }
}