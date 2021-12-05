using System.Collections;
using System.Globalization;
using System.Resources;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay;

namespace Fenrir13.GamePlay;

internal class ResourceProvider: IResourceProvider
{
    public IDictionary<string, IEnumerable<string>> GetConversationsAnswersFromResources()
    {
        return new Dictionary<string, IEnumerable<string>>();
    }

    
    public IDictionary<string, IEnumerable<string>> GetItemsFromResources()
    {
        var result = new Dictionary<string, IEnumerable<string>>();


        ResourceSet resourceSet =
            Items.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        if (resourceSet != null)
        {
            foreach (DictionaryEntry entry in resourceSet)
            {
                var inputList = entry.Value?.ToString()?.Split('|').ToList();
                result.Add(entry.Key.ToString()!, inputList);
            }
        }

        return result;
    }

    public IDictionary<string, IEnumerable<string>> GetCharactersFromResources()
    {
        var result = new Dictionary<string, IEnumerable<string>>();

        ResourceSet resourceSet =
            Characters.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        if (resourceSet != null)
        {
            foreach (DictionaryEntry entry in resourceSet)
            {
                var inputList = entry.Value?.ToString().Split('|').ToList();
                result.Add(entry.Key.ToString()!, inputList);
            }
        }

        return result;
    }

    public IDictionary<string, IEnumerable<string>> GetLocationsFromResources()
    {
        var result = new Dictionary<string, IEnumerable<string>>();

        ResourceSet resourceSet =
            Locations.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        if (resourceSet != null)
        {
            foreach (DictionaryEntry entry in resourceSet)
            {
                var inputList = entry.Value?.ToString().Split('|').ToList();
                result.Add(entry.Key.ToString()!, inputList);
            }
        }

        return result;
    }
}