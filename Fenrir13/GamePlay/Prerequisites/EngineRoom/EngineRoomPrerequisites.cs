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
            Description = Descriptions.ENGINE_ROOM
        };

        AddSurroundings(engine);
        
        return engine;
    }
    
    private static void AddSurroundings(Location engine)
    {
        engine.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
    }
}