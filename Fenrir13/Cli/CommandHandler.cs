using Fenrir13.Events;
using Fenrir13.GamePlay;
using Fenrir13.Printing;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.GamePlay.EventSystem;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Subsystems;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    public string FileName { get; set; }
    
    [ArgDescription("Sets the maximum width of the output, regardless of the physical width of the console")]
    public int ConsoleWidth { get; set; }

    public void Main()
    {
        var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            services.AddSingleton<IPrintingSubsystem, ConsolePrintingSubsystem>();
            services.AddSingleton<IGamePrerequisitesAssembler, GamePrerequisitesAssembler>();
            services.AddSingleton<IResourceProvider, ResourceProvider>();
            services.AddSingleton<IGrammar, GermanGrammar>();
            services.AddSingleton<GameLoop>();
            services.AddSingleton<InputProcessor>();
            services.AddSingleton<EventProvider>();
            services.AddSingleton<Universe>();
        }).Build();

        var gameLoop = ActivatorUtilities.CreateInstance<GameLoop>(host.Services, ConsoleWidth);
        gameLoop.Run(FileName);
    }
}