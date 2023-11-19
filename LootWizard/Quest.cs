using System.Collections.Generic;
using System.Text.Json;

namespace LootWizard;

public record struct Quest
{
    public string id;
    public string name;
    public List<RequiredItem> require_items;
    public string searchable_name;
    public string trader_name;

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
        public int count;

        public RequiredItem(string id, int count)
        {
            this.id = id;
            this.count = count;
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