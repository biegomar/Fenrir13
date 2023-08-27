using Fenrir13.GamePlay;
using Heretic.InteractiveFiction.GamePlay;
using PowerArgs;

namespace Fenrir13.Cli;

internal class CommandHandler
{
    [HelpHook]
    [ArgShortcut("-h")]
    [ArgDescription("Shows this help")]
    internal bool Help { get; set; }

    [ArgExistingFile]
    [ArgDescription("Loads a game from a file")]
    public string FileName { get; set; } = string.Empty;
    
    [ArgDescription("Sets the maximum width of the output, regardless of the physical width of the console")]
    public int ConsoleWidth { get; set; }

    public void Main()
    {
        IGamePrerequisitesAssembler gamePrerequisitesAssembler = new GamePrerequisitesAssembler();
        var gameLoop = new GameLoop(gamePrerequisitesAssembler, ConsoleWidth);

        gameLoop.Run(FileName);
    }
}