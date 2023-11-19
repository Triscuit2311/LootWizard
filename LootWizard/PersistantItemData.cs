using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Media;

namespace LootWizard;

public static class PersistentItemManager
{
    private static readonly Dictionary<string, PersistentItemData> Map = new();

    public static void SetFavorite(string id, bool favorite)
    {
        Map[id].favorite = favorite;
    }

    public static void SetSelected(string id, bool selected)
    {
        Map[id].selected = selected;
        NotifyItemChanged(id,selected);
    }


    public static void SetColor(string id, Color color)
    {
        Map[id].color = color;
    }

    public static bool AddDoNotUpdate(string id, PersistentItemData data)
    {
        if (!Map.ContainsKey(id)) Map[id] = data;
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
        SerializeDictionaryToFile(Map, "persistent_item_data.json");
    }

    public static bool LoadUpdate()
    {
        if (!File.Exists("persistent_item_data.json")) return false;
        var tmap = DeserializeDictionaryFromFile("persistent_item_data.json");
        foreach (var kvp in tmap) Map[kvp.Key] = kvp.Value;

        return true;
    }

    private static void SerializeDictionaryToFile(Dictionary<string, PersistentItemData> data, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(filePath, json);
    }

    private static Dictionary<string, PersistentItemData> DeserializeDictionaryFromFile(string filePath)
    {
        if (!File.Exists(filePath)) return new Dictionary<string, PersistentItemData>();

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Dictionary<string, PersistentItemData>>(json);
    }
    
    public static event EventHandler<ItemChangedEventArgs> ItemChanged;

    public static void NotifyItemChanged(string itemId, bool isSelected)
    {
        ItemChanged?.Invoke(null, new ItemChangedEventArgs(itemId, isSelected));
    }
}

public class ItemChangedEventArgs : EventArgs
{
    public string ItemId { get; private set; }
    public bool IsSelected { get; private set; }

    public ItemChangedEventArgs(string itemId, bool isSelected)
    {
        ItemId = itemId;
        IsSelected = isSelected;
    }
}

public class PersistentItemData
{
    public PersistentItemData(string id, Color color, bool favorite)
    {
        this.id = id;
        this.color = color;
        this.favorite = favorite;
        selected = false;
    }

    public string id { get; set; }
    public Color color { get; set; }
    public bool favorite { get; set; }
    public bool selected { get; set; }
}