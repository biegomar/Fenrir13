using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Gym;

public class GymPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var gym = new Location()
        {
            Key = Keys.GYM,
            Name = Locations.GYM,
            Description = Descriptions.GYM
        };

        gym.Items.Add(GetDumbbellRack(eventProvider));
        
        AddSurroundings(gym);
        AddAfterLookEvents(gym, eventProvider);
        
        return gym;
    }
    
    private static void AddSurroundings(Location gym)
    {
        gym.Surroundings.Add(Keys.CARDIO_STATION, Descriptions.CARDIO_STATION);
        gym.Surroundings.Add(Keys.GYM_POWERSTATION, Descriptions.GYM_POWERSTATION);
        gym.Surroundings.Add(Keys.EXERCISE_AREA, Descriptions.EXERCISE_AREA);
        gym.Surroundings.Add(Keys.BIKE, Descriptions.GYM_FLOSKEL);
        gym.Surroundings.Add(Keys.CROSSTRAINER, Descriptions.GYM_FLOSKEL);
        gym.Surroundings.Add(Keys.TREADMILL, Descriptions.GYM_FLOSKEL);
        gym.Surroundings.Add(Keys.STEPPER, Descriptions.GYM_FLOSKEL);
        gym.Surroundings.Add(Keys.FITNESSMACHINE, Descriptions.FITNESSMACHINE);
        gym.Surroundings.Add(Keys.WORKOUTS, Descriptions.WORKOUTS);
    }
    
    private static Item GetDumbbellRack(EventProvider eventProvider)
    {
        var item = new Item()
        {
            Key = Keys.DUMBBELL_RACK,
            Name = Items.DUMBBELL_RACK,
            Description = Descriptions.DUMBBELL_RACK,
            IsHidden = true,
            IsPickAble = false
        };
        
        item.Items.Add(GetDumbbellBar(eventProvider));
        
        return item;
    }
    
    private static Item GetDumbbellBar(EventProvider eventProvider)
    {
        var item = new Item()
        {
            Key = Keys.DUMBBELL_BAR,
            Name = Items.DUMBBELL_BAR,
            Description = Descriptions.DUMBBELL_BAR,
            ContainmentDescription = Descriptions.DUMBBELL_BAR_CONTAINMENT,
            IsHidden = true,
            Weight = 5000
        };
        
        return item;
    }
    
    private static void AddAfterLookEvents(Location gym, EventProvider eventProvider)
    {
        gym.AfterLook += eventProvider.LookAtPowerStation;
    }
}