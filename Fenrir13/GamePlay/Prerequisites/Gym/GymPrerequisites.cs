using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.Grammars;
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
            Description = Descriptions.GYM,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        
        AddNewVerbs(gym);

        gym.Items.Add(GetDumbbellRack(eventProvider));
        
        AddSurroundings(gym, eventProvider);
        AddAfterLookEvents(gym, eventProvider);

        return gym;
    }

    private static void AddNewVerbs(Location gym)
    {
        gym.AddOptionalVerb(VerbKey.USE, OptionalVerbs.DRIVE, Descriptions.GYM_IMPOSSIBLE_RIDE);
        gym.AddOptionalVerb(VerbKey.USE, OptionalVerbs.TRAIN, Descriptions.GYM_IMPOSSIBLE_TRAINING);
    }
    
    private static void AddSurroundings(Location location, EventProvider eventProvider)
    {
        var ceiling = new Item()
        {
            Key = Keys.CEILING,
            Name = Items.CEILING,
            Description = Descriptions.CEILING,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(ceiling);
        
        var wall = new Item()
        {
            Key = Keys.CHAMBER_WALL,
            Name = Items.CHAMBER_WALL,
            Description = Descriptions.CHAMBER_WALL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(wall);
        
        var cardio = new Item()
        {
            Key = Keys.CARDIO_STATION,
            Name = Items.CARDIO_STATION,
            Description = Descriptions.CARDIO_STATION,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(cardio);
        
        var power = new Item()
        {
            Key = Keys.GYM_POWERSTATION,
            Name = Items.GYM_POWERSTATION,
            Description = Descriptions.GYM_POWERSTATION,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(power);
        
        var exercise = new Item()
        {
            Key = Keys.EXERCISE_AREA,
            Name = Items.EXERCISE_AREA,
            Description = Descriptions.EXERCISE_AREA,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Male)
        };
        location.Items.Add(exercise);
        
        var bike = new Item()
        {
            Key = Keys.BIKE,
            Name = Items.BIKE,
            Description = Descriptions.GYM_FLOSKEL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Neutrum)
        };
        location.Items.Add(bike);
        
        var crossTrainer = new Item()
        {
            Key = Keys.CROSSTRAINER,
            Name = Items.CROSSTRAINER,
            Description = Descriptions.GYM_FLOSKEL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Male)
        };
        location.Items.Add(crossTrainer);
        
        var treadMill = new Item()
        {
            Key = Keys.TREADMILL,
            Name = Items.TREADMILL,
            Description = Descriptions.GYM_FLOSKEL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Neutrum)
        };
        location.Items.Add(treadMill);
        
        var stepper = new Item()
        {
            Key = Keys.STEPPER,
            Name = Items.STEPPER,
            Description = Descriptions.GYM_FLOSKEL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Male)
        };
        location.Items.Add(stepper);
        
        var fitnessMachine = new Item()
        {
            Key = Keys.FITNESSMACHINE,
            Name = Items.FITNESSMACHINE,
            Description = Descriptions.FITNESSMACHINE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(fitnessMachine);
        
        var workouts = new Item()
        {
            Key = Keys.WORKOUTS,
            Name = Items.WORKOUTS,
            Description = Descriptions.WORKOUTS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(workouts);
        
        var weightPlates = new Item()
        {
            Key = Keys.WEIGHT_PLATES,
            Name = Items.WEIGHT_PLATES,
            Description = Descriptions.WEIGHT_PLATES,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(weightPlates);
        
        var ropes = new Item()
        {
            Key = Keys.GYM_ROPES,
            Name = Items.GYM_ROPES,
            Description = Descriptions.GYM_ROPES,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Neutrum)
        };
        location.Items.Add(ropes);
        AddTakeEvents(ropes, eventProvider);
        AddRopeSkippingEvent(ropes, eventProvider);
        
        var climbingFrame = new Item()
        {
            Key = Keys.CLIMBING_FRAME,
            Name = Items.CLIMBING_FRAME,
            Description = Descriptions.CLIMBING_FRAME,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Neutrum)
        };
        location.Items.Add(climbingFrame);
        
        var bracket = new Item()
        {
            Key = Keys.GYM_BRACKET,
            Name = Items.GYM_BRACKET,
            Description = Descriptions.GYM_BRACKET,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(bracket);
        
        var sandBag = new Item()
        {
            Key = Keys.GYM_SANDBAG,
            Name = Items.GYM_SANDBAG,
            Description = Descriptions.GYM_SANDBAG,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(sandBag);
        AddTakeEvents(sandBag, eventProvider);
        
        var loop = new Item()
        {
            Key = Keys.GYM_LOOP,
            Name = Items.GYM_LOOP,
            Description = Descriptions.GYM_LOOP,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(loop);
    }

    private static void AddRopeSkippingEvent(Item rope, EventProvider eventProvider)
    {
        rope.Jump += eventProvider.RopeSkipping;
    }
    
    private static Item GetDumbbellRack(EventProvider eventProvider)
    {
        var item = new Item()
        {
            Key = Keys.DUMBBELL_RACK,
            Name = Items.DUMBBELL_RACK,
            Description = Descriptions.DUMBBELL_RACK,
            IsHidden = true,
            IsUnveilable = false,
            IsPickable = false
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
    
    private static void AddAfterLookEvents(Location gym, EventProvider eventProvider)
    {
        gym.AfterLook += eventProvider.LookAtPowerStation;
    }
    
    private static void AddAfterTakeEvents(Item bar, EventProvider eventProvider)
    {
        bar.AfterTake += eventProvider.TakeDumbbellBar;
        eventProvider.RegisterScore(nameof(eventProvider.TakeDumbbellBar), 1);
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseDumbbellBarWithLever;
        eventProvider.RegisterScore(nameof(eventProvider.UseDumbbellBarWithLever), 10);
    }
    
    private static void AddTakeEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeTake += eventProvider.TryToTakeThingsFromGym;
    }
}