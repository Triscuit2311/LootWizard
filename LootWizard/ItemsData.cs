using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LootWizard;

public class ItemsData
{
    public Dictionary<string, Item> ItemsDict = new();
    public List<SearchEntry> SearchEntries = new();
    public ObservableCollection<DisplayItem> SearchResults = new();
    public Dictionary<string, Item> SelectedItems = new();

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
}