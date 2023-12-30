using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LootWizard;

public record struct Item
{
    public Item(JsonElement itemElement)
    {
        // Helper function to safely get properties from JsonElement
        T GetJsonProperty<T>(Func<JsonElement, T> accessor, T defaultValue)
        {
            try
            {
                return accessor(itemElement);
            }
            catch
            {
                return defaultValue;
            }
        }

        id = GetJsonProperty(e => e.GetProperty("id").GetString(), string.Empty);
        name = GetJsonProperty(e => e.GetProperty("name").GetString(), string.Empty);
        searchable_full = name.ToLower();
        short_name = GetJsonProperty(e => e.GetProperty("shortName").GetString(), string.Empty);
        searchable_short = short_name.ToLower();
        icon_link = GetJsonProperty(e => e.GetProperty("iconLink").GetString(), string.Empty);
        avg_price = GetJsonProperty(e => e.GetProperty("avg24hPrice").GetInt32(), 0);

        width = GetJsonProperty(e => e.GetProperty("width").GetInt32(), 0);
        height = GetJsonProperty(e => e.GetProperty("height").GetInt32(), 0);
        slots = width * height;

        item_types = GetJsonProperty(e => e.GetProperty("types").EnumerateArray().Select(type => type.GetString()).ToList(), new List<string>());

        properties_type = GetJsonProperty(e => e.GetProperty("properties").GetProperty("__typename").GetString(), string.Empty);

        quest_ids = GetJsonProperty(e => e.GetProperty("usedInTasks").EnumerateArray().Select(task => task.GetProperty("id").GetString()).ToList(), new List<string>());

        img_path = FileHelpers.ResolveImagePath($"img\\{id}-icon.jpeg");

    }

    public string id { get; set; }
    public string name { get; set; }
    public string short_name { get; set; }
    public string searchable_short { get; set; }
    public string searchable_full { get; set; }
    public string icon_link { get; set; }
    public int avg_price { get; set; }
    public int width { get; set; }
    public int height { get; set; }

    // width * height
    public int slots { get; set; }

    public List<string> item_types { get; set; }
    public string properties_type { get; set; }
    public string img_path { get; set; }
    public List<string> quest_ids { get; set; }
}

/*
{
"data": {
    "items": [
    {
        "id": "544fb45d4bdc2dee738b4568",
        "name": "Salewa first aid kit",
        "shortName": "Salewa",
        "iconLink": "https://assets.tarkov.dev/544fb45d4bdc2dee738b4568-icon.webp",
        "avg24hPrice": 33913,
        "width": 1,
        "height": 2,
        "usedInTasks": [
        {
            "tarkovDataId": 19
        }
        ],
        "types": [
        "meds",
        "provisions"
            ],
        "properties": {
            "__typename": "ItemPropertiesMedKit"
        }
    },
    {...},
    {...},
    ]
}
}
*/