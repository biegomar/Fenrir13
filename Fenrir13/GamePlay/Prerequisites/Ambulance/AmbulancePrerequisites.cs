using System.Runtime.CompilerServices;
using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Ambulance;

internal static class AmbulancePrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var ambulance = new Location()
        {
            Key = Keys.AMBULANCE,
            Name = Locations.AMBULANCE,
            Description = Descriptions.AMBULANCE
        };
        
        ambulance.Items.Add(GetRespirator(eventProvider));
        
        AddSurroundings(ambulance);
        
        AddAfterLookEvents(ambulance, eventProvider);
        
        return ambulance;
    }
    
    private static Item GetRespirator(EventProvider eventProvider)
    {
        var respirator = new Item
        {
            Key = Keys.AMBULANCE_RESPIRATOR,
            Name = Items.AMBULANCE_RESPIRATOR,
            Description = Descriptions.AMBULANCE_RESPIRATOR,
            IsPickAble = false,
            IsHidden = true,
            IsUnveilAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        
        respirator.Items.Add(GetFlap(eventProvider));
        respirator.Items.Add(GetOxygenBottle(eventProvider));
        
        return respirator;
    }

    private static Item GetOxygenBottle(EventProvider eventProvider)
    {
        var oxygenBottle = new Item
        {
            Key = Keys.OXYGEN_BOTTLE,
            Name = Items.OXYGEN_BOTTLE,
            Description = Descriptions.OXYGEN_BOTTLE,
            Weight = ItemWeights.OXYGEN_BOTTLE,
            IsHidden = true,
            IsUnveilAble = false
        };
        
        AddUseEvents(oxygenBottle, eventProvider);

        return oxygenBottle;
    }
    
    private static Item GetFlap(EventProvider eventProvider)
    {
        var flap = new Item
        {
            Key = Keys.AMBULANCE_RESPIRATOR_FLAP,
            Name = Items.AMBULANCE_RESPIRATOR_FLAP,
            Description = Descriptions.AMBULANCE_RESPIRATOR_FLAP,
            FirstLookDescription = Descriptions.AMBULANCE_RESPIRATOR_FLAP_FIRSTLOOK,
            ContainmentDescription = Descriptions.AMBULANCE_RESPIRATOR_FLAP_CONTAINMENT,
            IsPickAble = false,
            IsLockAble = true,
            IsLocked = true,
            IsCloseAble = true,
            IsClosed = true,
            IsBreakable = true,
            LockDescription = Descriptions.AMBULANCE_RESPIRATOR_FLAP_LOCK
        };
        
        AddBreakEvents(flap, eventProvider);
        AddOpenCloseEvents(flap, eventProvider);
        
        return flap;
    }
    
    private static void AddSurroundings(Location location)
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
        
        var opTable = new Item()
        {
            Key = Keys.AMBULANCE_OP_TABLE,
            Name = Items.AMBULANCE_OP_TABLE,
            Description = Descriptions.AMBULANCE_OP_TABLE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(opTable);
        
        var opRoboter = new Item()
        {
            Key = Keys.AMBULANCE_OP_ROBOTER,
            Name = Items.AMBULANCE_OP_ROBOTER,
            Description = Descriptions.AMBULANCE_OP_ROBOTER,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(opRoboter);
        
        var opBed = new Item()
        {
            Key = Keys.AMBULANCE_BED,
            Name = Items.AMBULANCE_BED,
            Description = Descriptions.AMBULANCE_BED,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(opBed);
        
        var opItems = new Item()
        {
            Key = Keys.AMBULANCE_OP_ITEMS,
            Name = Items.AMBULANCE_OP_ITEMS,
            Description = Descriptions.AMBULANCE_OP_ITEMS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(opItems);
        
        var opItemDetails = new Item()
        {
            Key = Keys.AMBULANCE_OP_ITEMS_DETAIL,
            Name = Items.AMBULANCE_OP_ITEMS_DETAIL,
            Description = Descriptions.AMBULANCE_OP_ITEMS_DETAIL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(opItemDetails);
        
        var opMedis = new Item()
        {
            Key = Keys.AMBULANCE_MEDIS,
            Name = Items.AMBULANCE_MEDIS,
            Description = Descriptions.AMBULANCE_MEDIS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum, false)
        };
        location.Items.Add(opMedis);
        
        var opMedisCabinet = new Item()
        {
            Key = Keys.AMBULANCE_MEDIS_CABINET,
            Name = Items.AMBULANCE_MEDIS_CABINET,
            Description = Descriptions.AMBULANCE_MEDIS_CABINET,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male, false)
        };
        location.Items.Add(opMedisCabinet);
        
        var opRespirator = new Item()
        {
            Key = Keys.AMBULANCE_RESPIRATOR,
            Name = Items.AMBULANCE_RESPIRATOR,
            Description = Descriptions.AMBULANCE_RESPIRATOR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(opRespirator);
        
        var opHose = new Item()
        {
            Key = Keys.AMBULANCE_OXYGEN_HOSE,
            Name = Items.AMBULANCE_OXYGEN_HOSE,
            Description = Descriptions.AMBULANCE_OXYGEN_HOSE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male, false)
        };
        location.Items.Add(opHose);
    }
    
    private static void AddAfterLookEvents(Location ambulance, EventProvider eventProvider)
    {
        ambulance.AfterLook += eventProvider.LookAtRespirator;
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseOxygenBottleWithHelmet;
    }
    
    private static void AddBreakEvents(Item item, EventProvider eventProvider)
    {
        item.Break += eventProvider.BreakFlapWithDumbbellBar;
    }
    
    private static void AddOpenCloseEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeOpen += eventProvider.OpenFlap;
    }
}