﻿using System.Windows.Media;
namespace LootWizard;

public record struct DisplayItem
{
    public Item item { get; set; }
    public PersistentItemData itemData { get; set; }
    public Brush brush { get; set; }

    public string display_name{ get; set; }
    public string display_price{ get; set; }
    public string display_price_slot{ get; set; }
    
    
    public static string StarActivePath { get; set; }
    public static string StarInactivePath { get; set; }
    
    public static string CheckActivePath { get; set; }
    public static string CheckInactivePath { get; set; }
    
    public DisplayItem(Item _item, PersistentItemData _data)
    {
        item = _item;
        itemData = _data;
        brush = new SolidColorBrush(_data.color);
        display_name = $"{item.name}";
        display_price = $"{item.avg_price}\u20bd";
        display_price_slot = $"({item.avg_price/(item.slots > 0 ? item.slots : 1)}\u20bd/slot)";

        if (StarActivePath == null)
        {
            StarActivePath = FileHelpers.ResolveImagePath("img/star_active.png");
            StarInactivePath = FileHelpers.ResolveImagePath("img/star_inactive.png");
            CheckActivePath = FileHelpers.ResolveImagePath("img/check_active.png");
            CheckInactivePath = FileHelpers.ResolveImagePath("img/check_inactive.png");
        }
    }
}