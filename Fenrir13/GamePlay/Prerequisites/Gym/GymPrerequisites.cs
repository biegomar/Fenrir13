using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Gym;

internal static class GymPrerequisites
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
        AddTakeEvents(gym, eventProvider);
        
        return gym;
    }
    
    private static void AddSurroundings(Location gym)
    {
        gym.Surroundings.Add(Keys.CARDIO_STATION, () => Descriptions.CARDIO_STATION);
        gym.Surroundings.Add(Keys.GYM_POWERSTATION, () => Descriptions.GYM_POWERSTATION);
        gym.Surroundings.Add(Keys.EXERCISE_AREA, () => Descriptions.EXERCISE_AREA);
        gym.Surroundings.Add(Keys.BIKE, () => Descriptions.GYM_FLOSKEL);
        gym.Surroundings.Add(Keys.CROSSTRAINER, () => Descriptions.GYM_FLOSKEL);
        gym.Surroundings.Add(Keys.TREADMILL, () => Descriptions.GYM_FLOSKEL);
        gym.Surroundings.Add(Keys.STEPPER, () => Descriptions.GYM_FLOSKEL);
        gym.Surroundings.Add(Keys.FITNESSMACHINE, () => Descriptions.FITNESSMACHINE);
        gym.Surroundings.Add(Keys.WORKOUTS, () => Descriptions.WORKOUTS);
        gym.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
        gym.Surroundings.Add(Keys.WEIGHT_PLATES, () => Descriptions.WEIGHT_PLATES);
        gym.Surroundings.Add(Keys.GYM_ROPES, () => Descriptions.GYM_ROPES);
        gym.Surroundings.Add(Keys.GYM_BRACKET, () => Descriptions.GYM_BRACKET);
        gym.Surroundings.Add(Keys.GYM_SANDBAG, () => Descriptions.GYM_SANDBAG);
        gym.Surroundings.Add(Keys.GYM_LOOP, () => Descriptions.GYM_LOOP);
    }
    
    private static Item GetDumbbellRack(EventProvider eventProvider)
    {
        var item = new Item()
        {
            Key = Keys.DUMBBELL_RACK,
            Name = Items.DUMBBELL_RACK,
            Description = Descriptions.DUMBBELL_RACK,
            IsHidden = true,
            IsUnveilAble = false,
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
            Weight = ItemWeights.DUMBBELL_BAR
        };
        
        AddAfterTakeEvents(item, eventProvider);
        AddUseEvents(item, eventProvider);
        
        
        return item;
    }
    
    private static void AddAfterLookEvents(AContainerObject gym, EventProvider eventProvider)
    {
        gym.AfterLook += eventProvider.LookAtPowerStation;
    }
    
    private static void AddAfterTakeEvents(AContainerObject bar, EventProvider eventProvider)
    {
        bar.AfterTake += eventProvider.TakeDumbbellBar;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.TakeDumbbellBar), 1);
    }
    
    private static void AddUseEvents(AContainerObject item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseDumbbellBarWithLever;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.UseDumbbellBarWithLever), 10);
    }
    
    private static void AddTakeEvents(AContainerObject item, EventProvider eventProvider)
    {
        item.Take += eventProvider.TryToTakeThingsFromGym;
    }
}