using Fenrir13.Events;
using Fenrir13.GamePlay;
using Fenrir13.Printing;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.GamePlay.EventSystem;
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
            services.AddSingleton<GameLoop>();
            services.AddSingleton<InputProcessor>();
            services.AddSingleton<EventProvider>();
            services.AddSingleton<Universe>();
            services.AddSingleton<PeriodicEvent>(new PeriodicEvent()
            {
                MinDistanceBetweenEvents = 2,
                MaxDistanceBetweenEvents = 20,
                AverageDistanceBetweenEvents = 7,
                Phrases = string.Empty,
                Active = false
            });
        }).Build();

        var gameLoop = ActivatorUtilities.CreateInstance<GameLoop>(host.Services, !string.IsNullOrEmpty(FileName) ? FileName : string.Empty, ConsoleWidth);
        gameLoop.Run();
    }
}