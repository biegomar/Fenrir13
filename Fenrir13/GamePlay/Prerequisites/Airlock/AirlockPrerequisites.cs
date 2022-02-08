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
            ContainmentDescription = Descriptions.AIRLOCK_KEYPAD_CONTAINMENT
            
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
            IsHidden = true
        };

        return greenButton;
    }
    
    private static Item GetRedButton(EventProvider eventProvider)
    {
        var redButton = new Item
        {
            Key = Keys.AIRLOCK_KEYPAD_RED_BUTTON,
            Name = Items.AIRLOCK_KEYPAD_RED_BUTTON,
            Description = Descriptions.AIRLOCK_KEYPAD_RED_BUTTON,
            IsHidden = true
        };

        return redButton;
    }
    
    private static void AddSurroundings(Location airlock)
    {
        airlock.Surroundings.Add(Keys.AIRLOCK_KEYPAD, Descriptions.AIRLOCK_KEYPAD);
    }
    
    private static void AddChangeRoomEvents(Location airlock, EventProvider eventProvider)
    {
        airlock.AfterChangeLocation += eventProvider.EnterAirlock;
        airlock.BeforeChangeLocation += eventProvider.LeaveAirlock;
    }
}