using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Subsystems;

namespace Fenrir13.Help;

public class HelpSubsystem: BaseHelpSubsystem
{
    public HelpSubsystem(IGrammar grammar, IPrintingSubsystem printingSubsystem) : base(grammar, printingSubsystem)
    {
    }

    public override bool Help()
    {
        return this.General()
               && this.Directions()
               && this.Interactions()
               && this.Container()
               && this.MetaInformation();
    }
}