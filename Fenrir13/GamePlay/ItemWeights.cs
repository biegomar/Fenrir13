namespace Fenrir13.GamePlay;

internal static class ItemWeights
{
    internal static int DUMBBELL_BAR = 10000;
    internal static int HELMET = 2500;
    internal static int GLOVES = 150;
    internal static int BOOTS = 500;
    internal static int BELT = 150;
    internal static int FRIDGE_DOOR_HANDLE = 200;
    internal static int OXYGEN_BOTTLE = 3550;
    internal static int SOCIALROOM_ANTENNA = 150;
    internal static int MAINTENANCE_ROOM_TOOL = 100;
    
    internal static Dictionary <string,int> WeightDictionary = new Dictionary<string, int>()
    {
        {nameof(DUMBBELL_BAR), DUMBBELL_BAR},
        {nameof(HELMET), HELMET},
        {nameof(GLOVES), GLOVES},
        {nameof(BOOTS), BOOTS},
        {nameof(BELT), BELT},
        {nameof(FRIDGE_DOOR_HANDLE), FRIDGE_DOOR_HANDLE},
        {nameof(OXYGEN_BOTTLE), OXYGEN_BOTTLE},
        {nameof(SOCIALROOM_ANTENNA), SOCIALROOM_ANTENNA},
        {nameof(MAINTENANCE_ROOM_TOOL), MAINTENANCE_ROOM_TOOL}
    };



}