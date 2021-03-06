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
            ContainmentDescription = Descriptions.AIRLOCK_KEYPAD_CONTAINMENT,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
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
            IsHidden = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        
        AddPushGreenButtonEvents(greenButton, eventProvider);

        return greenButton;
    }
    
    private static Item GetRedButton(EventProvider eventProvider)
    {
        var redButton = new Item
        {
            Key = Keys.AIRLOCK_KEYPAD_RED_BUTTON,
            Name = Items.AIRLOCK_KEYPAD_RED_BUTTON,
            Description = Descriptions.AIRLOCK_KEYPAD_RED_BUTTON,
            IsHidden = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };

        AddPushRedButtonEvents(redButton, eventProvider);
        
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
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        
        AddUseEvents(airlockRope, eventProvider);

        return airlockRope;
    }
    
    
    private static void AddSurroundings(Location location)
    {
        var ceiling = new Item()
        {
            Key = Keys.CEILING,
            Name = Items.CEILING,
            Description = Descriptions.CEILING,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(ceiling);
        
        var wall = new Item()
        {
            Key = Keys.CHAMBER_WALL,
            Name = Items.CHAMBER_WALL,
            Description = Descriptions.CHAMBER_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(wall);
        
        var door = new Item()
        {
            Key = Keys.ROOM_DOOR,
            Description = Descriptions.ROOM_DOOR,
            Name = Items.ROOM_DOOR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(door);
        
        var northWall = new Item()
        {
            Key = Keys.AIRLOCK_NORTHERN_WALL,
            Name = Items.AIRLOCK_NORTHERN_WALL,
            Description = Descriptions.AIRLOCK_NORTHERN_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(northWall);
        
        var southWall = new Item()
        {
            Key = Keys.AIRLOCK_SOUTHERN_WALL,
            Name = Items.AIRLOCK_SOUTHERN_WALL,
            Description = Descriptions.AIRLOCK_SOUTHERN_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(southWall);
        
        var bulkHead = new Item()
        {
            Key = Keys.AIRLOCK_BULKHEAD,
            Name = Items.AIRLOCK_BULKHEAD,
            Description = Descriptions.AIRLOCK_BULKHEAD,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(bulkHead);
    }
    
    private static void AddChangeRoomEvents(Location airlock, EventProvider eventProvider)
    {
        airlock.AfterChangeLocation += eventProvider.EnterAirlock;
        airlock.BeforeChangeLocation += eventProvider.CantLeaveWithOpenBulkHeadOrTiedRope;
        airlock.BeforeChangeLocation += eventProvider.CantLeaveWithoutOpenBulkHead;
        airlock.BeforeChangeLocation += eventProvider.LeaveAirlock;
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseAirlockRopeWithEyeletOrBelt;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.UseAirlockRopeWithEyeletOrBelt), 1);
    }
    
    private static void AddPushRedButtonEvents(Item item, EventProvider eventProvider)
    {
        item.Push += eventProvider.PushRedButton;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.PushRedButton), 5);
    }
    
    private static void AddPushGreenButtonEvents(Item item, EventProvider eventProvider)
    {
        item.Push += eventProvider.PushGreenButton;
    }
}