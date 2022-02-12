using System.Runtime.CompilerServices;
using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Airlock;

internal class AirlockPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var airlock = new Location()
        {
            Key = Keys.AIRLOCK,
            Name = Locations.AIRLOCK,
            Description = Descriptions.AIRLOCK
        };
        
        airlock.Items.Add(GetCableWinch(eventProvider));
        airlock.Items.Add(GetKeyPad(eventProvider));

        AddSurroundings(airlock);
        
        AddChangeRoomEvents(airlock, eventProvider);
        
        return airlock;
    }

    private static Item GetKeyPad(EventProvider eventProvider)
    {
        var keyPad = new Item
        {
            Key = Keys.AIRLOCK_KEYPAD,
            Name = Items.AIRLOCK_KEYPAD,
            Description = Descriptions.AIRLOCK_KEYPAD,
            ContainmentDescription = Descriptions.AIRLOCK_KEYPAD_CONTAINMENT
            
        };
        
        keyPad.Items.Add(GetGreenButton(eventProvider));
        keyPad.Items.Add(GetRedButton(eventProvider));

        return keyPad;
    }
    
    private static Item GetGreenButton(EventProvider eventProvider)
    {
        var greenButton = new Item
        {
            Key = Keys.AIRLOCK_KEYPAD_GREEN_BUTTON,
            Name = Items.AIRLOCK_KEYPAD_GREEN_BUTTON,
            Description = Descriptions.AIRLOCK_KEYPAD_GREEN_BUTTON,
            IsHidden = true
        };

        return greenButton;
    }
    
    private static Item GetRedButton(EventProvider eventProvider)
    {
        var redButton = new Item
        {
            Key = Keys.AIRLOCK_KEYPAD_RED_BUTTON,
            Name = Items.AIRLOCK_KEYPAD_RED_BUTTON,
            Description = Descriptions.AIRLOCK_KEYPAD_RED_BUTTON,
            IsHidden = true
        };

        AddPushEvents(redButton, eventProvider);
        
        return redButton;
    }
    
    private static Item GetCableWinch(EventProvider eventProvider)
    {
        var cableWinch = new Item
        {
            Key = Keys.AIRLOCK_CABLE_WINCH,
            Name = Items.AIRLOCK_CABLE_WINCH,
            Description = Descriptions.AIRLOCK_CABLE_WINCH,
            ContainmentDescription = Descriptions.AIRLOCK_CABLE_WINCH_CONTAINMENT,
            IsPickAble = false
        };

        cableWinch.Items.Add(GetAirlockRope(eventProvider));
        return cableWinch;
    }
    
    private static Item GetAirlockRope(EventProvider eventProvider)
    {
        var airlockRope = new Item
        {
            Key = Keys.AIRLOCK_ROPE,
            Name = Items.AIRLOCK_ROPE,
            Description = Descriptions.AIRLOCK_ROPE,
            ContainmentDescription = Descriptions.AIRLOCK_ROPE_CONTAINMENT,
            LinkedToDescription = Descriptions.AIRLOCK_ROPE_LINKEDTO,
            IsPickAble = false
        };
        
        AddUseEvents(airlockRope, eventProvider);

        return airlockRope;
    }
    
    
    private static void AddSurroundings(Location airlock)
    {
        //airlock.Surroundings.Add(Keys.AIRLOCK_KEYPAD, Descriptions.AIRLOCK_KEYPAD);
    }
    
    private static void AddChangeRoomEvents(Location airlock, EventProvider eventProvider)
    {
        airlock.AfterChangeLocation += eventProvider.EnterAirlock;
        airlock.BeforeChangeLocation += eventProvider.LeaveAirlock;
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseAirlockRopeWithBelt;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.UseAirlockRopeWithBelt), 1);
    }
    
    private static void AddPushEvents(Item item, EventProvider eventProvider)
    {
        item.Push += eventProvider.PushRedButton;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.PushRedButton), 5);
    }}