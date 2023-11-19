using System.Collections.Generic;
using System.Text.Json;

namespace LootWizard;

public record struct Quest
{
    public string id { get; set; }
    public string name;
    public List<RequiredItem> require_items;
    public string searchable_name;
    public string trader_name;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public List<RequiredItem> RequireItems
    {
        get => require_items;
        set => require_items = value;
    }

    public string SearchableName
    {
        get => searchable_name;
        set => searchable_name = value;
    }

    public string TraderName
    {
        get => trader_name;
        set => trader_name = value;
    }

    public Quest(JsonElement taskElement)
    {
        id = taskElement.GetProperty("id").GetString();
        name = taskElement.GetProperty("name").GetString();
        trader_name = taskElement.GetProperty("trader").GetProperty("name").GetString();
        searchable_name = name.ToLower();

        require_items = new List<RequiredItem>();
        foreach (var objective in taskElement.GetProperty("objectives").EnumerateArray())
            if (objective.GetProperty("type").GetString() == "giveItem")
            {
                var itemId = objective.GetProperty("item").GetProperty("id").GetString();
                var itemCount = objective.GetProperty("count").GetInt32();
                require_items.Add(new RequiredItem(itemId, itemCount));
            }
    }

    public struct RequiredItem
    {
        public string id;

        public string Id
        {
            get => id;
            set => id = value;
        }

        public int Count
        {
            get => count;
            set => count = value;
        }

        public int count;

        public RequiredItem(string id, int count)
        {
            this.id = id;
            this.count = count;
        }
    }
}