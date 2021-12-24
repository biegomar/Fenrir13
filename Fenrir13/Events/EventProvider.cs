using System.Text;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay.EventSystem.EventArgs;
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
    
    internal void LookAtPierhole(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.PIERHOLE)
        {
            this.universe.Score += this.universe.ScoreBoard[nameof(this.LookAtPierhole)];
            cryoChamber.AfterLook -= LookAtPierhole;
        }
    }
    
    internal void LookAtDisplay(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.DISPLAY)
        {
            var barEaten = false;
            var bar = this.universe.ActiveLocation.GetItemByKey(Keys.CHOCOLATEBAR);
            if (bar == default)
            {
                bar = this.universe.ActivePlayer.GetItemByKey(Keys.CHOCOLATEBAR);

                if (bar == default)
                {
                    barEaten = true;
                }
            }

            if (barEaten)
            {
                PrintingSubsystem.Resource(Descriptions.DISPLAY_BAR_EATEN);
                PrintingSubsystem.ForegroundColor = TextColor.Red;
                PrintingSubsystem.Resource(Descriptions.INTERVENTION_REQUIRED);
                PrintingSubsystem.ResetColors();
                PrintingSubsystem.Resource(Descriptions.INTERVENTION_REQUIRED_INTERPRETATION);
                
                var corridorMap = this.universe.GetLocationMapByEnumString(Enum.GetName(Directions.W));
                var corridor = corridorMap.Location;
                if (corridor is { IsLocked: true })
                {
                    corridor.IsLocked = false;
                    this.universe.Score += this.universe.ScoreBoard[nameof(this.LookAtDisplay)];
                    cryoChamber.AfterLook -= LookAtDisplay;
                }
            }
            else
            {
                PrintingSubsystem.Resource(Descriptions.DISPLAY_BAR_NOT_EATEN);
            }
        }
    }
    
    internal void LookAtClosedDoor(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.CLOSET_DOOR)
        {
            var bar = this.universe.ActiveLocation.GetItemByKey(Keys.CHOCOLATEBAR);
            
            if (bar != default)
            {
                PrintingSubsystem.Resource(Descriptions.CLOSET_DOOR_FIRSTLOOK);
                bar.IsHidden = false;
                this.universe.Score += this.universe.ScoreBoard[nameof(this.LookAtClosedDoor)];
                cryoChamber.AfterLook -= LookAtClosedDoor;
            }
            
        }
    }
    
    internal void EatChocolateBar(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item powerBar && powerBar.Key == Keys.CHOCOLATEBAR)
        {
            this.universe.Score += this.universe.ScoreBoard[nameof(this.EatChocolateBar)];
            powerBar.AfterEat -= EatChocolateBar;
            PrintingSubsystem.Resource(Descriptions.CHOCOLATEBAR_EATEN);
            this.universe.ActivePlayer.Items.Remove(powerBar);
        }
    }
    
    internal void TakeSpaceSuite(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item spaceSuite && spaceSuite.Key == Keys.SPACE_SUITE)
        {
            this.universe.ActivePlayer.FirstLookDescription = string.Empty;
            this.universe.ActivePlayer.StandardClothing = Descriptions.SPACE_SUITE_FIT;
            this.universe.ActivePlayer.Items.Remove(spaceSuite);
            this.universe.Score += this.universe.ScoreBoard[nameof(this.TakeSpaceSuite)];
            PrintingSubsystem.Resource(Descriptions.SPACE_SUITE_TAKEN);
            spaceSuite.AfterTake -= TakeSpaceSuite;
        }
    }
    
}