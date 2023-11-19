using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace LootWizard;

public class DisplayQuest
{
    public DisplayQuest(Quest quest, PersistentQuestData data)
    {
        this.quest = quest;
        
        traderName = quest.trader_name;
        questName = quest.name;
        trader_img_res = $"pack://application:,,,/LootWizard;component/resources/eft/traders/{traderName}.png";

        RequiredItems = new ObservableCollection<QuestItem>();

        foreach (var requireItem in quest.require_items)
        {
            try
            {
                var item = new QuestItem
                {
                    Id = requireItem.id,
                    Name = ItemsData.ItemsDict[requireItem.id].name,
                    ImageResource =
                        $"pack://application:,,,/LootWizard;component/resources/eft/items/{requireItem.id}-icon.jpeg",
                    QuantityNeeded = requireItem.count,
                    QuantityFound = data.found_items[requireItem.id],
                    QuantityNeededStr = $"Required: {requireItem.count}",
                    Selected = PersistentItemManager.Get(requireItem.id).selected
                };
                RequiredItems.Add(item);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }

    public Quest quest { get; set; }
    public string trader_img_res{ get; set; }
    public string traderName{ get; set; }
    public string questName{ get; set; }
    public ObservableCollection<QuestItem> RequiredItems { get; set; }
}

public class QuestItem : INotifyPropertyChanged
{
    private int _quantityFound;
    private bool _selected;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        PersistentItemManager.ItemChanged += OnPersistentItemChanged;
    }
    
    private void OnPersistentItemChanged(object sender, ItemChangedEventArgs e)
    {
        if (e.ItemId == Id)
        {
            Selected = e.IsSelected;
        }
    }
    
    

    public string Id { get; set; }
    public string Name { get; set; }
    public string ImageResource { get; set; }
    public int QuantityNeeded { get; set; }
    public string QuantityNeededStr { get; set; }
    
    
    public int QuantityFound
    {
        get => _quantityFound;
        set
        {
            _quantityFound = value;
            OnPropertyChanged(nameof(QuantityFound));
        }
    }
    
    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            OnPropertyChanged(nameof(Selected));
        }
    }


    public void IncrementQuantity()
    {
        QuantityFound++;
    }

    public void DecrementQuantity()
    {
        if (QuantityFound > 0)
            QuantityFound--;
    }
    
}