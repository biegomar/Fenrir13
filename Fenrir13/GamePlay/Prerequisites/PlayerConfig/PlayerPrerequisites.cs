using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.PlayerConfig;

public static class PlayerPrerequisites
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
            Grammar = new IndividualObjectGrammar(Genders.Male, isPlayer:true)
        };
        
        AddSitDownEvents(player, eventProvider);
        AddStandUpEvents(player, eventProvider);
        AddToBeEvents(player, eventProvider);
        
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
        eventProvider.ScoreBoard.Add(nameof(eventProvider.AfterSitDownOnQuestsSolved), 1);
    }

    private static void AddToBeEvents(Player you, EventProvider eventProvider)
    {
        you.ToBe += eventProvider.SetPlayersName;
    }
}