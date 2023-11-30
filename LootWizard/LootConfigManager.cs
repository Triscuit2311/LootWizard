using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace LootWizard;

public class LootConfigManager
{
    private static string output_path = "C:/_cfg/eft/";
    private static string persist_path = "output_path.json";

    public static void ChangeOutputPath(string path)
    {
        if (!IsValidPath(path))
        {
            Console.WriteLine("Invalid path.");
            return;
        }
        if (!Directory.Exists(path))
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory: {ex.Message}");
                return;
            }
        }
        output_path = path;
        SavePathToJsonFile(path);
    }

    public static bool IsValidPath(string path)
    {
        try
        {
            Path.GetFullPath(path);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool TryCreateDirectory(string path)
    {
        try
        {
            Directory.CreateDirectory(path);
            return true;
        }
        catch
        {
            return false;
        }
    }


    private static void SavePathToJsonFile(string path)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(new { outputPath = path }, options);
            File.WriteAllText(persist_path, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to JSON file: {ex.Message}");
        }
    }
    public static bool Compile(ItemsData itemsData, QuestsData questsData)
    {
        Console.WriteLine($"Starting config gen, we have {itemsData.SelectedItems.Count} items selected.");
    
        // Ensure the directory exists or create it
        string directoryPath = Path.GetDirectoryName(output_path + "/loot_generated.ini");
        if (!Directory.Exists(directoryPath))
        {
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create directory: {ex.Message}");
                return false;
            }
        }

        var sb = new StringBuilder();
        foreach (var kvp in itemsData.SelectedItems) 
        {
            sb.Append($"{kvp.Value.id} # {kvp.Value.short_name}\n");
        }

        try
        {
            File.WriteAllText(output_path + "/loot_generated.ini", sb.ToString());
            Console.WriteLine("File written");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write file: {ex.Message}");
            return false;
        }
    }
}