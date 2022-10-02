using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.EquipmentRoom;

internal static class EquipmentRoomPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var equipmentRoom = new Location()
        {
            Key = Keys.EQUIPMENT_ROOM,
            Name = Locations.EQUIPMENT_ROOM,
            Description = Descriptions.EQUIPMENT_ROOM,
            Grammar = new Grammars(Genders.Male)
        };
        
        equipmentRoom.Items.Add(GetBox(eventProvider));

        AddSurroundings(equipmentRoom);

        return equipmentRoom;
    }
    
    private static void AddSurroundings(Location location)
    {
        var ceiling = new Item()
        {
            Key = Keys.CEILING,
            Name = Items.CEILING,
            Description = Descriptions.CEILING,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars()
        };
        location.Items.Add(ceiling);
        
        var wall = new Item()
        {
            Key = Keys.CHAMBER_WALL,
            Name = Items.CHAMBER_WALL,
            Description = Descriptions.CHAMBER_WALL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars()
        };
        location.Items.Add(wall);
        
        var bench = new Item()
        {
            Key = Keys.EQUIPMENT_ROOM_BENCH,
            Name = Items.EQUIPMENT_ROOM_BENCH,
            Description = Descriptions.EQUIPMENT_ROOM_BENCH,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(bench);
        
        var niche = new Item()
        {
            Key = Keys.EQUIPMENT_ROOM_NICHE,
            Name = Items.EQUIPMENT_ROOM_NICHE,
            Description = Descriptions.EQUIPMENT_ROOM_NICHE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(niche);
        
        var trashBin = new Item()
        {
            Key = Keys.EQUIPMENT_ROOM_TRASHBIN,
            Name = Items.EQUIPMENT_ROOM_TRASHBIN,
            Description = Descriptions.EQUIPMENT_ROOM_TRASHBIN,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(gender: Genders.Male)
        };
        location.Items.Add(trashBin);
        
        var cloth = new Item()
        {
            Key = Keys.EQUIPMENT_ROOM_CLOTH,
            Name = Items.EQUIPMENT_ROOM_CLOTH,
            Description = Descriptions.EQUIPMENT_ROOM_CLOTH,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(cloth);
        
        var eyelet = new Item()
        {
            Key = Keys.EYELET,
            Name = Items.EYELET,
            Description = Descriptions.EYELET,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars()
        };
        location.Items.Add(eyelet);
        
        var roomDoor = new Item()
        {
            Key = Keys.ROOM_DOOR,
            Name = Items.ROOM_DOOR,
            Description = Descriptions.ROOM_DOOR,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars()
        };
        location.Items.Add(roomDoor);
    }

    private static Item GetBox(EventProvider eventProvider)
    {
        var box = new Item()
        {
            Key = Keys.EQUIPMENT_BOX,
            Name = Items.EQUIPMENT_BOX,
            Description = Descriptions.EQUIPMENT_BOX,
            FirstLookDescription = Descriptions.EQUIPMENT_BOX_FIRSTLOOK,
            ContainmentDescription = Descriptions.EQUIPMENT_BOX_CONTAINMENT,
            CloseDescription = Descriptions.EQUIPMENT_BOX_CLOSED,
            IsClosed = true,
            IsLocked = true,
            IsCloseable = true,
            IsPickable = false
        };
        
        box.LinkedTo.Add(GetBoxLock(eventProvider));
        box.Items.Add(GetHelmet(eventProvider));
        box.Items.Add(GetGloves(eventProvider));
        box.Items.Add(GetBoots(eventProvider));
        box.Items.Add(GetBelt(eventProvider));
        
        AddOpenEvents(box, eventProvider);

        return box;
    }
    
    private static Item GetBoxLock(EventProvider eventProvider)
    {
        var boxLock = new Item()
        {
            Key = Keys.EQUIPMENT_BOX_LOCK,
            Name = Items.EQUIPMENT_BOX_LOCK,
            Description = Descriptions.EQUIPMENT_BOX_LOCK,
            LinkedToDescription = Descriptions.EQUIPMENT_BOX_LOCK_CONTAINMENT,
            IsPickable = false,
            IsHidden = true,
            IsBreakable = true,
            Grammar = new Grammars(Genders.Neutrum)
        };
        
        AddBreakEvents(boxLock, eventProvider);

        return boxLock;
    }

    private static Item GetHelmet(EventProvider eventProvider)
    {
        var helmet = new Item()
        {
            Key = Keys.HELMET,
            Name = Items.HELMET,
            Description = Descriptions.HELMET,
            FirstLookDescription = Descriptions.HELMET_FIRSTLOOK,
            IsHidden = true,
            IsWearable = true,
            Weight = ItemWeights.HELMET,
            Grammar = new Grammars(Genders.Male)
        };
        
        AddUseEvents(helmet, eventProvider);
        AddBeforeDropEvents(helmet, eventProvider);
        
        return helmet;
    }
    
    private static Item GetGloves(EventProvider eventProvider)
    {
        var gloves = new Item()
        {
            Key = Keys.GLOVES,
            Name = Items.GLOVES,
            Description = Descriptions.GLOVES,
            IsHidden = true,
            IsWearable = true,
            Weight = ItemWeights.GLOVES,
            Grammar = new Grammars(Genders.Male, false)
        };
        
        AddBeforeDropEvents(gloves, eventProvider);
        
        return gloves;
    }
    
    private static Item GetBoots(EventProvider eventProvider)
    {
        var boots = new Item()
        {
            Key = Keys.BOOTS,
            Name = Items.BOOTS,
            Description = Descriptions.BOOTS,
            IsHidden = true,
            IsWearable = true,
            Weight = ItemWeights.BOOTS,
            Grammar = new Grammars(Genders.Male, false)
        };
        
        AddBeforeDropEvents(boots, eventProvider);
        
        return boots;
    }
    
    private static Item GetBelt(EventProvider eventProvider)
    {
        var belt = new Item()
        {
            Key = Keys.BELT,
            Name = Items.BELT,
            Description = Descriptions.BELT,
            IsHidden = true,
            IsWearable = true,
            Weight = ItemWeights.BELT,
            Grammar = new Grammars(Genders.Male)
        };
        
        belt.Items.Add(GetEyelet(eventProvider));
        
        AddEyeletUseEvents(belt, eventProvider);
        AddBeforeDropEvents(belt, eventProvider);

        return belt;
    }
    
    private static Item GetEyelet(EventProvider eventProvider)
    {
        var eyelet = new Item()
        {
            Key = Keys.EYELET,
            Name = Items.EYELET,
            Description = Descriptions.EYELET,
            ContainmentDescription = Descriptions.EYELET_CONTAINMENT,
            LinkedToDescription = Descriptions.EYELET_LINKEDTO,
            IsHidden = true,
            IsPickable = false
        };

        AddEyeletUseEvents(eyelet, eventProvider);
        
        return eyelet;
    }
    

    private static void AddBreakEvents(Item box, EventProvider eventProvider)
    {
        box.Break += eventProvider.BreakEquipmentBoxLock;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.BreakEquipmentBoxLock), 5);
    }
    
    private static void AddOpenEvents(Item box, EventProvider eventProvider)
    {
        box.AfterOpen += eventProvider.OpenEquipmentBox;
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseOxygenBottleWithHelmet;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.UseOxygenBottleWithHelmet), 5);
    }
    
    private static void AddEyeletUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseAirlockRopeWithEyeletOrBelt;
    }
    
    private static void AddBeforeDropEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeDrop += eventProvider.DropClothsWithOpenAirlock;
    }
}