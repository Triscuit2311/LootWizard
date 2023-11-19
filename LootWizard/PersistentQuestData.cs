using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LootWizard;

public static class PersistentQuestManager
{
    private static readonly Dictionary<string, PersistentQuestData> Map = new();

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
        foreach (var kvp in tmap) Map[kvp.Key] = kvp.Value;

        return true;
    }

    private static void SerializeDictionaryToFile(Dictionary<string, PersistentQuestData> data, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(filePath, json);
    }

    private static Dictionary<string, PersistentQuestData> DeserializeDictionaryFromFile(string filePath)
    {
        if (!File.Exists(filePath)) return new Dictionary<string, PersistentQuestData>();

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Dictionary<string, PersistentQuestData>>(json);
    }
}



public class PersistentQuestData
{
    public string id;
    public bool completed{ get; set; }
    public Dictionary<string, int> found_items{ get; set; }
    
    public PersistentQuestData(string id, bool completed, Dictionary<string, int> foundItems)
    {
        this.id = id;
        this.completed = completed;
        found_items = foundItems;
    }
}