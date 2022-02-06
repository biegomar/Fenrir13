namespace Fenrir13.GamePlay;

public static class ItemWeights
{
    public static int DUMBBELL_BAR = 10000;
    public static int HELMET = 2500;
    public static int GLOVES = 150;
    public static int BOOTS = 500;
    public static int BELT = 150;
    public static int FRIDGE_DOOR_HANDLE = 200;
    public static int OXYGEN_BOTTLE = 3550;
    
    public static Dictionary <string,int> WeightDictionary = new Dictionary<string, int>()
    {
        {nameof(DUMBBELL_BAR), DUMBBELL_BAR},
        {nameof(HELMET), HELMET},
        {nameof(GLOVES), GLOVES},
        {nameof(BOOTS), BOOTS},
        {nameof(BELT), BELT},
        {nameof(FRIDGE_DOOR_HANDLE), FRIDGE_DOOR_HANDLE},
        {nameof(OXYGEN_BOTTLE), OXYGEN_BOTTLE}
    };



}