using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.PlayerConfig;

public class PlayerPrerequisites
{
    internal static Player Get(EventProvider eventProvider)
    {
        var player = new Player()
        {
            Key = Keys.PLAYER,
            Name = "",
            Description = Descriptions.PLAYER,
            FirstLookDescription = Descriptions.PLAYER_FIRSTLOOK,
            MaxPayload = 5000,
        };
        
        AddSitDownEvents(player, eventProvider);
        AddStandUpEvents(player, eventProvider);

        return player;
    }

    private static void AddStandUpEvents(Player you, EventProvider eventProvider)
    {
        you.BeforeStandUp += eventProvider.BeforeStandUp;
    }
    
    private static void AddSitDownEvents(Player you, EventProvider eventProvider)
    {
        you.AfterSitDown += eventProvider.AfterSitDown;
    }
}