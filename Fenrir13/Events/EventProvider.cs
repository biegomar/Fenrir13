using System.Text;
using Fenrir13.GamePlay;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Exceptions;
using Heretic.InteractiveFiction.GamePlay.EventSystem.EventArgs;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Resources;
using Heretic.InteractiveFiction.Subsystems;
using PowerArgs;

namespace Fenrir13.Events;

internal class EventProvider
{
    private readonly Universe universe;
    private readonly IPrintingSubsystem PrintingSubsystem;
    private bool isPowerBarEaten;
    private bool isAccessGranted;
    private bool isAirlockOpen;

    internal IDictionary<string, int> ScoreBoard => this.universe.ScoreBoard;

    public EventProvider(Universe universe, IPrintingSubsystem printingSubsystem)
    {
        this.PrintingSubsystem = printingSubsystem;
        this.universe = universe;
        this.isPowerBarEaten = false;
        this.isAccessGranted = false;
        this.isAirlockOpen = false;
    }

    internal void PullLever(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item lever && lever.Key == Keys.PANEL_TOP_LEVER)
        {
            throw new PullException(Descriptions.PANEL_TOP_LEVER_PULL);
        }
    }
    
    internal void PullFridgeHandle(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item fridgeHandle && fridgeHandle.Key == Keys.FRIDGE_DOOR_HANDLE)
        {
            var fridge = this.universe.ActiveLocation.GetItemByKey(Keys.FRIDGE);
            if (fridge != default)
            {
                fridgeHandle.IsPickAble = true;
                PrintingSubsystem.Resource(Descriptions.FRIDGE_DOOR_HANDLE_PULL);
                var isHandlePicked = this.universe.PickObject(fridgeHandle, true);
                if (!isHandlePicked || !this.universe.ActivePlayer.Items.Contains(fridgeHandle))
                {
                    fridge.Items.Remove(fridgeHandle);
                    this.universe.ActiveLocation.Items.Add(fridgeHandle);
                    PrintingSubsystem.Resource(Descriptions.FRIDGE_DOOR_HANDLE_DROP);
                }

                fridge.IsClosed = true;
                fridge.IsLocked = true;

                fridgeHandle.IsHidden = false;
                fridgeHandle.ContainmentDescription = string.Empty;
                    
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
                    doorHandle.LinkedToDescription = Descriptions.FRIDGE_DOOR_HANDLE_FLAP_LINKEDTO;
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

    internal void PushGreenButton(object sender, PushItemEventArgs eventArgs)
    {
        if (sender is Item greenButton && greenButton.Key == Keys.AIRLOCK_KEYPAD_GREEN_BUTTON)
        {
            if (this.isAirlockOpen)
            {
                this.isAirlockOpen = false;
                PrintingSubsystem.Resource(Descriptions.PRESS_GREEN_BUTTON);
            }
            else
            {
                PrintingSubsystem.Resource(BaseDescriptions.NOTHING_HAPPENS);
            }
        }
    }

    internal void SitDownOnChairInCryoChamber(object sender, SitDownEventArgs eventArgs)
    {
        if (sender is Location activeLocation && activeLocation.Key == Keys.CRYOCHAMBER)
        {
            if (string.IsNullOrEmpty(eventArgs.ExternalItemKey))
            {
                throw new SitDownException(Descriptions.WHERE_TO_SIT);
            }

            if (eventArgs.ExternalItemKey == Keys.CHAIR)
            {
                throw new SitDownException(Descriptions.TRY_TO_SIT_ON_CHAIR);
            }

            if (eventArgs.ExternalItemKey == Keys.OFFICECHAIR)
            {
                throw new SitDownException(Descriptions.TRY_TO_SIT_ON_OFFICECHAIR);
            }

            throw new SitDownException(BaseDescriptions.NO_SEAT);
        }
    }

    internal void TryToTakeThingsFromCryoChamber(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location activeLocation && activeLocation.Key == Keys.CRYOCHAMBER)
        {
            if (eventArgs.ExternalItemKey == Keys.LAPTOP)
            {
                throw new TakeException(Descriptions.TRY_TO_TAKE_LAPTOP);
            }
            
            throw new TakeException(this.GetRandomPhrase(BaseDescriptions.IMPOSSIBLE_PICKUP));
        }
    }
    
    internal void TryToTakeThingsFromGym(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location activeLocation && activeLocation.Key == Keys.GYM)
        {
            if (eventArgs.ExternalItemKey == Keys.GYM_ROPES)
            {
                throw new TakeException(Descriptions.GYM_ROPES_NO_PICKUP);
            }
            
            if (eventArgs.ExternalItemKey == Keys.GYM_SANDBAG)
            {
                throw new TakeException(Descriptions.GYM_SANDBAG_NO_PICKUP);
            }
            
            throw new TakeException(this.GetRandomPhrase(BaseDescriptions.IMPOSSIBLE_PICKUP));
        }
    }
    
    internal void PushRedButton(object sender, PushItemEventArgs eventArgs)
    {
        if (sender is Item redButton && redButton.Key == Keys.AIRLOCK_KEYPAD_RED_BUTTON)
        {
            if (this.isAirlockOpen)
            {
                PrintingSubsystem.Resource(BaseDescriptions.NOTHING_HAPPENS);
                return;
            }
            
            var listOfPossibleWearables = new List<string>()
            {
                Keys.BELT,
                Keys.GLOVES,
                Keys.HELMET,
                Keys.BOOTS
            };

            var clothes = this.universe.ActivePlayer.Clothes.Select(x => x.Key).Intersect(listOfPossibleWearables).ToList();
            var isPlayerPreparedForSpaceWalk = clothes.Count == listOfPossibleWearables.Count;

            if (isPlayerPreparedForSpaceWalk)
            {
                var helmet = this.universe.ActivePlayer.Clothes.First(x => x.Key == Keys.HELMET);

                var isHelmetLinkedToOxygen = helmet.LinkedTo.Any(x => x.Key == Keys.OXYGEN_BOTTLE);
                if (isHelmetLinkedToOxygen)
                {
                    var belt = this.universe.ActivePlayer.Clothes.First(x => x.Key == Keys.BELT);
                    var eyelet = belt.GetUnhiddenItemByKey(Keys.EYELET);
                
                    var isEyeletLinkedToRope = eyelet.LinkedTo.Any(x => x.Key == Keys.AIRLOCK_ROPE);
                    if (isEyeletLinkedToRope)
                    {
                        this.isAirlockOpen = true;
                        PrintingSubsystem.Resource(Descriptions.PREPARED_FOR_SPACE_WALK);
                        this.universe.Score += this.universe.ScoreBoard[nameof(this.PushRedButton)];
                        this.universe.SolveQuest(MetaData.QUEST_VI);
                    }
                    else
                    {
                        throw new PushException(Descriptions.SAFETY_FIRST);
                    }
                }
                else
                {
                    throw new PushException(Descriptions.OXYGEN_NEEDED);
                }

            }
            else
            {
                throw new PushException(Descriptions.SPACE_CLOTHS_NEEDED);
            }
        }
    }

    internal void LookAtDisplay(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.DISPLAY)
        {
            if (this.isPowerBarEaten)
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
                    this.universe.SolveQuest(MetaData.QUEST_I);
                }
            }
            else
            {
                PrintingSubsystem.Resource(Descriptions.DISPLAY_BAR_NOT_EATEN);
            }
        }
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
            PrintingSubsystem.Resource(this.universe.ActiveLocation.Surroundings[eventArgs.ExternalItemKey]());
            
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
        else
        {
            throw new OpenException(BaseDescriptions.DOES_NOT_WORK);
        }
    }
    
    private void OpenClosetDoorAgain(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location cryoChamber && cryoChamber.Key == Keys.CRYOCHAMBER && eventArgs.ExternalItemKey == Keys.CLOSET_DOOR)
        {
            PrintingSubsystem.Resource(this.universe.ActiveLocation.Surroundings[eventArgs.ExternalItemKey].ToString());
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
    
    internal void LookAtRedDots(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location engineRoom && engineRoom.Key == Keys.ENGINE_ROOM && eventArgs.ExternalItemKey == Keys.ENGINE_ROOM_RED_DOTS)
        {
            var lever = this.universe.GetObjectFromWorldByKey(Keys.PANEL_TOP_LEVER);
            if (lever != default)
            {
                lever.ContainmentDescription = Descriptions.PANEL_TOP_LEVER_REDDOTS_CONTAINMENT;
            }
            
            this.universe.Score += this.universe.ScoreBoard[nameof(this.LookAtRedDots)];
            engineRoom.AfterLook -= LookAtRedDots;
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
            this.isPowerBarEaten = true;
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
            if (this.isPowerBarEaten)
            {
                var suite = this.universe.ActiveLocation.GetItemByKey(Keys.SPACE_SUIT);
                if (suite != default)
                {
                    throw new BeforeChangeLocationException(Descriptions.CANT_LEAVE_CHAMBER);
                }   
            }
        }
    }
    
    internal void CantLeaveWithoutOpenBulkHead(object sender, ChangeLocationEventArgs eventArgs)
    {
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK
                                       && eventArgs.NewDestinationNode.Location.Key == Keys.JETTY)
        {
            if (!isAirlockOpen)
            {
                throw new BeforeChangeLocationException(Descriptions.CANT_LEAVE_AIRLOCK);
            }
        }
    }
    
    internal void CantLeaveWithOpenBulkHeadOrTiedRope(object sender, ChangeLocationEventArgs eventArgs)
    {
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK
                                       && eventArgs.NewDestinationNode.Location.Key == Keys.MACHINE_CORRIDOR_MID)
        {
            if (isAirlockOpen)
            {
                throw new BeforeChangeLocationException(Descriptions.CANT_LEAVE_OPEN_AIRLOCK);
            }
            
            var belt = this.universe.ActivePlayer.Clothes.FirstOrDefault(x => x.Key == Keys.BELT);
            var eyelet = belt?.GetItemByKey(Keys.EYELET);
            
            if (belt != null && eyelet != default && eyelet.LinkedTo.Count > 0)
            {
                throw new BeforeChangeLocationException(Descriptions.CANT_LEAVE_WITH_TIED_EYELET);
            }
        }
    }
    
    internal void PullOnWearablesOnPlayer(object sender, PullItemEventArgs eventArgs)
    {
        void SwapItem(Item item)
        {
            this.universe.ActivePlayer.Items.Remove(item);
            this.universe.ActivePlayer.Clothes.Add(item);
            PrintingSubsystem.FormattedResource(Descriptions.PULLON_WEARABLE, item.Name, true);
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
    
    internal void PullOnWearables(object sender, PullItemEventArgs eventArgs)
    {
        void SwapItem(Item item)
        {
            this.universe.ActivePlayer.Items.Remove(item);
            this.universe.ActivePlayer.Clothes.Add(item);
            PrintingSubsystem.FormattedResource(Descriptions.PULLON_WEARABLE, item.Name, true);
        }

        var listOfPossibleWearables = new List<string>()
        {
            Keys.BELT,
            Keys.GLOVES,
            Keys.HELMET,
            Keys.BOOTS
        };
        
        if (sender is Item wearable && listOfPossibleWearables.Contains(wearable.Key))
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

    internal void BeforeTakeAntenna(object sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item antenna && antenna.Key == Keys.SOCIALROOM_ANTENNA)
        {
            if (this.universe.ActiveLocation.Key == Keys.SOCIALROOM)
            {
                if (!this.universe.ActivePlayer.HasClimbed || this.universe.ActivePlayer.ClimbedObject == default)
                {
                    throw new TakeException(Descriptions.CANT_REACH_ANTENNA);
                }
            
                if (this.universe.ActivePlayer.ClimbedObject.Key == Keys.SOCIALROOM_COUCH)
                {
                    throw new TakeException(Descriptions.CANT_TAKE_ANTENNA);
                }
            
                throw new TakeException(BaseDescriptions.NOTHING_HAPPENS);    
            }

            if (this.universe.ActiveLocation.Key == Keys.ROOF_TOP && antenna.LinkedTo.Count > 0)
            {
                throw new TakeException(Descriptions.DROID_ANTENNE_UNPICKABLE);
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
    
    internal void UseToolWithAntennaInSocialRoom(object sender, UseItemEventArgs eventArgs)
    {
        if (sender is Item itemOne 
            && eventArgs.ItemToUse is Item itemTwo 
            && itemOne.Key != itemTwo.Key
            && this.universe.ActiveLocation.Key == Keys.SOCIALROOM)
        {
            var itemList = new List<Item> { itemOne, itemTwo };
            var tool = itemList.SingleOrDefault(i => i.Key == Keys.MAINTENANCE_ROOM_TOOL);
            var antenna = itemList.SingleOrDefault(i => i.Key == Keys.SOCIALROOM_ANTENNA);

            if (tool != default && antenna != default)
            {
                if (!this.universe.ActivePlayer.HasClimbed || this.universe.ActivePlayer.ClimbedObject == default)
                {
                    throw new UseException(Descriptions.CANT_REACH_ANTENNA);
                }

                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.MAINTENANCE_ROOM_TOOL) == default)
                {
                    this.universe.PickObject(tool);
                }
                
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.SOCIALROOM_ANTENNA) == default)
                {
                    var receiver = this.universe.GetObjectFromWorldByKey(Keys.SOCIALROOM_ANTENNA_CONSTRUCTION);
                    if (receiver != default)
                    {
                        this.universe.ActivePlayer.Items.Add(antenna);
                        receiver.LinkedTo.Remove(antenna);
                        receiver.Description = Descriptions.SOCIALROOM_ANTENNA_CONSTRUCTION_WITHOUT_ANTENNA;
                    }
                }

                PrintingSubsystem.Resource(Descriptions.GET_ANTENNA);
                tool.Use -= UseToolWithAntennaInSocialRoom;
                antenna.Use -= UseToolWithAntennaInSocialRoom;

                this.universe.Score += this.universe.ScoreBoard[nameof(UseToolWithAntennaInSocialRoom)];
                this.universe.SolveQuest(MetaData.QUEST_IV);
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }

    

    internal void MountAntennaToDroid(object sender, UseItemEventArgs eventArgs)
    {
        if (sender is Item itemOne
            && eventArgs.ItemToUse is Item itemTwo
            && itemOne.Key != itemTwo.Key
            && this.universe.ActiveLocation.Key == Keys.ROOF_TOP)
        {
            var itemList = new List<Item> { itemOne, itemTwo };
            var tool = itemList.SingleOrDefault(i => i.Key == Keys.MAINTENANCE_ROOM_TOOL);

            if (tool != default)
            {
                this.UseToolWithAntennaOnRoofTop(sender, eventArgs);
            }
            else
            {
                this.UseAntennaOnDroid(sender, eventArgs);
            }
        }
    }
    
    private void UseToolWithAntennaOnRoofTop(object sender, UseItemEventArgs eventArgs)
    {
        if (sender is Item itemOne 
            && eventArgs.ItemToUse is Item itemTwo 
            && itemOne.Key != itemTwo.Key
            && this.universe.ActiveLocation.Key == Keys.ROOF_TOP)
        {
            var itemList = new List<Item> { itemOne, itemTwo };
            var tool = itemList.SingleOrDefault(i => i.Key == Keys.MAINTENANCE_ROOM_TOOL);
            var secondItem = itemList.SingleOrDefault(i => i.Key == Keys.SOCIALROOM_ANTENNA);
            Item antenna = secondItem;

            if (tool != default && secondItem == default)
            {
                secondItem = itemList.SingleOrDefault(i => i.Key == Keys.DROID);
                if (secondItem != default)
                {
                    antenna = this.universe.ActivePlayer.Items.SingleOrDefault(i => i.Key == Keys.SOCIALROOM_ANTENNA);
                }
            }
            
            if (tool != default && antenna != default)
            {
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.MAINTENANCE_ROOM_TOOL) == default)
                {
                    this.universe.PickObject(tool);
                }
                
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.SOCIALROOM_ANTENNA) == default)
                {
                    this.universe.PickObject(antenna);
                }
                
                var droid = this.universe.ActiveLocation.GetItemByKey(Keys.DROID);
                if (droid != default)
                {
                    this.universe.ActivePlayer.Items.Remove(antenna);
                    droid.LinkedTo.Add(antenna);
                    droid.FirstLookDescription = string.Empty;
                    
                    antenna.LinkedTo.Add(droid);
                    antenna.LinkedToDescription = Descriptions.DROID_ANTENNA_LINKDESCRIPTION;

                    PrintingSubsystem.Resource(Descriptions.DROID_ANTENNA_MOUNTED);
                    tool.Use -= MountAntennaToDroid;
                    antenna.Use -= MountAntennaToDroid;
                    droid.Use -= MountAntennaToDroid;

                    this.universe.Score += this.universe.ScoreBoard[nameof(MountAntennaToDroid)];
                    this.universe.SolveQuest(MetaData.QUEST_VII);
                }
                else
                {
                    throw new UseException(BaseDescriptions.DOES_NOT_WORK);
                }
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }
    
    private void UseAntennaOnDroid(object sender, UseItemEventArgs eventArgs)
    {
        if (sender is Item itemOne && eventArgs.ItemToUse is Item itemTwo && itemOne.Key != itemTwo.Key)
        {
            var itemList = new List<Item> { itemOne, itemTwo };
            var antenna = itemList.SingleOrDefault(i => i.Key == Keys.SOCIALROOM_ANTENNA);
            var droid = itemList.SingleOrDefault(i => i.Key == Keys.DROID);
            
            if (antenna != default && droid != default)
            {
                var tool = this.universe.GetObjectFromWorldByKey(Keys.MAINTENANCE_ROOM_TOOL) as Item;
                if (tool == default)
                {
                    throw new UseException(Descriptions.MAINTENANCE_ROOM_TOOL_NOT_PRESENT); 
                }
                
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.MAINTENANCE_ROOM_TOOL) == default)
                {
                    if (!this.universe.PickObject(tool))
                    {
                        throw new UseException(Descriptions.MAINTENANCE_ROOM_TOOL_NOT_PRESENT);
                    }
                }
                
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.SOCIALROOM_ANTENNA) == default)
                {
                    this.universe.PickObject(antenna);
                }
                
                this.universe.ActivePlayer.Items.Remove(antenna);
                droid.LinkedTo.Add(antenna);
                droid.FirstLookDescription = string.Empty;
                
                antenna.LinkedTo.Add(droid);
                antenna.LinkedToDescription = Descriptions.DROID_ANTENNA_LINKDESCRIPTION;

                PrintingSubsystem.Resource(Descriptions.DROID_ANTENNA_MOUNTED);
                
                tool.Use -= MountAntennaToDroid;
                antenna.Use -= MountAntennaToDroid;
                droid.Use -= MountAntennaToDroid;

                this.universe.Score += this.universe.ScoreBoard[nameof(MountAntennaToDroid)];
                this.universe.SolveQuest(MetaData.QUEST_VII);
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
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
                helmet.FirstLookDescription = string.Empty;

                PrintingSubsystem.Resource(Descriptions.LINK_OXYGEN_BOTTLE_TO_HELMET);
                oxygenBottle.Use -= UseOxygenBottleWithHelmet;
                helmet.Use -= UseOxygenBottleWithHelmet;

                this.universe.Score += this.universe.ScoreBoard[nameof(UseOxygenBottleWithHelmet)];
                this.universe.SolveQuest(MetaData.QUEST_V);
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }

    internal void UseDumbbellBarWithLever(object sender, UseItemEventArgs eventArgs)
    {
        if (sender is Item itemOne && eventArgs.ItemToUse is Item itemTwo && itemOne.Key != itemTwo.Key)
        {
            var itemList = new List<Item> { itemOne, itemTwo };
            var dumbbellBar = itemList.SingleOrDefault(i => i.Key == Keys.DUMBBELL_BAR);
            var lever = itemList.SingleOrDefault(i => i.Key == Keys.PANEL_TOP_LEVER);
            
            if (dumbbellBar != default && lever != default)
            {
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.DUMBBELL_BAR) == default)
                {
                    this.universe.PickObject(dumbbellBar);
                }
                
                PrintingSubsystem.Resource(Descriptions.PANEL_TOP_LEVER_DUMBBELL_BAR);
                dumbbellBar.Use -= UseDumbbellBarWithLever;
                lever.Use -= UseDumbbellBarWithLever;

                var droid = this.universe.GetObjectFromWorldByKey(Keys.DROID);
                if (droid != default)
                {
                    droid.ContainmentDescription = Descriptions.DROID_ENERGY_CONTAINMENT;
                }
                

                this.universe.Score += this.universe.ScoreBoard[nameof(UseDumbbellBarWithLever)];
                this.universe.SolveQuest(MetaData.QUEST_VIII);
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }

    internal void UseAirlockRopeWithEyeletOrBelt(object sender, UseItemEventArgs eventArgs)
    {
        if (sender is Item itemOne && eventArgs.ItemToUse is Item itemTwo && itemOne.Key != itemTwo.Key)
        {
            var itemList = new List<Item> { itemOne, itemTwo };
            var rope = itemList.SingleOrDefault(i => i.Key == Keys.AIRLOCK_ROPE);
            var endPoint = itemList.SingleOrDefault(i => i.Key == Keys.EYELET);

            if (rope != default && endPoint == default)
            {
                endPoint = itemList.SingleOrDefault(i => i.Key == Keys.BELT);
                if (endPoint != default)
                {
                    endPoint.Use -= UseAirlockRopeWithEyeletOrBelt;
                    endPoint = endPoint.Items.SingleOrDefault(i => i.Key == Keys.EYELET);
                }
            }

            if (rope != default && endPoint != default)
            {
                if (this.universe.ActivePlayer.GetUnhiddenItemByKey(Keys.BELT) == default)
                {
                    var belt = this.universe.ActiveLocation.GetItemByKey(Keys.BELT);
                    this.universe.PickObject(belt);
                }
                
                rope.LinkedTo.Add(endPoint);
                endPoint.LinkedTo.Add(rope);

                PrintingSubsystem.Resource(Descriptions.LINK_ROPE_TO_EYELET);
                rope.Use -= UseAirlockRopeWithEyeletOrBelt;
                endPoint.Use -= UseAirlockRopeWithEyeletOrBelt;

                this.universe.Score += this.universe.ScoreBoard[nameof(UseAirlockRopeWithEyeletOrBelt)];
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
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
                this.universe.SolveQuest(MetaData.QUEST_III);
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
    
    internal void TakeTool(object sender, ContainerObjectEventArgs eventArg)
    {
        if (sender is Item tool && tool.Key == Keys.MAINTENANCE_ROOM_TOOL)
        {
            this.universe.Score += this.universe.ScoreBoard[nameof(this.TakeTool)];
            tool.AfterTake -= TakeTool;
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
                    this.universe.SolveQuest(MetaData.QUEST_II);
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

    internal void DropClothsWithOpenAirlock(object sender, ContainerObjectEventArgs eventArgs)
    {
        var listOfPossibleWearables = new List<string>()
        {
            Keys.BELT,
            Keys.GLOVES,
            Keys.HELMET,
            Keys.BOOTS
        };

        if (sender is Item cloth && listOfPossibleWearables.Contains(cloth.Key) && this.isAirlockOpen)
        {
            throw new BeforeDropException(Descriptions.AIRLOCK_OPEN_DROP_CLOTHES);
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
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK 
                                       && eventArgs.NewDestinationNode.Location.Key == Keys.MACHINE_CORRIDOR_MID 
                                       && !this.isAirlockOpen)
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
    
    private string GetRandomPhrase(string message)
    {
        var phrases = message.Split("|");

        var rand = new Random();
        var index = rand.Next(phrases.Length);

        return phrases[index];
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