using System;
using System.IO;
using System.Text;

namespace LootWizard;

public class LootConfigManager
{
    public static bool Compile(ItemsData itemsData, QuestsData questsData)
    {
        // string item_format = "{id}\t##{short_name} {fav}\n";
        // string item_format_color = "{id}={color.r},{color.g},{color.b},1\t##\t{short_name}\n";

        Console.WriteLine($"Starting config gen, we have {itemsData.SelectedItems.Count} items selected.");

        var sb = new StringBuilder();
        foreach (var kvp in itemsData.SelectedItems) sb.Append($"{kvp.Value.id}\t##\t{kvp.Value.short_name}\n");

        File.WriteAllText("C:/_cfg/eft/loot_generated.ini", sb.ToString());

        Console.WriteLine("File written");

        return true;
    }
}