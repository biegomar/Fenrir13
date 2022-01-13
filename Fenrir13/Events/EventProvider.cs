using System.Text;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Exceptions;
using Heretic.InteractiveFiction.GamePlay.EventSystem.EventArgs;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Resources;
using Heretic.InteractiveFiction.Subsystems;

namespace Fenrir13.Events;

internal partial class EventProvider
{
    private readonly Universe universe;
    private readonly IPrintingSubsystem PrintingSubsystem;
    private bool isAccessGranted;

    internal IDictionary<string, int> ScoreBoard => this.universe.ScoreBoard;

    public EventProvider(Universe universe, IPrintingSubsystem printingSubsystem)
    {
        this.PrintingSubsystem = printingSubsystem;
        this.universe = universe;
        this.isAccessGranted = false;
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
            if (IsPowerBarEaten())
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

    private bool IsPowerBarEaten()
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

        return barEaten;
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
    
    internal void TryToOpenClosedDoor(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.CRYOCHAMBER_DOOR)
        {
            PrintingSubsystem.Resource(Descriptions.CRYOCHAMBER_DOOR_TRY_OPEN);
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
    
    internal void CantLeaveWithoutSuiteAndUneatenBar(object sender, ChangeLocationEventArgs eventArgs)
    {
        if (sender is Location chamber && chamber.Key == Keys.CRYOCHAMBER
            && eventArgs.NewDestinationNode.Location.Key == Keys.CORRIDOR_EAST)
        {
            if (IsPowerBarEaten())
            {
                var suite = this.universe.ActiveLocation.GetItemByKey(Keys.SPACE_SUITE);
                if (suite != default)
                {
                    throw new BeforeChangeLocationException(Descriptions.CANT_LEAVE_CHAMBER);
                }   
            }
        }
    }
    
    internal void BeforeStandUp(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Player you && you.Key == Keys.PLAYER)
        {
            if (you.IsSitting && this.universe.ActiveLocation.Key == Keys.COMPUTER_TERMINAL)
            {
                var location = this.universe.LocationMap.Keys.FirstOrDefault(location => location.Key == Keys.COMMANDBRIDGE);
                if (location is { } commandBridge)
                {
                    this.universe.ActiveLocation = commandBridge;
                }
            }
        }
    }
    
    internal void AfterStandUp(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Player you && you.Key == Keys.PLAYER)
        {
            if (this.universe.ActiveLocation.Key == Keys.COMMANDBRIDGE)
            {
                PrintingSubsystem.ActiveLocation(this.universe.ActiveLocation, this.universe.LocationMap);
            }
        }
    }
    
