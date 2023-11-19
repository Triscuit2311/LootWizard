using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LootWizard;

public static class PersistentQuestManager
{
    private static readonly Dictionary<string, PersistentQuestData> Map = new();

    public static void UpdateItemCount(string quest_id, string item_id, int count)
    {
        Map[quest_id].found_items[item_id] = count;
    }

    public static bool Has(string qid)
    {
        return Map.ContainsKey(qid);
    }
    public static void SetCompleted(string id, bool favorite)
    {
        Map[id].completed = favorite;
    }

    public static bool AddDoNotUpdate(string id, PersistentQuestData data)
    {
        if (!Map.ContainsKey(id)) Map[id] = data;
        return true;
    }

    public static void AddOrUpdate(string id, PersistentQuestData data)
    {
        Map[id] = data;
    }

    public static PersistentQuestData Get(string id)
    {
        return Map.TryGetValue(id, out var value) ? value : null;
    }

    public static void Save()
    {
        SerializeDictionaryToFile(Map, "persistent_quest_data.json");
    }

    public static bool LoadUpdate()
    {
        if (!File.Exists("persistent_quest_data.json")) return false;

        var tmap = DeserializeDictionaryFromFile("persistent_quest_data.json");
        foreach (var kvp in tmap)
        {
            Map[kvp.Key] = kvp.Value;
        }

        return true;
    }

    private static void SerializeDictionaryToFile(Dictionary<string, PersistentQuestData> data, string filePath)
    {
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            JsonSerializer.Serialize(new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }), data);
        }
    }

    private static Dictionary<string, PersistentQuestData> DeserializeDictionaryFromFile(string filePath)
    {
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            return JsonSerializer.Deserialize<Dictionary<string, PersistentQuestData>>(stream);
        }
    }


}



public class PersistentQuestData
{
    public string id { get; set; }
    public bool completed { get; set; }
    public Dictionary<string, int> found_items { get; set; } = new Dictionary<string, int>();

    
    public PersistentQuestData(string id, bool completed, Dictionary<string, int> foundItems)
    {
        this.id = id;
        this.completed = completed;

        foreach (var item in QuestsData.QuestList[id].require_items)
        {
            found_items[item.id] = 0;
        }

        foreach (var kvp in foundItems)
        {
            found_items[kvp.Key] = kvp.Value;
        }
    }
    
    public PersistentQuestData(){}
}