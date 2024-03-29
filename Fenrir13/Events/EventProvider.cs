using System.Text;
using Fenrir13.GamePlay;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Exceptions;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.GamePlay.EventSystem.EventArgs;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Resources;
using Heretic.InteractiveFiction.Subsystems;
using PowerArgs;

namespace Fenrir13.Events;

internal class EventProvider
{
    private readonly Universe universe;
    private readonly ObjectHandler objectHandler;
    private readonly IPrintingSubsystem PrintingSubsystem;
    private readonly ScoreBoard scoreBoard;
    private bool isPowerBarEaten;
    private bool isAccessGranted;
    private bool isAirlockOpen;
    
    public EventProvider(Universe universe, IPrintingSubsystem printingSubsystem, ScoreBoard scoreBoard)
    {
        this.PrintingSubsystem = printingSubsystem;
        this.scoreBoard = scoreBoard;
        this.universe = universe;
        this.objectHandler = new ObjectHandler(this.universe);
        this.isPowerBarEaten = false;
        this.isAccessGranted = false;
        this.isAirlockOpen = false;
    }
    
    internal void RegisterScore(string key, int value)
    {
        this.scoreBoard.RegisterScore(key, value);
    }

    internal void PullLever(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item lever && lever.Key == Keys.PANEL_TOP_LEVER)
        {
            throw new PullException(Descriptions.PANEL_TOP_LEVER_PULL);
        }
    }
    
    internal void PullFridgeHandle(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item fridgeHandle && fridgeHandle.Key == Keys.FRIDGE_DOOR_HANDLE)
        {
            var fridge = this.universe.ActiveLocation.GetItem(Keys.FRIDGE);
            if (fridge != default)
            {
                fridgeHandle.IsPickable = true;
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
                    
                this.scoreBoard.WinScore(nameof(PullFridgeHandle));
                fridgeHandle.Pull -= PullFridgeHandle;
            }
        }
    }
    
    internal void PushDoorHandleIntoRespiratorFlap(object? sender, PushItemEventArgs eventArgs)
    {
        if (sender is Item doorHandle && doorHandle.Key == Keys.FRIDGE_DOOR_HANDLE)
        {
            if (eventArgs.ItemToUse is Item respiratorFlap && respiratorFlap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP)
            {
                UseHandleOnFlap(respiratorFlap, doorHandle);
            }
            else
            {
                throw new PushException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }

    internal void UseDoorHandleWithRespiratorFlap(object? sender, UseItemEventArgs eventArgs)
    {
        if (sender is Item doorHandle && doorHandle.Key == Keys.FRIDGE_DOOR_HANDLE)
        {
            if (eventArgs.ItemToUse is Item respiratorFlap && respiratorFlap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP)
            {
                UseHandleOnFlap(respiratorFlap, doorHandle);
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }
    
    private void UseHandleOnFlap(Item respiratorFlap, Item doorHandle)
    {
        var respirator = this.universe.ActiveLocation.GetItem(Keys.AMBULANCE_RESPIRATOR);
        if (respirator != default)
        {
            respirator.IsLocked = false;
            respiratorFlap.IsLocked = false;
            respiratorFlap.LinkedTo.Add(doorHandle);
            doorHandle.LinkedTo.Add(respiratorFlap);
            respiratorFlap.FirstLookDescription = string.Empty;
            doorHandle.LinkedToDescription = Descriptions.FRIDGE_DOOR_HANDLE_FLAP_LINKEDTO;
            doorHandle.IsPickable = false;
            doorHandle.UnPickAbleDescription = Descriptions.FRIDGE_DOOR_HANDLE_FLAP_UNPICKABLE;
            this.universe.ActivePlayer.Items.Remove(doorHandle);
            PrintingSubsystem.Resource(Descriptions.FRIDGE_DOOR_HANDLE_PUSHED);
            
            this.scoreBoard.WinScore(nameof(PushDoorHandleIntoRespiratorFlap));
            doorHandle.Push -= PushDoorHandleIntoRespiratorFlap;
            doorHandle.Use -= UseDoorHandleWithRespiratorFlap;
            doorHandle.Connect -= ConnectDoorHandleWithRespiratorFlap;
        }
    }

    internal void TryToDisconnectHandleFromFlap(object? sender, DisconnectEventArgs eventArgs)
    {
        if (sender is Item { IsLinked: true })
        {
            throw new DisconnectException("Der Griff steckt fest in der Klappe und kann nicht wieder entfernt werden.");
        }
    }
    
    internal void ConnectDoorHandleWithRespiratorFlap(object? sender, ConnectEventArgs eventArgs)
    {
        if (sender is Item doorHandle && doorHandle.Key == Keys.FRIDGE_DOOR_HANDLE)
        {
            if (eventArgs.ItemToUse is Item respiratorFlap && respiratorFlap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP)
            {
                ConnectHandleOnFlap(respiratorFlap, doorHandle);
            }
            else
            {
                throw new ConnectException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }
    
    private void ConnectHandleOnFlap(Item respiratorFlap, Item doorHandle)
    {
        var respirator = this.universe.ActiveLocation.GetItem(Keys.AMBULANCE_RESPIRATOR);
        if (respirator != default)
        {
            respirator.IsLocked = false;
            respiratorFlap.IsLocked = false;
            respiratorFlap.FirstLookDescription = string.Empty;
            doorHandle.LinkedToDescription = Descriptions.FRIDGE_DOOR_HANDLE_FLAP_LINKEDTO;
            doorHandle.IsPickable = false;
            doorHandle.UnPickAbleDescription = Descriptions.FRIDGE_DOOR_HANDLE_FLAP_UNPICKABLE;
            this.universe.ActivePlayer.Items.Remove(doorHandle);
            PrintingSubsystem.Resource(Descriptions.FRIDGE_DOOR_HANDLE_PUSHED);
            
            this.scoreBoard.WinScore(nameof(PushDoorHandleIntoRespiratorFlap));
            doorHandle.Push -= PushDoorHandleIntoRespiratorFlap;
            doorHandle.Use -= UseDoorHandleWithRespiratorFlap;
            doorHandle.Connect -= ConnectDoorHandleWithRespiratorFlap;
        }
    }

    internal void PushGreenButton(object? sender, PushItemEventArgs eventArgs)
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

    internal void SitDownOnChairInCryoChamber(object? sender, SitDownEventArgs eventArgs)
    {
        if (sender is Item chair)
        {
            if (chair.Key == Keys.CHAIR)
            {
                throw new SitDownException(Descriptions.TRY_TO_SIT_ON_CHAIR);
            }

            if (chair.Key == Keys.OFFICECHAIR)
            {
                throw new SitDownException(Descriptions.TRY_TO_SIT_ON_OFFICECHAIR);
            }

            throw new SitDownException(BaseDescriptions.NO_SEAT);
        }
    }
    
    internal void SitDownOnCouchInSocialRoom(object? sender, SitDownEventArgs eventArgs)
    {
        if (sender is Item item)
        {
            if (item.Key == Keys.SOCIALROOM_SEAT)
            {
                throw new SitDownException(Descriptions.TRY_TO_SIT_ON_SEAT);
            }

            if (item.Key == Keys.SOCIALROOM_GLASS_TABLE)
            {
                throw new SitDownException(Descriptions.TRY_TO_SIT_ON_GLASS_TABLE);    
            }

            if (item.Key == Keys.SOCIALROOM_BILLARD)
            {
                throw new SitDownException(Descriptions.TRY_TO_SIT_ON_BILLARD_TABLE);
            }

            throw new SitDownException(BaseDescriptions.NO_SEAT);
        }
    }
    
    internal void SitDownOnChairInKitchen(object? sender, SitDownEventArgs eventArgs)
    {
        if (sender is Item chair && chair.Key == Keys.CHAIR)
        {
            throw new SitDownException(Descriptions.TRY_TO_SIT_ON_CHAIR);
        }
    }

    internal void TryToTakeThingsFromGym(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item item)
        {
            if (item.Key == Keys.GYM_ROPES)
            {
                throw new TakeException(Descriptions.GYM_ROPES_NO_PICKUP);
            }
            
            if (item.Key == Keys.GYM_SANDBAG)
            {
                throw new TakeException(Descriptions.GYM_SANDBAG_NO_PICKUP);
            }
            
            throw new TakeException(this.GetRandomPhrase(BaseDescriptions.IMPOSSIBLE_PICKUP));
        }
    }

    internal void PushRedButton(object? sender, PushItemEventArgs eventArgs)
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
                    var isBeltLinkedToRope = belt.LinkedTo.Any(x => x.Key == Keys.AIRLOCK_ROPE);
                    if (isBeltLinkedToRope)
                    {
                        this.isAirlockOpen = true;
                        PrintingSubsystem.Resource(Descriptions.PREPARED_FOR_SPACE_WALK);
                        if (!this.universe.IsQuestSolved(MetaData.QUEST_VI))
                        {
                            this.scoreBoard.WinScore(nameof(PushRedButton));
                            this.universe.SolveQuest(MetaData.QUEST_VI);
                        }
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

    internal void LookAtDisplay(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item display && display.Key == Keys.DISPLAY)
        {
            if (this.isPowerBarEaten)
            {
                PrintingSubsystem.Resource(Descriptions.DISPLAY_BAR_EATEN);
                PrintingSubsystem.ForegroundColor = TextColor.Red;
                PrintingSubsystem.Resource(Descriptions.INTERVENTION_REQUIRED);
                PrintingSubsystem.ResetColors();
                PrintingSubsystem.Resource(Descriptions.INTERVENTION_REQUIRED_INTERPRETATION);
                
                var corridorMap = this.objectHandler.GetDestinationNodeFromActiveLocationByDirection(Directions.W);
                var corridor = corridorMap?.Location;
                if (corridor is { IsLocked: true })
                {
                    corridor.IsLocked = false;
                    this.scoreBoard.WinScore(nameof(LookAtDisplay));
                    display.AfterLook -= LookAtDisplay;
                    this.universe.SolveQuest(MetaData.QUEST_I);
                }
            }
            else
            {
                PrintingSubsystem.Resource(Descriptions.DISPLAY_BAR_NOT_EATEN);
            }
        }
    }

    internal void LookAtClosetDoor(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item closetDoor && closetDoor.Key == Keys.CLOSET_DOOR)
        {
            HandleClosetPart();
        }
    }

    internal void OpenCloset(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item closetPart && (closetPart.Key == Keys.CLOSET || closetPart.Key == Keys.CLOSET_DOOR))
        {
            PrintingSubsystem.Resource(Descriptions.CLOSET_DOOR);
            HandleClosetPart();
        }
        else
        {
            throw new OpenException(BaseDescriptions.IMPOSSIBLE_OPEN);
        }
    }

    internal void CloseCloset(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item closetPart && (closetPart.Key == Keys.CLOSET || closetPart.Key == Keys.CLOSET_DOOR))
        {
            CloseClosetPart();
        }
        else
        {
            throw new CloseException(BaseDescriptions.IMPOSSIBLE_CLOSE);
        }
    }
    
    private void CloseClosetPart()
    {
        if (this.universe.ActiveLocation.GetItem(Keys.CLOSET_DOOR) is { } closetDoor)
        {
            closetDoor.IsClosed = true;
        }

        if (this.universe.ActiveLocation.GetItem(Keys.CLOSET) is { } closet)
        {
            closet.IsClosed = true;
        }
    }
    
    private void HandleClosetPart()
    {
        void prepareObject(Item item)
        {
            item.AfterLook -= LookAtClosetDoor;
            item.Open -= OpenCloset;
            item.Open += OpenClosetAgain;
            item.IsClosed = false;
        }

        PrintingSubsystem.Resource(Descriptions.CLOSET_DOOR_FIRSTLOOK);
        var bar = this.universe.ActiveLocation.GetItem(Keys.CHOCOLATEBAR);
        if (bar != default)
        {
            bar.IsHidden = false;
            this.scoreBoard.WinScore(nameof(LookAtClosetDoor));
        }
        
        var closetDoor = this.universe.ActiveLocation.GetItem(Keys.CLOSET_DOOR);
        if (closetDoor != default)
        {
            prepareObject(closetDoor);
        }
        
        var closet = this.universe.ActiveLocation.GetItem(Keys.CLOSET);
        if (closet != default)
        {
            prepareObject(closet);
        }
    }

    private void OpenClosetAgain(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item closetPart && (closetPart.Key == Keys.CLOSET_DOOR || closetPart.Key == Keys.CLOSET))
        {
            if (this.universe.ActiveLocation.GetItem(Keys.CLOSET_DOOR) is { } closetDoor)
            {
                closetDoor.IsClosed = true;
            }

            if (this.universe.ActiveLocation.GetItem(Keys.CLOSET) is { } closet)
            {
                closet.IsClosed = true;
            }
        }
    }
    
    internal void RopeSkipping(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item rope && rope.Key == Keys.GYM_ROPES)
        {
            throw new JumpException(Descriptions.ROPE_SKIPPING);
        }
        
        throw new JumpException(BaseDescriptions.NOTHING_HAPPENS);
    }
    
    internal void TakeLaptop(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item laptop && laptop.Key == Keys.LAPTOP)
        {
            throw new TakeException(Descriptions.TRY_TO_TAKE_LAPTOP);
        }
        
        throw new TakeException(BaseDescriptions.NOTHING_HAPPENS);
    }

    internal void LookAtRespirator(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item respirator && respirator.Key == Keys.AMBULANCE_RESPIRATOR)
        {
            respirator.ContainmentDescription = string.Empty;
            respirator.BeforeLook -= LookAtRespirator;
        }
    }
    
    internal void LookAtRedDots(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location engineRoom && engineRoom.Key == Keys.ENGINE_ROOM && eventArgs.ExternalItemKey == Keys.ENGINE_ROOM_RED_DOTS)
        {
            var lever = this.objectHandler.GetObjectFromWorldByKey(Keys.PANEL_TOP_LEVER);
            if (lever != default)
            {
                lever.ContainmentDescription = Descriptions.PANEL_TOP_LEVER_REDDOTS_CONTAINMENT;
            }
            
            this.scoreBoard.WinScore(nameof(LookAtRedDots));
            engineRoom.AfterLook -= LookAtRedDots;
        }
    }

    internal void TurnHologram(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item hologram && hologram.Key == Keys.ENGINE_ROOM_SHIP_MODEL)
        {
            this.PrintingSubsystem.Resource(Descriptions.TURN_SHIP_MODEL);
        }
    }
    
    internal void TryToOpenCryoChamberDoor(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item cryoChamberDoor && cryoChamberDoor.Key == Keys.CRYOCHAMBER_DOOR)
        {
            PrintingSubsystem.Resource(Descriptions.CRYOCHAMBER_DOOR_TRY_OPEN);
        }
    }
    
    internal void EatChocolateBar(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item powerBar && powerBar.Key == Keys.CHOCOLATEBAR)
        {
            this.scoreBoard.WinScore(nameof(EatChocolateBar));
            powerBar.Eat -= EatChocolateBar;
            this.isPowerBarEaten = true;

            throw new EatException(Descriptions.CHOCOLATEBAR_EATEN);
        }
    }
    
    internal void TakeSpaceSuite(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item spaceSuite && spaceSuite.Key == Keys.SPACE_SUIT)
        {
            this.universe.ActivePlayer.FirstLookDescription = string.Empty;
            this.universe.ActivePlayer.Clothes.Add(spaceSuite);
            this.universe.ActivePlayer.Items.Remove(spaceSuite);
            
            this.scoreBoard.WinScore(nameof(TakeSpaceSuite));
            PrintingSubsystem.Resource(Descriptions.SPACE_SUIT_TAKEN);
            spaceSuite.AfterTake -= TakeSpaceSuite;
        }
    }
    
    internal void CantLeaveWithoutSuiteAndUneatenBar(object? sender, LeaveLocationEventArgs eventArgs)
    {
        if (sender is Location chamber && chamber.Key == Keys.CRYOCHAMBER
            && eventArgs.NewDestinationNode?.Location?.Key == Keys.CORRIDOR_EAST)
        {
            if (this.isPowerBarEaten)
            {
                var suite = this.universe.ActiveLocation.GetItem(Keys.SPACE_SUIT);
                if (suite != default)
                {
                    throw new LeaveLocationException(Descriptions.CANT_LEAVE_CHAMBER);
                }   
            }
        }
    }
    
    internal void CantLeaveWithoutOpenBulkHead(object? sender, LeaveLocationEventArgs eventArgs)
    {
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK
                                       && eventArgs.NewDestinationNode?.Location?.Key == Keys.JETTY)
        {
            if (!isAirlockOpen)
            {
                throw new LeaveLocationException(Descriptions.CANT_LEAVE_AIRLOCK);
            }
        }
    }
    
    internal void CantLeaveWithOpenBulkHeadOrTiedRope(object? sender, LeaveLocationEventArgs eventArgs)
    {
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK
                                       && eventArgs.NewDestinationNode?.Location?.Key == Keys.MACHINE_CORRIDOR_MID)
        {
            if (isAirlockOpen)
            {
                throw new LeaveLocationException(Descriptions.CANT_LEAVE_OPEN_AIRLOCK);
            }
            
            var belt = this.universe.ActivePlayer.Clothes.SingleOrDefault(x => x.Key == Keys.BELT);
            if (belt == default)
            {
                belt = this.universe.ActivePlayer.Items.SingleOrDefault(x => x.Key == Keys.BELT);
            }
            
            if (belt is { IsLinked: true })
            {
                throw new LeaveLocationException(Descriptions.CANT_LEAVE_WITH_TIED_EYELET);
            }
        }
    }
    
    internal void BeforeStandUp(object? sender, ContainerObjectEventArgs eventArgs)
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

    internal void BeforeTakeAntenna(object? sender, ContainerObjectEventArgs eventArgs)
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
    
    internal void AfterStandUp(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Player you && you.Key == Keys.PLAYER)
        {
            if (this.universe.ActiveLocation.Key == Keys.COMMANDBRIDGE)
            {
                PrintingSubsystem.ActiveLocation(this.universe.ActiveLocation, this.universe.LocationMap);
            }
        }
    }
    
    internal void AfterSitDown(object? sender, SitDownEventArgs eventArgs)
    {
        if (sender is Player you && you.Key == Keys.PLAYER)
        {
            if (you.IsSitting && this.universe.ActiveLocation.Key == Keys.COMMANDBRIDGE)
            {
                var location = this.objectHandler.GetLocationByKey(Keys.COMPUTER_TERMINAL);
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
    
    internal void AfterSitDownOnQuestsSolved(object? sender, SitDownEventArgs eventArgs)
    {
        if (this.universe.IsQuestSolved(MetaData.QUEST_VII) && this.universe.IsQuestSolved(MetaData.QUEST_VIII))
        {
            if (sender is Player you && you.Key == Keys.PLAYER)
            {
                if (you.IsSitting && this.universe.ActiveLocation.Key == Keys.COMMANDBRIDGE)
                {
                    var location = this.objectHandler.GetLocationByKey(Keys.COMPUTER_TERMINAL);
                    if (location is { } terminal)
                    {
                        this.universe.ActiveLocation = terminal;
                        PrintingSubsystem.ActiveLocation(this.universe.ActiveLocation, this.universe.LocationMap);
                        
                        this.scoreBoard.WinScore(nameof(AfterSitDownOnQuestsSolved));
                        this.universe.SolveQuest(MetaData.QUEST_IX);
                    }
                }
            }
        }
    }
    
    internal void AfterSitDownOnCouch(object? sender, SitDownEventArgs eventArgs)
    {
        if (sender is Player you && you.Key == Keys.PLAYER)
        {
            if (you.IsSitting && eventArgs.ItemToSitOn?.Key == Keys.SOCIALROOM_COUCH)
            {
                PrintingSubsystem.Resource(Descriptions.TRY_TO_SIT_ON_COUCH);
                you.StandUpFromSeat();
            }
        }
    }
    
    internal void ClimbOnTablesInSocialRoom(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item table)
        {
            if (table.Key == Keys.SOCIALROOM_BILLARD || table.Key == Keys.SOCIALROOM_GLASS_TABLE)
            {
                if (this.universe.ActivePlayer.HasClimbed)
                {
                    throw new ClimbException(BaseDescriptions.ALREADY_CLIMBED);
                }

                throw new ClimbException(Descriptions.SOCIALROOM_CANT_REACH_RECEIVER_FROM_HERE);
            }
            
            throw new ClimbException(BaseDescriptions.IMPOSSIBLE_CLIMB);
        }
    }
    
    internal void LookAtControlPanel(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location bridge && bridge.Key == Keys.COMMANDBRIDGE && eventArgs.ExternalItemKey == Keys.CONTROL_PANEL)
        {
            var note = this.universe.ActiveLocation.GetItem(Keys.STICKY_NOTE);
            if (note != default)
            {
                note.IsHidden = false;
                PrintingSubsystem.Resource(Descriptions.STICKY_NOTE_FOUND);
                this.universe.PickObject(note, true);
                
                this.scoreBoard.WinScore(nameof(LookAtControlPanel));
                bridge.AfterLook -= LookAtControlPanel;
            }
        }
    }

    internal void LookAtPowerStation(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Location gym && gym.Key == Keys.GYM && eventArgs.ExternalItemKey == Keys.GYM_POWERSTATION)
        {
            var rack = this.universe.ActiveLocation.GetItem(Keys.DUMBBELL_RACK);
            if (rack != default)
            {
                rack.IsHidden = false;
                gym.AfterLook -= LookAtPowerStation;
            }
        }
    }

    internal void UseToolWithAntennaInSocialRoom(object? sender, UseItemEventArgs eventArgs)
    {
        if (eventArgs.ItemToUse == default)
        {
            throw new UseException(BaseDescriptions.WHAT_TO_USE);
        }
        
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

                if (this.universe.ActivePlayer.GetUnhiddenItem(Keys.MAINTENANCE_ROOM_TOOL) == default)
                {
                    this.universe.PickObject(tool);
                }
                
                if (this.universe.ActivePlayer.GetUnhiddenItem(Keys.SOCIALROOM_ANTENNA) == default)
                {
                    var receiver = this.objectHandler.GetObjectFromWorldByKey(Keys.SOCIALROOM_ANTENNA_CONSTRUCTION);
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

                this.scoreBoard.WinScore(nameof(UseToolWithAntennaInSocialRoom));
                this.universe.SolveQuest(MetaData.QUEST_IV);
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }

    

    internal void MountAntennaToDroid(object? sender, ConnectEventArgs eventArgs)
    {
        if (this.universe.ActiveLocation.Key == Keys.ROOF_TOP)
        {
            if (sender is Item antenna)
            {
                if (eventArgs.ItemToUse is Item)
                {
                    this.ConnectAntennaToDroid(sender, eventArgs);
                    
                    return;
                }
                
                throw new ConnectException(string.Format(BaseDescriptions.WHAT_TO_CONNECT_TO,
                    ArticleHandler.GetNameWithArticleForObject(antenna, GrammarCase.Dative,
                        lowerFirstCharacter: true)));    
            }
        }
    }
    
    private void ConnectAntennaToDroid(object? sender, ConnectEventArgs eventArgs)
    {
        if (sender is Item antenna && eventArgs.ItemToUse is Item droid && droid.Key == Keys.DROID)
        {
            if (!this.universe.ActivePlayer.OwnsItem(Keys.MAINTENANCE_ROOM_TOOL))
            {
                throw new ConnectException(Descriptions.MAINTENANCE_ROOM_TOOL_NOT_PRESENT);
            }

            if (!this.universe.ActivePlayer.OwnsItem(Keys.SOCIALROOM_ANTENNA))
            {
                this.universe.PickObject(antenna);
            }

            this.universe.ActivePlayer.Items.Remove(antenna);
            droid.FirstLookDescription = string.Empty;
            antenna.LinkedToDescription = Descriptions.DROID_ANTENNA_LINKDESCRIPTION;

            PrintingSubsystem.Resource(Descriptions.DROID_ANTENNA_MOUNTED);

            ChangeShipModelContainment();

            antenna.Connect -= MountAntennaToDroid;

            this.scoreBoard.WinScore(nameof(MountAntennaToDroid));
            this.SolveRobotQuest();
            
            return;
        }

        throw new ConnectException(BaseDescriptions.DOES_NOT_WORK);
    }

    private void ChangeShipModelContainment()
    {
        if (this.objectHandler.GetObjectFromWorldByKey(Keys.ENGINE_ROOM_SHIP_MODEL) is Item shipModel)
        {
            if (shipModel.ContainmentDescription == Descriptions.ENGINE_ROOM_SHIP_MODEL_CONTAINMENT)
            {
                shipModel.ContainmentDescription = Descriptions.ENGINE_ROOM_SHIP_MODEL_CONTAINMENT_II;
            }
            else if (shipModel.ContainmentDescription == Descriptions.ENGINE_ROOM_SHIP_MODEL_CONTAINMENT_II)
            {
                shipModel.ContainmentDescription = string.Empty;
            }
        }
    }

    private void SolveRobotQuest()
    {
        var engineRoom = this.objectHandler.GetLocationByKey(Keys.ENGINE_ROOM);
        if (engineRoom != default)
        {
            if (!this.universe.IsQuestSolved(MetaData.QUEST_VIII))
            {
                var item = engineRoom.GetItem(Keys.ENGINE_ROOM_RED_DOTS);
                if (item != default)
                {
                    item.Description = Descriptions.ENGINE_ROOM_RED_DOTS_NO_ROBOT;
                }
            }
            else
            {
                PrintingSubsystem.Resource(Descriptions.ROBOT_FIXING_WALL);

                var droid = this.universe.ActiveLocation.Items.SingleOrDefault(x => x.Key == Keys.DROID);
                if (droid != default)
                {
                    this.universe.ActiveLocation.Items.Remove(droid);
                }
                
                var item = engineRoom.GetItem(Keys.ENGINE_ROOM_RED_DOTS);
                if (item != default)
                {
                    engineRoom.RemoveItem(item);    
                }
            }
        }

        var corridor = this.objectHandler.GetLocationByKey(Keys.CORRIDOR_WEST);
        if (corridor != default)
        {
            corridor.IsLocked = false;
            corridor.LockDescription = string.Empty;
        }

        this.universe.SolveQuest(MetaData.QUEST_VII);
        
        this.InitiateFinalStep();
    }
    
    private void SolveEnergyQuest()
    {
        var engineRoom = this.objectHandler.GetLocationByKey(Keys.ENGINE_ROOM);

        if (engineRoom != default)
        {
            if (!this.universe.IsQuestSolved(MetaData.QUEST_VII))
            {
                var item = engineRoom.GetItem(Keys.ENGINE_ROOM_RED_DOTS);
                if (item != default)
                {
                    item.Description = Descriptions.ENGINE_ROOM_RED_DOTS_NO_SOLAR;
                }
            }
            else
            {
                PrintingSubsystem.Resource(Descriptions.ROBOT_FIXING_WALL);

                if (this.objectHandler.GetLocationByKey(Keys.ROOF_TOP) is {} roof)
                {
                    if (roof.Items.SingleOrDefault(x => x.Key == Keys.DROID) is {} droid)
                    {
                        roof.Items.Remove(droid);
                    }

                    if (engineRoom.GetItem(Keys.ENGINE_ROOM_RED_DOTS) is {} item)
                    {
                        engineRoom.RemoveItem(item);
                    }
                }
            }
            
        }
        
        this.universe.SolveQuest(MetaData.QUEST_VIII);

        this.InitiateFinalStep();
    }

    private void InitiateFinalStep()
    {
        if (this.universe.IsQuestSolved(MetaData.QUEST_VII) && this.universe.IsQuestSolved(MetaData.QUEST_VIII))
        {
            PrintingSubsystem.ForegroundColor = TextColor.Magenta;
            PrintingSubsystem.Resource(Descriptions.RETURN_TO_BRIDGE);
            PrintingSubsystem.ResetColors();

            this.universe.ActivePlayer.AfterSitDown -= this.AfterSitDown;
            this.universe.ActivePlayer.AfterSitDown += this.AfterSitDownOnQuestsSolved;
        }
    }

    internal void SetPlayersName(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Player)
        {
            this.universe.ActivePlayer.Name = eventArgs.ExternalItemKey;
            this.universe.ActivePlayer.IsStranger = false;
            PrintingSubsystem.ActivePlayer(this.universe.ActivePlayer);
        }
    }
    
    internal void ConnectOxygenBottleWithHelmet(object? sender, ConnectEventArgs eventArgs)
    {
        if (sender is Item helmet && helmet.Key == Keys.HELMET && eventArgs.ItemToUse is Item oxygenBottle && oxygenBottle.Key == Keys.OXYGEN_BOTTLE)
        {
            if (!this.universe.ActivePlayer.OwnsItem(Keys.OXYGEN_BOTTLE))
            {
                this.universe.PickObject(oxygenBottle);
            }
                
            if (!this.universe.ActivePlayer.OwnsItem(Keys.HELMET))
            {
                this.universe.PickObject(helmet);
            }
            
            this.universe.ActivePlayer.Items.Remove(oxygenBottle);

            helmet.FirstLookDescription = string.Empty;

            if (this.universe.ActivePlayer.Clothes.Contains(helmet))
            {
                PrintingSubsystem.FormattedResource(Descriptions.LINK_OXYGEN_BOTTLE_TO_HELMET_WITH_HELMET_ON,
                    Descriptions.LINK_OXYGEN_BOTTLE_TO_HELMET);
            }
            else
            {
                PrintingSubsystem.Resource(Descriptions.LINK_OXYGEN_BOTTLE_TO_HELMET);
            }

            helmet.Connect -= ConnectOxygenBottleWithHelmet;

            this.scoreBoard.WinScore(nameof(ConnectOxygenBottleWithHelmet));
            this.universe.SolveQuest(MetaData.QUEST_V);
        }
        else
        {
            throw new ConnectException(BaseDescriptions.DOES_NOT_WORK);    
        }
    }

    internal void UseDumbbellBarWithLever(object? sender, UseItemEventArgs eventArgs)
    {
        if (eventArgs.ItemToUse == default)
        {
            throw new UseException(BaseDescriptions.WHAT_TO_USE);
        }
        
        if (sender is Item itemOne && eventArgs.ItemToUse is Item itemTwo && itemOne.Key != itemTwo.Key)
        {
            var itemList = new List<Item> { itemOne, itemTwo };
            var dumbbellBar = itemList.SingleOrDefault(i => i.Key == Keys.DUMBBELL_BAR);
            var lever = itemList.SingleOrDefault(i => i.Key == Keys.PANEL_TOP_LEVER);
            
            if (dumbbellBar != default && lever != default)
            {
                if (this.universe.ActivePlayer.GetUnhiddenItem(Keys.DUMBBELL_BAR) == default)
                {
                    this.universe.PickObject(dumbbellBar);
                }
                
                PrintingSubsystem.Resource(Descriptions.PANEL_TOP_LEVER_DUMBBELL_BAR);

                ChangeShipModelContainment();
                    
                dumbbellBar.Use -= UseDumbbellBarWithLever;
                lever.Use -= UseDumbbellBarWithLever;

                var droid = this.objectHandler.GetObjectFromWorldByKey(Keys.DROID);
                if (droid != default)
                {
                    droid.ContainmentDescription = Descriptions.DROID_ENERGY_CONTAINMENT;
                    if (!string.IsNullOrEmpty(droid.FirstLookDescription))
                    {
                        droid.FirstLookDescription = Descriptions.DROID_FIRSTLOOK_II;
                    }
                }

                this.scoreBoard.WinScore(nameof(UseDumbbellBarWithLever));
                this.SolveEnergyQuest();
            }
            else
            {
                throw new UseException(BaseDescriptions.DOES_NOT_WORK);
            }
        }
    }

    internal void ConnectAirlockRopeWithBelt(object? sender, ConnectEventArgs eventArgs)
    {
        if (sender is Item rope && rope.Key == Keys.AIRLOCK_ROPE)
        {
            if (eventArgs.ItemToUse is Item fixPoint && Keys.BELT == fixPoint.Key)
            {
                var itemName = ArticleHandler.GetNameWithArticleForObject(fixPoint, GrammarCase.Accusative);
                
                if (!this.universe.ActivePlayer.OwnsObject(fixPoint))
                {
                    throw new ConnectException(string.Format(BaseDescriptions.ITEM_NOT_OWNED_FORMATTED, itemName,
                        true));
                }

                if (!this.universe.ActivePlayer.WearsItem(fixPoint))
                {
                    throw new ConnectException(string.Format(BaseDescriptions.ITEM_NOT_WEARN, itemName,
                        true));
                }
                
                PrintingSubsystem.Resource(Descriptions.LINK_ROPE_TO_EYELET);
                rope.Connect -= ConnectAirlockRopeWithBelt;

                this.scoreBoard.WinScore(nameof(ConnectAirlockRopeWithBelt));
                
                return;
            }
        }
        
        throw new ConnectException(BaseDescriptions.DOES_NOT_WORK);
    }

    internal void BreakEquipmentBoxLock(object? sender, BreakItemEventArgs eventArgs)
    {
        if (eventArgs.ItemToUse == default)
        {
            throw new BreakException(BaseDescriptions.IMPOSSIBLE_BREAK_ITEM);
        }

        if (sender is Item boxLock && boxLock.Key == Keys.EQUIPMENT_BOX_LOCK)
        {
            var box = this.universe.ActiveLocation.GetItem(Keys.EQUIPMENT_BOX);
            if (box != default)
            {
                if (eventArgs.ItemToUse.Key != Keys.DUMBBELL_BAR)
                {
                    throw new BreakException(BaseDescriptions.WRONG_BREAK_ITEM);
                }

                box.IsLocked = false;
                box.LinkedTo.Remove(boxLock);
                PrintingSubsystem.Resource(Descriptions.BREAK_EQUIPMENT_BOX_LOCK);
                
                this.universe.ActiveLocation.Items.Add(this.GetDestroyedBoxLock());
                
                this.scoreBoard.WinScore(nameof(BreakEquipmentBoxLock));
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
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        
        return boxLock;
    }
    
    internal void BreakFlapWithDumbbellBar(object? sender, BreakItemEventArgs eventArgs)
    {
        if (eventArgs.ItemToUse == default)
        {
            throw new BreakException(BaseDescriptions.IMPOSSIBLE_BREAK_ITEM);
        }
        
        if (sender is Item flap)
        {
            if (flap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP && eventArgs.ItemToUse.Key == Keys.DUMBBELL_BAR)
            {
                throw new BreakException(Descriptions.AMBULANCE_RESPIRATOR_FLAP_DUMBBELL_BAR_UNBREAKABLE);    
            }

            var flapName = ArticleHandler.GetNameWithArticleForObject(flap, GrammarCase.Dative, lowerFirstCharacter: true);
            throw new BreakException(String.Format(BaseDescriptions.INAPPROPRIATE_TOOL, flapName));    
        }
    }
    
    internal void OpenEquipmentBox(object? sender, ContainerObjectEventArgs eventArg)
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

    internal void OpenFridge(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item fridge && fridge.Key == Keys.FRIDGE)
        {
            if (fridge.IsClosed)
            {
                var handle = fridge.GetItem(Keys.FRIDGE_DOOR_HANDLE);
                if (handle != default)
                {
                    handle.IsHidden = false;
                    PrintingSubsystem.Resource(Descriptions.FRIDGE_DOOR_HANDLE_OPEN);
                    fridge.BeforeOpen -= OpenFridge;
                }
            }
        }
    }
    
    internal void OpenFlap(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item flap && flap.Key == Keys.AMBULANCE_RESPIRATOR_FLAP)
        {
            if (flap.IsClosed)
            {
                var oxygen = this.universe.ActiveLocation.GetItem(Keys.OXYGEN_BOTTLE);
                if (oxygen != default)
                {
                    oxygen.IsHidden = false;
                    PrintingSubsystem.Resource(Descriptions.OXYGEN_BOTTLE_FOUND);
                }
            }
        }
    }
    
    internal void TakeDumbbellBar(object? sender, ContainerObjectEventArgs eventArg)
    {
        if (sender is Item bar && bar.Key == Keys.DUMBBELL_BAR)
        {
            this.scoreBoard.WinScore(nameof(TakeDumbbellBar));
            bar.AfterTake -= TakeDumbbellBar;
        }
    }
    
    internal void TakeTool(object? sender, ContainerObjectEventArgs eventArg)
    {
        if (sender is Item tool && tool.Key == Keys.MAINTENANCE_ROOM_TOOL)
        {
            this.scoreBoard.WinScore(nameof(TakeTool));
            tool.AfterTake -= TakeTool;
        }
    }
    
    internal void WriteTextToComputerTerminal(object? sender, WriteEventArgs eventArgs)
    {
        if (sender is Location terminal && terminal.Key == Keys.COMPUTER_TERMINAL)
        {
            if (!this.isAccessGranted)
            {
                if (IsPasswordCorrect(eventArgs.Text))
                {
                    PrintingSubsystem.Resource(Descriptions.TERMINAL_PASSWORD_SUCCESS);
                    PrintPasswordMessage(Descriptions.COMPUTER_TERMINAL_DISPLAY_REPORT);
                    if (this.GetRoom(Keys.MACHINE_CORRIDOR_MID) is {} room)
                    {
                        room.IsLocked = false;
                    }
                    this.isAccessGranted = true;
                    
                    this.scoreBoard.WinScore(nameof(WriteTextToComputerTerminal));
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

    internal void DropClothsWithOpenAirlock(object? sender, ContainerObjectEventArgs eventArgs)
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
            throw new DropException(Descriptions.AIRLOCK_OPEN_DROP_CLOTHES);
        }
    }
    
    internal void EnterAirlock(object? sender, EnterLocationEventArgs eventArgs)
    {
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK)
        {
            ResetItemWeight(this.universe.ActivePlayer.Items);
            ResetItemWeight(this.universe.ActivePlayer.Clothes);
            PrintingSubsystem.Resource(Descriptions.ZERO_GRAVITY);
        }
    }

    internal void TryToPickupPortrait(object? sender, ContainerObjectEventArgs eventArgs)
    {
        if (sender is Item portrait && portrait.Key == Keys.PORTRAIT)
        {
            throw new TakeException(Descriptions.PORTRAIT_TO_HEAVY);
        }
    }
    
    internal void LeaveAirlock(object? sender, LeaveLocationEventArgs eventArgs)
    {
        if (sender is Location airlock && airlock.Key == Keys.AIRLOCK 
                                       && eventArgs.NewDestinationNode?.Location?.Key == Keys.MACHINE_CORRIDOR_MID 
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
                PrintingSubsystem.ForegroundColor = TextColor.Magenta;
                PrintingSubsystem.Resource(Descriptions.GRAVITY_ITEM_DROP);
                PrintingSubsystem.ResetColors();
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

    private Location? GetRoom(string locationKey)
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