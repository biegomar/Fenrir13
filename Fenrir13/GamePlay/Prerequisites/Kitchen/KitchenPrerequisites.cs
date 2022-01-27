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

        AddSurroundings(kitchen);
        
        return kitchen;
    }

    private static void AddSurroundings(Location kitchen)
    {
        kitchen.Surroundings.Add(Keys.CEILING, Descriptions.CEILING);
    }

    private static Item GetFridge(EventProvider eventProvider)
    {
        var fridge = new Item()
        {
            Key = Keys.FRIDGE, 
            Name = Items.FRIDGE, 
            Description = Descriptions.FRIDGE,
            IsClosed = true,
            IsCloseAble = true,
        };
        
        fridge.Items.Add(GetHandle(eventProvider));
        
        AddOpenEvents(fridge, eventProvider);

        return fridge;
    }

    private static Item GetHandle(EventProvider eventProvider)
    {
        var handle = new Item()
        {
            Key = Keys.FRIDGE_DOOR_HANDLE,
            Name = Items.FRIDGE_DOOR_HANDLE,
            Description = Descriptions.FRIDGE_DOOR_HANDLE,
            IsUnveilAble = false,
            IsHidden = true
        };

        return handle;
    }

    private static void AddOpenEvents(Item item, EventProvider eventProvider)
    {
        item.AfterOpen += eventProvider.OpenFridge;
    }
    
    private static Item GetFoodPrinter(EventProvider eventProvider)
    {
        var oven = new Item() 
        {
            Key = Keys.FOOD_PRINTER, 
            Name = Items.FOOD_PRINTER, 
            Description = Descriptions.FOOD_PRINTER,
            OpenDescription = Descriptions.FOOD_PRINTER_OPEN,
            IsClosed = true,
            IsCloseAble = true,
        };
        
        return oven;
    }
}