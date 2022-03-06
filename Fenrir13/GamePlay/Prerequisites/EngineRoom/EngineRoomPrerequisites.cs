using Fenrir13.Events;
using Fenrir13.Resources;
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
            Grammar = new Grammars(Genders.Male)
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
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };

        return shipModel;
    }
    
    private static void AddSurroundings(Location engine)
    {
        engine.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
        engine.Surroundings.Add(Keys.CHAMBER_WALL, () => Descriptions.CHAMBER_WALL);
        engine.Surroundings.Add(Keys.ENGINE_ROOM_RED_DOTS, () => Descriptions.ENGINE_ROOM_RED_DOTS);
        engine.Surroundings.Add(Keys.ENGINE_ROOM_ART, () => Descriptions.ENGINE_ROOM_ART);
        engine.Surroundings.Add(Keys.ENGINE_ROOM_TECHNIC, () => Descriptions.ENGINE_ROOM_TECHNIC);
        engine.Surroundings.Add(Keys.ENGINE_ROOM_STEALING, () => Descriptions.ENGINE_ROOM_STEALING);
        engine.Surroundings.Add(Keys.ENGINE_ROOM_HEAD, () => Descriptions.ENGINE_ROOM_HEAD);
        engine.Surroundings.Add(Keys.ENGINE_ROOM_TANGENT, () => Descriptions.ENGINE_ROOM_TANGENT);
        engine.Surroundings.Add(Keys.ENGINE_ROOM_COMPUTER, () => Descriptions.ENGINE_ROOM_COMPUTER);
    }
    
    private static void AddAfterLookEvents(Location item, EventProvider eventProvider)
    {
        item.AfterLook += eventProvider.LookAtRedDots;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.LookAtRedDots), 1);
    }
}