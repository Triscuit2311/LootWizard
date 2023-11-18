using System;

namespace LootWizard;

public class LootConfigManager
{
    public static bool Compile(ItemsData itemsData, QuestsData questsData)
    {
        string item_format = "{id}\t##{short_name} {fav}\n";
        string item_format_color = "{id}={color.r},{color.g},{color.b},1\t##\t{short_name}\n";
        
        Console.WriteLine($"Number of Selected Items: {itemsData.SelectedItems.Count}");

        foreach (var item in itemsData.SelectedItems)
        {
            
            //TODO 
            var pers = PersistentItemManager.Get(item.id);
            Console.WriteLine($"{item.id}: {item.short_name}: {pers.favorite}" );
        }
        
        return true;
    }
}