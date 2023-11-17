using System.Collections.Generic;

namespace WizardTestConsole;

public class ItemsData
{
    public struct SearchEntry
    {
        public string short_name;
        public string full_name;
        public string id;

        public SearchEntry(string itemSearchableShort, string itemSearchableFull, string itemId)
        {
            short_name = itemSearchableShort;
            full_name = itemSearchableFull;
            id = itemId;
        }
    }
    
    public List<Item> ItemsList = new();
    public Dictionary<string, Item> ItemsDict = new();
    public List<SearchEntry> SearchEntries = new();
}