    internal void AfterSitDown(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Player you && you.Key == Keys.PLAYER)
        {
            if (you.IsSitting && this.universe.ActiveLocation.Key == Keys.COMMANDBRIDGE)
            {
                var location = this.universe.LocationMap.Keys.FirstOrDefault(location => location.Key == Keys.COMPUTER_TERMINAL);
                if (location is { } terminal)
                {
                    this.universe.ActiveLocation = terminal;
                    PrintingSubsystem.ActiveLocation(this.universe.ActiveLocation, this.universe.LocationMap);
                    if (this.isAccessGranted)
                    {
                        PrintPasswordMessage(Descriptions.COMPUTER_TERMINAL_DISPLAY_REPORT);    
                    }
                    else
                    {
                        PrintPasswordMessage(Descriptions.COMPUTER_TERMINAL_DISPLAY_PASSWORD);
                    }
                    
                }
            }
        }
    }
    
    internal void LookAtControlPanel(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location bridge && bridge.Key == Keys.COMMANDBRIDGE && eventArgs.ExternalItemKey == Keys.CONTROL_PANEL)
        {
            var note = this.universe.ActiveLocation.GetItemByKey(Keys.STICKY_NOTE);
            if (note != default)
            {
                note.IsHidden = false;
                PrintingSubsystem.Resource(Descriptions.STICKY_NOTE_FOUND);
                this.universe.PickObject(note, true);
                this.universe.Score += this.universe.ScoreBoard[nameof(this.LookAtControlPanel)];
                bridge.AfterLook -= LookAtControlPanel;
            }
        }
    }

    internal void LookAtPowerStation(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location gym && gym.Key == Keys.GYM && eventArgs.ExternalItemKey == Keys.GYM_POWERSTATION)
        {
            var rack = this.universe.ActiveLocation.GetItemByKey(Keys.DUMBBELL_RACK);
            if (rack != default)
            {
                rack.IsHidden = false;
                gym.AfterLook -= LookAtPowerStation;
            }
        }
    }

    internal void BreakEquipmentBoxLock(object sender, BreakItemEventArg eventArg)
    {
        if (eventArg.ItemToUse == default)
        {
            throw new BreakException(BaseDescriptions.IMPOSSIBLE_BREAK_ITEM);
        }
        
        if (sender is Location equipmentRoom && equipmentRoom.Key == Keys.EQUIPMENT_ROOM && eventArg.ItemToUse.Key == Keys.DUMBBELL_BAR)
        {
            var box = equipmentRoom.GetItemByKey(Keys.EQUIPMENT_BOX);
            if (box != default)
            {
                box.IsLocked = false;
                PrintingSubsystem.Resource(Descriptions.BREAK_EQUIPMENT_BOX_LOCK);
                this.universe.Score += this.universe.ScoreBoard[nameof(this.BreakEquipmentBoxLock)];
                equipmentRoom.Break -= BreakEquipmentBoxLock;
            }
        }
    }
    
    internal void OpenEquipmentBox(object sender, ContainerObjectEventArgs eventArg)
    {
        if (sender is Item box && box.Key == Keys.EQUIPMENT_BOX)
        {
            if (!box.IsClosed)
            {
                box.ContainmentDescription = string.Empty;
                box.AfterOpen -= OpenEquipmentBox;
            }
        }
    }
    
    internal void WriteTextToComputerTerminal(object sender, WriteEventArgs eventArgs)
    {
        if (sender is Location terminal && terminal.Key == Keys.COMPUTER_TERMINAL)
        {
            if (!this.isAccessGranted)
            {
                if (IsPasswordCorrect(eventArgs.Text))
                {
                    PrintingSubsystem.Resource(Descriptions.TERMINAL_PASSWORD_SUCCESS);
                    PrintPasswordMessage(Descriptions.COMPUTER_TERMINAL_DISPLAY_REPORT);
                    var room = this.GetRoom(Keys.MACHINE_CORRIDOR_MID);
                    if (room != default)
                    {
                        room.IsLocked = false;
                    }
                    this.isAccessGranted = true;
                    this.universe.Score += this.universe.ScoreBoard[nameof(this.WriteTextToComputerTerminal)];
                }
                else if (eventArgs.Text.ToLower() == Descriptions.TERMINAL_PASSWORD_HINT.ToLower())
                {
                    PrintingSubsystem.Resource(Descriptions.TERMINAL_PASSWORD_HINT_WRONG);
                    PrintPasswordMessage(Descriptions.COMPUTER_TERMINAL_DISPLAY_PASSWORD);
                }
                else
                {
                    PrintingSubsystem.Resource(Descriptions.TERMINAL_PASSWORD_WRONG);
                    PrintPasswordMessage(Descriptions.COMPUTER_TERMINAL_DISPLAY_PASSWORD);
                }
            }
            else
            {
                this.PrintMenu(eventArgs.Text);
            }
        }
    }

    private Location GetRoom(string locationKey)
    {
        var room = this.universe.LocationMap.Keys.FirstOrDefault(location => location.Key == locationKey);
        
        if (room is { } roomLocation)
        {
            return roomLocation;
        }

        return default;
    }

    private void PrintPasswordMessage(string message)
    {
        PrintingSubsystem.ForegroundColor = TextColor.DarkGreen;
        PrintingSubsystem.Resource(message);
        PrintingSubsystem.ResetColors();
    }

    private bool IsPasswordCorrect(string password)
    {
        var passwordList = Descriptions.TERMINAL_PASSWORD.Split('|').ToList();
        var lowerList = passwordList.Select(x => x.ToLower()).ToList();
        return lowerList.Contains(password.ToLower());
    }

    private bool IsInSplitList(string resource, string text)
    {
        var splitList = resource.Split('|').ToList();
        var lowerList = splitList.Select(x => x.ToLower()).ToList();
        return lowerList.Contains(text.ToLower());
    }
    
    private void PrintMenu(string text)
    {
        if (IsInSplitList(Descriptions.TERMINAL_MENU_1, text))
        {
            PrintingSubsystem.Resource(Descriptions.COMPUTER_TERMINAL_DISPLAY_REPORT_1);
        }
        else if (IsInSplitList(Descriptions.TERMINAL_MENU_2, text))
        {
            PrintingSubsystem.Resource(Descriptions.COMPUTER_TERMINAL_DISPLAY_REPORT_2);
        }
        
        PrintPasswordMessage(Descriptions.COMPUTER_TERMINAL_DISPLAY_REPORT);
    }
}