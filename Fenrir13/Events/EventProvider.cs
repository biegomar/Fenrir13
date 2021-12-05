using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Subsystems;

namespace Fenrir13.Events;

internal partial class EventProvider
{
    private readonly Universe universe;
    private readonly IPrintingSubsystem PrintingSubsystem;

    internal IDictionary<string, int> ScoreBoard => this.universe.ScoreBoard;

    public EventProvider(Universe universe, IPrintingSubsystem printingSubsystem)
    {
        this.PrintingSubsystem = printingSubsystem;
        this.universe = universe;
    }
}