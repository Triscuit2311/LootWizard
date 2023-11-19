using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LootWizard;

public record struct Item
{
    public Item(JsonElement itemElement)
    {
        id = itemElement.GetProperty("id").GetString();
        name = itemElement.GetProperty("name").GetString();
        searchable_full = name.ToLower();
        short_name = itemElement.GetProperty("shortName").GetString();
        searchable_short = short_name.ToLower();
        icon_link = itemElement.GetProperty("iconLink").GetString();
        avg_price = itemElement.GetProperty("avg24hPrice").GetInt32();

        width = itemElement.GetProperty("width").GetInt32();
        height = itemElement.GetProperty("height").GetInt32();
        slots = width * height;

        item_types = itemElement.GetProperty("types").EnumerateArray().Select(type => type.GetString()).ToList();
        properties_type = itemElement.GetProperty("properties").GetProperty("__typename").GetString();

        quest_ids = itemElement.GetProperty("usedInTasks").EnumerateArray()
            .Select(task => task.GetProperty("id").GetString()).ToList();
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