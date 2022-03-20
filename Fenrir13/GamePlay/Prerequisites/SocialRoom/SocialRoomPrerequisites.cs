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
            Description = Descriptions.SOCIALROOM,
            Grammar = new Grammars(Genders.Male)
        };

        socialRoom.Items.Add(GetCouch(eventProvider));
        socialRoom.Items.Add(GetAntennaConstruction(eventProvider));
        
        AddSurroundings(socialRoom);
        
        AddSitDownEvents(socialRoom, eventProvider);

        return socialRoom;
    }

    private static void AddSurroundings(Location socialRoom)
    {
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_CEILING, () => Descriptions.SOCIALROOM_CEILING);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_MONITOR, () => Descriptions.SOCIALROOM_MONITOR);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_SEAT, () => Descriptions.SOCIALROOM_SEAT);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_EAST_WALL, () => Descriptions.SOCIALROOM_EAST_WEST_WALL);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_WEST_WALL, () => Descriptions.SOCIALROOM_EAST_WEST_WALL);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_SOUTH_WALL, () => Descriptions.SOCIALROOM_SOUTH_WALL);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_NORTH_WALL, () => Descriptions.SOCIALROOM_NORTH_WALL);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_WALL, () => Descriptions.SOCIALROOM_WALL);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_SEATGROUP, () => Descriptions.SOCIALROOM_SEATGROUP);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_BILLARD, () => Descriptions.SOCIALROOM_BILLARD);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_BILLARD_CUE, () => Descriptions.SOCIALROOM_BILLARD_CUE);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_BILLARD_BALLS, () => Descriptions.SOCIALROOM_BILLARD_BALLS);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_BILLARD_BRUSH, () => Descriptions.SOCIALROOM_BILLARD_BRUSH);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_BILLARD_CHALK, () => Descriptions.SOCIALROOM_BILLARD_CHALK);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_BILLARD_THINGS, () => Descriptions.SOCIALROOM_BILLARD_THINGS);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_BILLARD_TRIANGLE, () => Descriptions.SOCIALROOM_BILLARD_TRIANGLE);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_DARTS, () => Descriptions.SOCIALROOM_DARTS);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_GLASS_TABLE, () => Descriptions.SOCIALROOM_GLASS_TABLE);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_MEDIASERVER, () => Descriptions.SOCIALROOM_MEDIASERVER);
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
            IsSeatAble = true,
            IsPickAble = false
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
            Grammar = new Grammars(Genders.Male)
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
            IsHidden = true,
            Weight = ItemWeights.SOCIALROOM_ANTENNA
        };
        
        AddBeforeTakeEvents(antenna, eventProvider);
        AddUseEvents(antenna, eventProvider);

        return antenna;
    }
    
    private static void AddBeforeTakeEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeTake += eventProvider.BeforeTakeAntenna;
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseToolWithAntennaInSocialRoom;
        item.Use += eventProvider.MountAntennaToDroid;
    }
    
    private static void AddSitDownEvents(Location room, EventProvider eventProvider)
    {
        room.SitDown += eventProvider.SitDownOnCouchInSocialRoom;
    }
}