using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace LootWizard;

public static class PersistentItemManager
{
    private static Dictionary<string, PersistentItemData> Map = new();

    public static void SetFavorite(string id, bool favorite)
    {
        Map[id].favorite = favorite;
    }

    public static void SetSelected(string id, bool selected)
    {
        Map[id].selected = selected;
    }

    
    public static void SetColor(string id, Color color)
    {
        Map[id].color = color;
    }

    public static bool AddDoNotUpdate(string id, PersistentItemData data)
    {
        if (!Map.ContainsKey(id))
        {
            Map[id] = data;
        }
        return true;
    }
    
    public static void AddOrUpdate(string id, PersistentItemData data)
    {
        Map[id] = data;
    }
    
    public static PersistentItemData Get(string id)
    {
        return Map.TryGetValue(id, out var value) ? value : null;
    }
    
    public static void Save()
    {
        SerializeDictionaryToFile(Map,"persistent_item_data.json");
    }
    
    public static bool LoadUpdate()
    {
        if (!File.Exists("persistent_item_data.json"))
        {
            return false;
        }
        var tmap = DeserializeDictionaryFromFile("persistent_item_data.json");
        foreach (var kvp in tmap)
        {
            Map[kvp.Key] = kvp.Value;
        }

        return true;
    }
    private static void SerializeDictionaryToFile(Dictionary<string, PersistentItemData> data, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(filePath, json);
    }

    private static Dictionary<string, PersistentItemData> DeserializeDictionaryFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new Dictionary<string, PersistentItemData>();
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Dictionary<string, PersistentItemData>>(json);
    }
}

public class PersistentItemData
{


    // public struct ItemColor
    // {
    //     public int R  { get; set; }
    //     public int G { get; set; }
    //     public int B { get; set; }
    //     public int A { get; set; }
    //
    //     public ItemColor(int r, int g, int b, int a = 255)
    //     {
    //         R = r;
    //         G = g;
    //         B = b;
    //         A = a;
    //     }
    //     
    //     public ItemColor()
    //     {
    //         R = 255;
    //         G = 255;
    //         B = 255;
    //         A = 255;
    //     }
    // }
    
    public string id  { get; set; }
    public Color color  { get; set; }
    public bool favorite  { get; set; }
    public bool selected{ get; set; }

    public PersistentItemData(string id, Color color, bool favorite)
    {
        this.id = id;
        this.color = color;
        this.favorite = favorite;
        this.selected = false;
    }

}