using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay;
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
        
        socialRoom.AddOptionalVerb(VerbKeys.CLIMB, OptionalVerbs.PUT, string.Empty);
        
        socialRoom.Items.Add(GetCouch(eventProvider));
        socialRoom.Items.Add(GetAntennaConstruction(eventProvider));
        
        AddSurroundings(socialRoom, eventProvider);

        return socialRoom;
    }

    private static void AddSurroundings(Location location, EventProvider eventProvider)
    {
        var ceiling = new Item()
        {
            Key = Keys.SOCIALROOM_CEILING,
            Name = Items.SOCIALROOM_CEILING,
            Description = Descriptions.SOCIALROOM_CEILING,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(ceiling);
        
        var monitor = new Item()
        {
            Key = Keys.SOCIALROOM_MONITOR,
            Name = Items.SOCIALROOM_MONITOR,
            Description = Descriptions.SOCIALROOM_MONITOR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(monitor);
        
        var seat = new Item()
        {
            Key = Keys.SOCIALROOM_SEAT,
            Name = Items.SOCIALROOM_SEAT,
            Description = Descriptions.SOCIALROOM_SEAT,
            IsSurrounding = true,
            IsPickAble = false,
            IsSeatAble = true,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(seat);
        AddSitDownEvents(seat, eventProvider);
        
        var eastWall = new Item()
        {
            Key = Keys.SOCIALROOM_EAST_WALL,
            Name = Items.SOCIALROOM_EAST_WALL,
            Description = Descriptions.SOCIALROOM_EAST_WEST_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(eastWall);
        
        var westWall = new Item()
        {
            Key = Keys.SOCIALROOM_WEST_WALL,
            Name = Items.SOCIALROOM_WEST_WALL,
            Description = Descriptions.SOCIALROOM_EAST_WEST_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(westWall);
        
        var southWall = new Item()
        {
            Key = Keys.SOCIALROOM_SOUTH_WALL,
            Name = Items.SOCIALROOM_SOUTH_WALL,
            Description = Descriptions.SOCIALROOM_SOUTH_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(southWall);
        
        var northWall = new Item()
        {
            Key = Keys.SOCIALROOM_NORTH_WALL,
            Name = Items.SOCIALROOM_NORTH_WALL,
            Description = Descriptions.SOCIALROOM_NORTH_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(northWall);
        
        var wall = new Item()
        {
            Key = Keys.SOCIALROOM_WALL,
            Name = Items.SOCIALROOM_WALL,
            Description = Descriptions.SOCIALROOM_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(wall);
        
        var seatGroup = new Item()
        {
            Key = Keys.SOCIALROOM_SEATGROUP,
            Name = Items.SOCIALROOM_SEATGROUP,
            Description = Descriptions.SOCIALROOM_SEATGROUP,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(seatGroup);

        var billardCue = new Item()
        {
            Key = Keys.SOCIALROOM_BILLARD_CUE,
            Name = Items.SOCIALROOM_BILLARD_CUE,
            Description = Descriptions.SOCIALROOM_BILLARD_CUE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(billardCue);
        
        var billardBalls = new Item()
        {
            Key = Keys.SOCIALROOM_BILLARD_BALLS,
            Name = Items.SOCIALROOM_BILLARD_BALLS,
            Description = Descriptions.SOCIALROOM_BILLARD_BALLS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(billardBalls);
        
        var billardBrush = new Item()
        {
            Key = Keys.SOCIALROOM_BILLARD_BRUSH,
            Name = Items.SOCIALROOM_BILLARD_BRUSH,
            Description = Descriptions.SOCIALROOM_BILLARD_BRUSH,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(billardBrush);
        
        var billardChalk = new Item()
        {
            Key = Keys.SOCIALROOM_BILLARD_CHALK,
            Name = Items.SOCIALROOM_BILLARD_CHALK,
            Description = Descriptions.SOCIALROOM_BILLARD_CHALK,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(billardChalk);
        
        var billardThings = new Item()
        {
            Key = Keys.SOCIALROOM_BILLARD_THINGS,
            Name = Items.SOCIALROOM_BILLARD_THINGS,
            Description = Descriptions.SOCIALROOM_BILLARD_THINGS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(billardThings);
        
        var billardTriangle = new Item()
        {
            Key = Keys.SOCIALROOM_BILLARD_TRIANGLE,
            Name = Items.SOCIALROOM_BILLARD_TRIANGLE,
            Description = Descriptions.SOCIALROOM_BILLARD_TRIANGLE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(billardTriangle);
        
        var billard = new Item()
        {
            Key = Keys.SOCIALROOM_BILLARD,
            Name = Items.SOCIALROOM_BILLARD,
            Description = Descriptions.SOCIALROOM_BILLARD,
            IsSurrounding = true,
            IsPickAble = false,
            IsSeatAble = true,
            IsClimbAble = true,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(billard);
        AddSitDownEvents(billard, eventProvider);
        AddClimbEvents(billard, eventProvider);
        
        var darts = new Item()
        {
            Key = Keys.SOCIALROOM_DARTS,
            Name = Items.SOCIALROOM_DARTS,
            Description = Descriptions.SOCIALROOM_DARTS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(darts);
        
        var glassTable = new Item()
        {
            Key = Keys.SOCIALROOM_GLASS_TABLE,
            Name = Items.SOCIALROOM_GLASS_TABLE,
            Description = Descriptions.SOCIALROOM_GLASS_TABLE,
            IsSurrounding = true,
            IsPickAble = false,
            IsSeatAble = true,
            IsClimbAble = true,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(glassTable);
        AddSitDownEvents(glassTable, eventProvider);
        AddClimbEvents(glassTable, eventProvider);
        
        var mediaServer = new Item()
        {
            Key = Keys.SOCIALROOM_MEDIASERVER,
            Name = Items.SOCIALROOM_MEDIASERVER,
            Description = Descriptions.SOCIALROOM_MEDIASERVER,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(mediaServer);
        
        var books = new Item
        {
            Key = Keys.SOCIALROOM_BOOKS,
            Name = Items.SOCIALROOM_BOOKS,
            Description = GetBookTitle(),
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(books);
    }

    private static Func<string> GetBookTitle()
    {
        var bookList = new List<string>
        {
            Descriptions.SOCIALROOM_BOOK_I, Descriptions.SOCIALROOM_BOOK_II, Descriptions.SOCIALROOM_BOOK_III,
            Descriptions.SOCIALROOM_BOOK_IV, Descriptions.SOCIALROOM_BOOK_V,
            Descriptions.SOCIALROOM_BOOK_VI, Descriptions.SOCIALROOM_BOOK_VII, Descriptions.SOCIALROOM_BOOK_VIII,
            Descriptions.SOCIALROOM_BOOK_IX, Descriptions.SOCIALROOM_BOOK_X
        }; 
        
        var rnd = new Random();
        
        return () => string.Format(Descriptions.SOCIALROOM_BOOKS, bookList[rnd.Next(0, bookList.Count)]);
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
    
    private static void AddSitDownEvents(Item item, EventProvider eventProvider)
    {
        item.SitDown += eventProvider.SitDownOnCouchInSocialRoom;
    }
    
    private static void AddClimbEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeClimb += eventProvider.ClimbOnTablesInSocialRoom;
    }
}