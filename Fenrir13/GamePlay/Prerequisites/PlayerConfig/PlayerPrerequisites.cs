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

        return player;
    }
}