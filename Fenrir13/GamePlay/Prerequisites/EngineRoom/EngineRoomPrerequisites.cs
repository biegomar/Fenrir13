using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.EngineRoom;

internal class EngineRoomPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var engine = new Location()
        {
            Key = Keys.ENGINE_ROOM,
            Name = Locations.ENGINE_ROOM,
            Description = Descriptions.ENGINE_ROOM,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };

        engine.Items.Add(GetShipModel(eventProvider));
        
        AddAfterLookEvents(engine, eventProvider);
        
        AddSurroundings(engine);
        
        return engine;
    }

    private static Item GetShipModel(EventProvider eventProvider)
    {
        var shipModel = new Item
        {
            Key = Keys.ENGINE_ROOM_SHIP_MODEL, 
            Name = Items.ENGINE_ROOM_SHIP_MODEL, 
            Description = Descriptions.ENGINE_ROOM_SHIP_MODEL,
            ContainmentDescription = Descriptions.ENGINE_ROOM_SHIP_MODEL_CONTAINMENT,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        
        AddATurnEvents(shipModel, eventProvider);

        return shipModel;
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
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(ceiling);
        
        var wall = new Item()
        {
            Key = Keys.CHAMBER_WALL,
            Name = Items.CHAMBER_WALL,
            Description = Descriptions.CHAMBER_WALL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(wall);
        
        var redDots = new Item()
        {
            Key = Keys.ENGINE_ROOM_RED_DOTS,
            Name = Items.ENGINE_ROOM_RED_DOTS,
            Description = Descriptions.ENGINE_ROOM_RED_DOTS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Male, isSingular: false)
        };
        location.Items.Add(redDots);
        
        var art = new Item()
        {
            Key = Keys.ENGINE_ROOM_ART,
            Name = Items.ENGINE_ROOM_ART,
            Description = Descriptions.ENGINE_ROOM_ART,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(art);
        
        var technic = new Item()
        {
            Key = Keys.ENGINE_ROOM_TECHNIC,
            Name = Items.ENGINE_ROOM_TECHNIC,
            Description = Descriptions.ENGINE_ROOM_TECHNIC,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(technic);
        
        var stealing = new Item()
        {
            Key = Keys.ENGINE_ROOM_STEALING,
            Name = Items.ENGINE_ROOM_STEALING,
            Description = Descriptions.ENGINE_ROOM_STEALING,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(stealing);
        
        var head = new Item()
        {
            Key = Keys.ENGINE_ROOM_HEAD,
            Name = Items.ENGINE_ROOM_HEAD,
            Description = Descriptions.ENGINE_ROOM_HEAD,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(head);
        
        var tangent = new Item()
        {
            Key = Keys.ENGINE_ROOM_TANGENT,
            Name = Items.ENGINE_ROOM_TANGENT,
            Description = Descriptions.ENGINE_ROOM_TANGENT,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(tangent);
        
        var computer = new Item()
        {
            Key = Keys.ENGINE_ROOM_COMPUTER,
            Name = Items.ENGINE_ROOM_COMPUTER,
            Description = Descriptions.ENGINE_ROOM_COMPUTER,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Male)
        };
        location.Items.Add(computer);
        
        var cube = new Item()
        {
            Key = Keys.QUATUM_CUBE,
            Name = Items.QUATUM_CUBE,
            Description = Descriptions.QUATUM_CUBE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Male)
        };
        location.Items.Add(cube);
    }
    
    private static void AddAfterLookEvents(Location item, EventProvider eventProvider)
    {
        item.AfterLook += eventProvider.LookAtRedDots;
        eventProvider.RegisterScore(nameof(eventProvider.LookAtRedDots), 1);
    }
    
    private static void AddATurnEvents(Item hologram, EventProvider eventProvider)
    {
        hologram.Turn += eventProvider.TurnHologram;
    }
}