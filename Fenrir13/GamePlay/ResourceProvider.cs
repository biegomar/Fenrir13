using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay;

namespace Fenrir13.GamePlay;

internal class ResourceProvider: IResourceProvider
{
    public IDictionary<string, IEnumerable<string>> GetItemsFromResources()
    {
        return ((IResourceProvider)this).ReadEntriesFromResources(Items.ResourceManager);
    }

    public IDictionary<string, IEnumerable<string>> GetCharactersFromResources()
    {
        return ((IResourceProvider)this).ReadEntriesFromResources(Characters.ResourceManager);
    }

    public IDictionary<string, IEnumerable<string>> GetLocationsFromResources()
    {
        return ((IResourceProvider)this).ReadEntriesFromResources(Locations.ResourceManager);
    }
}