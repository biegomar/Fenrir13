using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.SocialRoom;

public class SocialRoomPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var socialRoom = new Location()
        {
            Key = Keys.SOCIALROOM,
            Name = Locations.SOCIALROOM,
            Description = Descriptions.SOCIALROOM
        };

        socialRoom.Items.Add(GetCouch(eventProvider));
        socialRoom.Items.Add(GetAntennaConstruction(eventProvider));
        
        AddSurroundings(socialRoom);

        return socialRoom;
    }

    private static void AddSurroundings(Location socialRoom)
    {
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_CEILING, () => Descriptions.SOCIALROOM_CEILING);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_MONITOR, () => Descriptions.SOCIALROOM_MONITOR);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_SEAT, () => Descriptions.SOCIALROOM_SEAT);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_EAST_WALL, () => Descriptions.SOCIALROOM_EAST_WEST_WALL);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_WEST_WALL, () => Descriptions.SOCIALROOM_EAST_WEST_WALL);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_SEATGROUP, () => Descriptions.SOCIALROOM_SEATGROUP);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_BOOKS, () => string.Format(Descriptions.SOCIALROOM_BOOKS, GetBookTitle()));
    }

    private static string GetBookTitle()
    {
        var bookList = new List<string>
        {
            Descriptions.SOCIALROOM_BOOK_I, Descriptions.SOCIALROOM_BOOK_II, Descriptions.SOCIALROOM_BOOK_III,
            Descriptions.SOCIALROOM_BOOK_IV, Descriptions.SOCIALROOM_BOOK_V,
            Descriptions.SOCIALROOM_BOOK_VI, Descriptions.SOCIALROOM_BOOK_VII, Descriptions.SOCIALROOM_BOOK_VIII,
            Descriptions.SOCIALROOM_BOOK_IX, Descriptions.SOCIALROOM_BOOK_X
        }; 
        
        var rnd = new Random();
        
        return bookList[rnd.Next(0, bookList.Count)];
    }

    private static Item GetCouch(EventProvider eventProvider)
    {
        var couch = new Item()
        {
            Key = Keys.SOCIALROOM_COUCH,
            Name = Items.SOCIALROOM_COUCH,
            Description = Descriptions.SOCIALROOM_COUCH,
            IsClimbAble = true,
            IsPickAble = false,
        };

        return couch;
    }
    
    private static Item GetAntennaConstruction(EventProvider eventProvider)
    {
        var antennaConstruction = new Item()
        {
            Key = Keys.SOCIALROOM_ANTENNA_CONSTRUCTION,
            Name = Items.SOCIALROOM_ANTENNA_CONSTRUCTION,
            Description = Descriptions.SOCIALROOM_ANTENNA_CONSTRUCTION,
            ContainmentDescription = Descriptions.SOCIALROOM_ANTENNA_CONSTRUCTION_CONTAINMENT,
            IsPickAble = false,
        };
        
        antennaConstruction.LinkedTo.Add(GetAntenna(eventProvider));

        return antennaConstruction;
    }
    
    private static Item GetAntenna(EventProvider eventProvider)
    {
        var antenna = new Item()
        {
            Key = Keys.SOCIALROOM_ANTENNA,
            Name = Items.SOCIALROOM_ANTENNA,
            Description = Descriptions.SOCIALROOM_ANTENNA,
            LinkedToDescription = Descriptions.SOCIALROOM_ANTENNA_LINKEDTODESCRIPTION,
            IsHidden = true
        };
        
        AddBeforeTakeEvents(antenna, eventProvider);

        return antenna;
    }
    
    private static void AddBeforeTakeEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeTake += eventProvider.BeforeTakeAntenna;
    }
}