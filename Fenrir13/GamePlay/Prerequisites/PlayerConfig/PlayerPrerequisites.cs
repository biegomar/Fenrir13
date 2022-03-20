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
            MaxPayload = 10000,
        };
        
        AddSitDownEvents(player, eventProvider);
        AddStandUpEvents(player, eventProvider);
        AddPullEvents(player, eventProvider);

        return player;
    }

    private static void AddStandUpEvents(Player you, EventProvider eventProvider)
    {
        you.BeforeStandUp += eventProvider.BeforeStandUp;
        you.AfterStandUp += eventProvider.AfterStandUp;
    }
    
    private static void AddSitDownEvents(Player you, EventProvider eventProvider)
    {
        you.AfterSitDown += eventProvider.AfterSitDown;
        you.AfterSitDown += eventProvider.AfterSitDownOnCouch;
    }
    
    private static void AddPullEvents(Player you, EventProvider eventProvider)
    {
        you.Pull += eventProvider.PullOnWearablesOnPlayer;
        
    }
}