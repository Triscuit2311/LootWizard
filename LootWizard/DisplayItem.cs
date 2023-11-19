using System.Windows.Media;

namespace LootWizard;

public record struct DisplayItem
{
    public DisplayItem(Item _item, PersistentItemData _data)
    {
        item = _item;
        itemData = _data;
        brush = new SolidColorBrush(_data.color);
        display_name = $"{item.name}";
        display_price = $"{item.avg_price}\u20bd";
        display_price_slot = $"({item.avg_price / (item.slots > 0 ? item.slots : 1)}\u20bd/slot)";
        img_res = $"pack://application:,,,/LootWizard;component/resources/eft/items/{item.id}-icon.jpeg";
        PersistentItemManager.ItemChanged += OnPersistentItemChanged;
    }

    private void OnPersistentItemChanged(object sender, ItemChangedEventArgs e)
    {
        if (e.ItemId == itemData.id)
        {
            itemData.selected = e.IsSelected;
        }
    }

    public string img_res { get; set; }
    public Item item { get; set; }
    public PersistentItemData itemData { get; set; }
    public Brush brush { get; set; }

    public string display_name { get; set; }
    public string display_price { get; set; }
    public string display_price_slot { get; set; }

}