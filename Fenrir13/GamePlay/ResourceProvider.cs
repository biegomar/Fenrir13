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
                var normalizedList = this.NormalizeResourceList(inputList);
                result.Add(entry.Key.ToString()!, normalizedList);
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
                var inputList = entry.Value?.ToString()?.Split('|').ToList();
                var normalizedList = this.NormalizeResourceList(inputList);
                result.Add(entry.Key.ToString()!, normalizedList);
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
                var inputList = entry.Value?.ToString()?.Split('|').ToList();
                var normalizedList = this.NormalizeResourceList(inputList);
                result.Add(entry.Key.ToString()!, normalizedList);
            }
        }

        return result;
    }

    private IEnumerable<string> NormalizeResourceList(IEnumerable<string> inputList)
    {
        var result = new List<string>();
        foreach (var item in inputList)
        {
            result.Add(item);
            var trimmedItem = string.Concat(item.Where(c => !char.IsWhiteSpace(c)));
            if (item != trimmedItem)
            {
                result.Add(trimmedItem);
            }
        }

        return result;
    }
}