using System.Collections.Generic;
using System.Text.Json;

namespace WizardTestConsole;

public record struct Quest
{
    public struct RequiredItem
    {
        public string id;
        public int count;
        public RequiredItem(string id, int count)
        {
            this.id = id;
            this.count = count;
        }
    }
    public string id;
    public string name;
    public string trader_name;
    public string searchable_name;
    public List<RequiredItem> require_items;
    
    public Quest(JsonElement taskElement)
    {
        this.id = taskElement.GetProperty("id").GetString();
        this.name = taskElement.GetProperty("name").GetString();
        this.trader_name = taskElement.GetProperty("trader").GetProperty("name").GetString();
        this.searchable_name = name.ToLower();

        this.require_items = new List<RequiredItem>();
        foreach (var objective in taskElement.GetProperty("objectives").EnumerateArray())
        {
            if (objective.GetProperty("type").GetString() == "giveItem")
            {
                string itemId = objective.GetProperty("item").GetProperty("id").GetString();
                int itemCount = objective.GetProperty("count").GetInt32();
                this.require_items.Add(new RequiredItem(itemId, itemCount));
            }
        }
    }
    
}

//
// {
// "data": {
//     "tasks": [
//     {
//         "id": "5936d90786f7742b1420ba5b",
//         "name": "Debut",
//         "trader": {
//             "name": "Prapor"
//         },
//         "objectives": [
//         {
//             "type": "shoot"
//         },
//         {
//             "type": "giveItem",
//             "item": {
//                 "id": "54491c4f4bdc2db1078b4568"
//             },
//             "count": 2
//         }
//         ]
//     },
//     {
//         "id": "5967733e86f774602332fc84",
//         "name": "Shortage",
//         "trader": {
//             "name": "Therapist"
//         },
//         "objectives": [
//         {
//             "type": "findItem",
//             "item": {
//                 "id": "544fb45d4bdc2dee738b4568"
//             },
//             "count": 3
//         },
//         {
//             "type": "giveItem",
//             "item": {
//                 "id": "544fb45d4bdc2dee738b4568"
//             },
//             "count": 3
//         }
//         ]
//     },
//     {
//         "id": "5ac23c6186f7741247042bad",
//         "name": "Gunsmith - Part 1",
//         "trader": {
//             "name": "Mechanic"
//         },
//         "objectives": [
//         {
//             "type": "buildWeapon"
//         }
//         ]
//     }
//     ]
//     }
// }