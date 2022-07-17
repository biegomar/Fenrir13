using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Kitchen;

internal static class KitchenPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var kitchen = new Location()
        {
            Key = Keys.KITCHEN,
            Name = Locations.KITCHEN,
            Description = Descriptions.KITCHEN
        };

        kitchen.Items.Add(GetFridge(eventProvider));
        kitchen.Items.Add(GetFoodPrinter(eventProvider));
        kitchen.Items.Add(GetRecycler(eventProvider));

        AddSurroundings(kitchen, eventProvider);

        return kitchen;
    }

    private static void AddSurroundings(Location location, EventProvider eventProvider)
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
        
        var floor = new Item()
        {
            Key = Keys.CHAMBER_FLOOR,
            Name = Items.CHAMBER_FLOOR,
            Description = Descriptions.CHAMBER_FLOOR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(floor);
        
        var table = new Item()
        {
            Key = Keys.TABLE,
            Name = Items.TABLE,
            Description = Descriptions.TABLE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(table);
        
        var chair = new Item()
        {
            Key = Keys.CHAIR,
            Name = Items.CHAIR,
            Description = Descriptions.CHAIR,
            IsSurrounding = true,
            IsPickAble = false,
            IsSeatAble = true,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(chair);
        AddSitDownEvents(chair, eventProvider);
        
        var cabinet = new Item()
        {
            Key = Keys.KITCHEN_CABINET,
            Name = Items.KITCHEN_CABINET,
            Description = Descriptions.KITCHEN_CABINET,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(cabinet);
        
        var foodBag = new Item()
        {
            Key = Keys.KITCHEN_FOOD_BAG,
            Name = Items.KITCHEN_FOOD_BAG,
            Description = Descriptions.KITCHEN_FOOD_BAG,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male, isSingular: false)
        };
        location.Items.Add(foodBag);
    }

    private static Item GetFridge(EventProvider eventProvider)
    {
        var fridge = new Item()
        {
            Key = Keys.FRIDGE, 
            Name = Items.FRIDGE, 
            Description = Descriptions.FRIDGE,
            CloseDescription = Descriptions.FRIDGE_CLOSE,
            OpenDescription = Descriptions.FRIDGE_OPEN,
            LockDescription = Descriptions.FRIDGE_LOCKED,
            IsClosed = true,
            IsCloseAble = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        
        fridge.Items.Add(GetHandle(eventProvider));
        
        AddOpenEvents(fridge, eventProvider);

        return fridge;
    }
    
    private static void AddOpenEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeOpen += eventProvider.OpenFridge;
    }

    private static Item GetHandle(EventProvider eventProvider)
    {
        var handle = new Item()
        {
            Key = Keys.FRIDGE_DOOR_HANDLE,
            Name = Items.FRIDGE_DOOR_HANDLE,
            Description = Descriptions.FRIDGE_DOOR_HANDLE,
            ContainmentDescription = Descriptions.FRIDGE_DOOR_HANDLE_CONTAINMENT,
            UnPickAbleDescription = Descriptions.FRIDGE_DOOR_HANDLE_UNPICKABLE,
            IsUnveilAble = false,
            IsHidden = true,
            IsPickAble = false,
            Weight = ItemWeights.FRIDGE_DOOR_HANDLE,
            Grammar = new Grammars(Genders.Male)
        };

        AddPullEvents(handle, eventProvider);
        AddPushEvents(handle, eventProvider);
        
        return handle;
    }

    private static void AddPullEvents(Item item, EventProvider eventProvider)
    {
        item.Pull += eventProvider.PullFridgeHandle;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.PullFridgeHandle), 1);
    }
    
    private static void AddPushEvents(Item item, EventProvider eventProvider)
    {
        item.Push += eventProvider.PushDoorHandleIntoRespiratorFlap;
        item.Use += eventProvider.UseDoorHandleWithRespiratorFlap;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.PushDoorHandleIntoRespiratorFlap), 5);
    }
    
    private static void AddSitDownEvents(Item item, EventProvider eventProvider)
    {
        item.SitDown += eventProvider.SitDownOnChairInKitchen;
    }
    
    private static Item GetFoodPrinter(EventProvider eventProvider)
    {
        var oven = new Item() 
        {
            Key = Keys.FOOD_PRINTER, 
            Name = Items.FOOD_PRINTER, 
            Description = Descriptions.FOOD_PRINTER,
            CloseDescription = Descriptions.FOOD_PRINTER_CLOSE,
            OpenDescription = Descriptions.FOOD_PRINTER_OPEN,
            IsClosed = true,
            IsCloseAble = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        
        return oven;
    }
    
    private static Item GetRecycler(EventProvider eventProvider)
    {
        var recycler = new Item()
        {
            Key = Keys.RECYCLER,
            Name = Items.RECYCLER,
            Description = Descriptions.RECYCLER,
            FirstLookDescription = Descriptions.RECYCLER_FIRSTLOOK,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        
        return recycler;
    }
}