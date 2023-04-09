using Fenrir13.Events;
using Fenrir13.GamePlay;
using Fenrir13.Help;
using Fenrir13.Printing;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Subsystems;
using Microsoft.Extensions.DependencyInjection;
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
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<IPrintingSubsystem, ConsolePrintingSubsystem>();
        services.AddSingleton<IGamePrerequisitesAssembler, GamePrerequisitesAssembler>();
        services.AddSingleton<IResourceProvider, ResourceProvider>();
        services.AddSingleton<IVerbHandler, GermanVerbHandler>();
        services.AddSingleton<IGrammar, GermanGrammar>();
        services.AddSingleton<IHelpSubsystem, HelpSubsystem>();
        services.AddSingleton<GameLoop>();
        services.AddSingleton<InputProcessor>();
        services.AddSingleton<ScoreBoard>();
        services.AddSingleton<EventProvider>();
        services.AddSingleton<Universe>();
        var serviceProvider = services.BuildServiceProvider();

        var gameLoop = ActivatorUtilities.CreateInstance<GameLoop>(serviceProvider, ConsoleWidth);
        gameLoop.Run(FileName);
    }
}