using System.Text;
using Fenrir13.GamePlay;
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
    
    internal void PullFridgeHandle(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item fridgeHandle && fridgeHandle.Key == Keys.FRIDGE_DOOR_HANDLE)
        {
            var fridge = this.universe.ActiveLocation.GetItemByKey(Keys.FRIDGE);
            if (fridge != default)
            {
                fridge.IsClosed = true;
                fridge.IsLocked = true;

                PrintingSubsystem.Resource(Descriptions.FRIDGE_DOOR_HANDLE_PULL);
                fridgeHandle.IsPickAble = true;
                fridgeHandle.IsHidden = false;
                this.universe.PickObject(fridgeHandle, true);
                this.universe.Score += this.universe.ScoreBoard[nameof(this.PullFridgeHandle)];
                fridgeHandle.Pull -= PullFridgeHandle;    
            }
        }
    }
    
    internal void PushDoorHandleIntoRespiratorFlap(object sender, PushItemEventArgs eventArgs)
    {
        if (sender is Item doorHandle && doorHandle.Key == Keys.FRIDGE_DOOR_HANDLE)
        {
            if (eventArgs.ItemToUse is Item respiratorFlap && respiratorFlap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP)
            {
                var respirator = this.universe.ActiveLocation.GetItemByKey(Keys.AMBULANCE_RESPIRATOR);
                if (respirator != default)
                {
                    respirator.IsLocked = false;
                    respiratorFlap.IsLocked = false;
                    respiratorFlap.LinkedTo.Add(doorHandle);
                    respiratorFlap.FirstLookDescription = string.Empty;
                    doorHandle.ContainmentDescription = Descriptions.FRIDGE_DOOR_HANDLE_FLAP_CONTAINMENT;
                    doorHandle.IsPickAble = false;
                    doorHandle.UnPickAbleDescription = Descriptions.FRIDGE_DOOR_HANDLE_FLAP_UNPICKABLE;
                    this.universe.ActivePlayer.Items.Remove(doorHandle);
                    PrintingSubsystem.Resource(Descriptions.FRIDGE_DOOR_HANDLE_PUSHED);
                    this.universe.Score += this.universe.ScoreBoard[nameof(this.PushDoorHandleIntoRespiratorFlap)];
                    doorHandle.Push -= PushDoorHandleIntoRespiratorFlap;
                }
            }
            else
            {
                throw new PushException(BaseDescriptions.DOES_NOT_WORK);
            }
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

    internal void LookAtClosetDoor(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.CLOSET_DOOR)
        {
            var bar = this.universe.ActiveLocation.GetItemByKey(Keys.CHOCOLATEBAR);
            
            if (bar != default)
            {
                PrintingSubsystem.Resource(Descriptions.CLOSET_DOOR_FIRSTLOOK);
                bar.IsHidden = false;
                this.universe.Score += this.universe.ScoreBoard[nameof(this.LookAtClosetDoor)];
                cryoChamber.AfterLook -= LookAtClosetDoor;
                cryoChamber.Open -= OpenClosetDoor;
                cryoChamber.Open += OpenClosetDoorAgain;
            }
        }
    }
    
    internal void OpenClosetDoor(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.CLOSET_DOOR)
        {
            PrintingSubsystem.Resource(this.universe.ActiveLocation.Surroundings[eventArgs.ExternalItemKey]);
            
            var bar = this.universe.ActiveLocation.GetItemByKey(Keys.CHOCOLATEBAR);
            if (bar != default)
            {
                PrintingSubsystem.Resource(Descriptions.CLOSET_DOOR_FIRSTLOOK);
                bar.IsHidden = false;
                this.universe.Score += this.universe.ScoreBoard[nameof(this.LookAtClosetDoor)];
                cryoChamber.AfterLook -= LookAtClosetDoor;
                cryoChamber.Open -= OpenClosetDoor;
                cryoChamber.Open += OpenClosetDoorAgain;
            }
        }
    }
    
    private void OpenClosetDoorAgain(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.CLOSET_DOOR)
        {
            PrintingSubsystem.Resource(this.universe.ActiveLocation.Surroundings[eventArgs.ExternalItemKey]);
        }
    }
    
    internal void LookAtRespirator(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location ambulance && ambulance.Key == Keys.AMBULANCE && eventArgs.ExternalItemKey == Keys.AMBULANCE_RESPIRATOR)
        {
            var respirator = this.universe.ActiveLocation.GetItemByKey(Keys.AMBULANCE_RESPIRATOR);
            
            if (respirator != default)
            {
                respirator.IsHidden = false;
                ambulance.AfterLook -= LookAtRespirator;
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
        if (sender is Item spaceSuite && spaceSuite.Key == Keys.SPACE_SUIT)
        {
            this.universe.ActivePlayer.FirstLookDescription = string.Empty;
            this.universe.ActivePlayer.Clothes.Add(spaceSuite);
            this.universe.ActivePlayer.Items.Remove(spaceSuite);
            this.universe.Score += this.universe.ScoreBoard[nameof(this.TakeSpaceSuite)];
            PrintingSubsystem.Resource(Descriptions.SPACE_SUIT_TAKEN);
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
                var suite = this.universe.ActiveLocation.GetItemByKey(Keys.SPACE_SUIT);
                if (suite != default)
                {
                    throw new BeforeChangeLocationException(Descriptions.CANT_LEAVE_CHAMBER);
                }   
            }
        }
    }
    
    internal void PullOnWearables(object sender, PullItemEventArgs eventArgs)
    {
        void SwapItem(Item item)
        {
            this.universe.ActivePlayer.Items.Remove(item);
            this.universe.ActivePlayer.Clothes.Add(item);
            PrintingSubsystem.FormattedResource(Descriptions.PULLON_WEARABLE, item.Name);
        }

        var listOfPossibleWearables = new List<string>()
        {
            Keys.BELT,
            Keys.GLOVES,
            Keys.HELMET,
            Keys.BOOTS
        };
        
        if (sender is Player player && player.Key == Keys.PLAYER && eventArgs.ItemToUse is Item wearable && listOfPossibleWearables.Contains(wearable.Key))
        {
            if (!this.universe.ActivePlayer.Clothes.Contains(wearable))
            {
                if (this.universe.ActivePlayer.Items.Contains(wearable))
                {
                    SwapItem(wearable);
                }
                else
                {
                    this.universe.PickObject(wearable, true);
                    if (this.universe.ActivePlayer.Items.Contains(wearable))
                    {
                        SwapItem(wearable);    
                    }
                }
            }
            else
            {
                PrintingSubsystem.Resource(Descriptions.ALREADY_WEARING);
            }
        }
        else
        {
            throw new PullException(BaseDescriptions.NOTHING_HAPPENS);
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
    
    internal void UseOxygenBottleWithHelmet(object sender, UseItemEventArgs eventArgs)
    {
        if (sender is Item itemOne && eventArgs.ItemToUse is Item itemTwo && itemOne.Key != itemTwo.Key)
        {
            var itemList = new List<Item> { itemOne, itemTwo };
            var oxygenBottle = itemList.SingleOrDefault(i => i.Key == Keys.OXYGEN_BOTTLE);
            var helmet = itemList.SingleOrDefault(i => i.Key == Keys.HELMET);

            if (oxygenBottle != default && helmet != default)
            {
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.OXYGEN_BOTTLE) == default)
                {
                    this.universe.PickObject(oxygenBottle);
                }
                
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.HELMET) == default)
                {
                    this.universe.PickObject(helmet);
                }
                
                oxygenBottle.LinkedTo.Add(helmet);
                helmet.LinkedTo.Add(oxygenBottle);

                PrintingSubsystem.Resource(Descriptions.LINK_OXYGEN_BOTTLE_TO_HELMET);
                oxygenBottle.Use -= UseOxygenBottleWithHelmet;
                helmet.Use -= UseOxygenBottleWithHelmet;

                this.universe.Score += this.universe.ScoreBoard[nameof(UseOxygenBottleWithHelmet)];
            }
        }
    }

    internal void BreakEquipmentBoxLock(object sender, BreakItemEventArg eventArg)
    {
        if (eventArg.ItemToUse == default)
        {
            throw new BreakException(BaseDescriptions.IMPOSSIBLE_BREAK_ITEM);
        }

        if (sender is Item boxLock && boxLock.Key == Keys.EQUIPMENT_BOX_LOCK)
        {
            var box = this.universe.ActiveLocation.GetItemByKey(Keys.EQUIPMENT_BOX);
            if (box != default)
            {
                if (eventArg.ItemToUse.Key != Keys.DUMBBELL_BAR)
                {
                    throw new BreakException(BaseDescriptions.WRONG_BREAK_ITEM);
                }

                box.IsLocked = false;
                box.LinkedTo.Remove(boxLock);
                PrintingSubsystem.Resource(Descriptions.BREAK_EQUIPMENT_BOX_LOCK);
                
                this.universe.ActiveLocation.Items.Add(this.GetDestroyedBoxLock());
                
                this.universe.Score += this.universe.ScoreBoard[nameof(this.BreakEquipmentBoxLock)];
                boxLock.Break -= BreakEquipmentBoxLock;    
            }
        }
    }
    
    private Item GetDestroyedBoxLock()
    {
        var boxLock = new Item()
        {
            Key = Keys.EQUIPMENT_BOX_LOCK_DESTROYED,
            Name = Items.EQUIPMENT_BOX_LOCK,
            Description = Descriptions.EQUIPMENT_BOX_LOCK_DESTROYED,
            ContainmentDescription = Descriptions.EQUIPMENT_BOX_LOCK_BREAK_CONTAINMENT,
            IsPickAble = false
        };
        
        return boxLock;
    }
    
    internal void BreakFlapWithDumbbellBar(object sender, BreakItemEventArg eventArg)
    {
        if (eventArg.ItemToUse == default)
        {
            throw new BreakException(BaseDescriptions.IMPOSSIBLE_BREAK_ITEM);
        }
        
        if (sender is Item flap && flap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP && eventArg.ItemToUse.Key == Keys.DUMBBELL_BAR)
        {
            throw new BreakException(Descriptions.AMBULANCE_RESPIRATOR_FLAP_DUMBBELL_BAR_UNBREAKABLE);
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

    internal void OpenFridge(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item fridge && fridge.Key == Keys.FRIDGE)
        {
            if (fridge.IsClosed)
            {
                var handle = fridge.GetItemByKey(Keys.FRIDGE_DOOR_HANDLE);
                if (handle != default)
                {
                    handle.IsHidden = false;
                    PrintingSubsystem.Resource(Descriptions.FRIDGE_DOOR_HANDLE_OPEN);
                    fridge.BeforeOpen -= OpenFridge;
                }
            }
        }
    }
    
    internal void OpenFlap(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item flap && flap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP)
        {
            if (flap.IsClosed)
            {
                var oxygen = this.universe.ActiveLocation.GetItemByKey(Keys.OXYGEN_BOTTLE);
                if (oxygen != default)
                {
                    oxygen.IsHidden = false;
                    PrintingSubsystem.Resource(Descriptions.OXYGEN_BOTTLE_FOUND);
                }
            }
        }
    }
    
    internal void CloseFlap(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item flap && flap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP)
        {
            if (!flap.IsClosed)
            {
                var oxygen = this.universe.ActiveLocation.GetItemByKey(Keys.OXYGEN_BOTTLE);
                if (oxygen != default)
                {
                    //oxygen.IsHidden = true;
                }
            }
        }
    }

    internal void TakeDumbbellBar(object sender, ContainerObjectEventArgs eventArg)
    {
        if (sender is Item bar && bar.Key == Keys.DUMBBELL_BAR)
        {
            this.universe.Score += this.universe.ScoreBoard[nameof(this.TakeDumbbellBar)];
            bar.AfterTake -= TakeDumbbellBar;
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
    
    internal void EnterAirlock(object sender, ChangeLocationEventArgs eventArgs)
    {
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK)
        {
            ResetItemWeight(this.universe.ActivePlayer.Items);
            ResetItemWeight(this.universe.ActivePlayer.Clothes);
            PrintingSubsystem.Resource(Descriptions.ZERO_GRAVITY);
        }
    }
    
    internal void LeaveAirlock(object sender, ChangeLocationEventArgs eventArgs)
    {
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK && eventArgs.NewDestinationNode.Location.Key == Keys.MACHINE_CORRIDOR_MID)
        {
            var isItemDropped = false;
            SetItemWeight(this.universe.ActivePlayer.Items);
            SetItemWeight(this.universe.ActivePlayer.Clothes);
            var itemWeight = this.universe.ActivePlayer.GetActualPayload();
            do
            {
                if (itemWeight > this.universe.ActivePlayer.MaxPayload)
                {
                    var heaviestItem = this.universe.ActivePlayer.Items.OrderByDescending(i => i.Weight).First();
                    var singleDropResult = this.universe.ActivePlayer.RemoveItem(heaviestItem);
                    if (singleDropResult)
                    {
                        this.universe.ActiveLocation.Items.Add(heaviestItem);
                        isItemDropped = true;
                    }
                }

                itemWeight = this.universe.ActivePlayer.GetActualPayload();;
            } while (itemWeight > this.universe.ActivePlayer.MaxPayload);

            if (isItemDropped)
            {
                PrintingSubsystem.Resource(Descriptions.GRAVITY_ITEM_DROP);
            }
            else
            {
                PrintingSubsystem.Resource(Descriptions.GRAVITY_NORMAL);    
            }
        }
    }

    private void ResetItemWeight(ICollection<Item> items)
    {
        foreach (var item in items)
        {
            item.Weight = 0;
        }
    }

    private void SetItemWeight(ICollection<Item> items)
    {
        foreach (var item in items)
        {
            if (ItemWeights.WeightDictionary.ContainsKey(item.Key))
            {
                item.Weight = ItemWeights.WeightDictionary[item.Key];    
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