using Fenrir13.Events;
using Fenrir13.GamePlay.Prerequisites.CryoChamber;
using Fenrir13.GamePlay.Prerequisites.PlayerConfig;
using Heretic.InteractiveFiction.Comparer;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay;

internal class GamePrerequisitesAssembler: IGamePrerequisitesAssembler
{
    private readonly EventProvider eventProvider;

    public GamePrerequisitesAssembler(EventProvider eventProvider)
    {
        this.eventProvider = eventProvider;
    }

    public (LocationMap map, Location activeLocation, Player activePlayer) AssembleGame()
    {
        var map = new LocationMap(new LocationComparer());

        var cryoChamber = CryoChamberPrerequisites.Get(this.eventProvider);
        
        
        
        var activeLocation = cryoChamber;

        var activePlayer = PlayerPrerequisites.Get(this.eventProvider);
        
        return (map, activeLocation, activePlayer);
    }
}