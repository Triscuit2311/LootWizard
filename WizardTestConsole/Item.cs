using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace WizardTestConsole;

public record struct Item
{
    public string id;
    public string name;
    public string short_name;
    public string searchable_short;
    public string searchable_full;
    public string icon_link;
    public int avg_price;
    public int width;
    public int height;
    
    // width * height
    public int slots;
    
    public List<string> item_types;
    public string properties_type;

    public List<string> quest_ids;
    
    public Item(JsonElement itemElement)
    {
        this.id = itemElement.GetProperty("id").GetString();
        this.name = itemElement.GetProperty("name").GetString();
        this.searchable_full = this.name.ToLower();
        this.short_name = itemElement.GetProperty("shortName").GetString();
        this.searchable_short = this.short_name.ToLower();
        this.icon_link = itemElement.GetProperty("iconLink").GetString();
        this.avg_price = itemElement.GetProperty("avg24hPrice").GetInt32();
        
        this.width = itemElement.GetProperty("width").GetInt32();
        this.height = itemElement.GetProperty("height").GetInt32();
        this.slots = this.width * this.height;

        this.item_types = itemElement.GetProperty("types").EnumerateArray().Select(type => type.GetString()).ToList();
        this.properties_type = itemElement.GetProperty("properties").GetProperty("__typename").GetString();

        this.quest_ids = itemElement.GetProperty("usedInTasks").EnumerateArray().Select(task => task.GetProperty("id").GetString()).ToList();
    }
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