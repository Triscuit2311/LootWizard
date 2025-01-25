using System;
using System.Collections.Generic;

namespace LootWizard;

public static class LootFilters
{
    public static List<LootFilter> ActiveFilters = new();

    public static LootFilter FilterByFavorites = new(item => PersistentItemManager.Get(item.id).favorite);
    public static LootFilter FilterBySelected = new(item => PersistentItemManager.Get(item.id).selected);

    public static LootFilter FilterByPrice(int price)
    {
        if (price <= 0) return new LootFilter(item => true);

        return new LootFilter(item => item.avg_price >= price);
    }

    public static LootFilter FilterByPricePerSlot(int price)
    {
        if (price <= 0) return new LootFilter(item => true);




        return new LootFilter(item => item.avg_price / (item.slots > 0 ? item.slots : 1) >= price);
    }


    



    // public static LootFilter FilerByItemType(string item_type)
    // {
    //     if(item_type.Length < 1) return new LootFilter(item => true);
    //     return new LootFilter(item => item.item_types.Contains(item_type.ToLower()));
    // }
}

public class LootFilter
{
    private readonly Func<Item, bool> criteria_check;

    public LootFilter(Func<Item, bool> check)
    {
        criteria_check = check;
    }

    public bool MeetsCriteria(Item item)
    {
        return criteria_check(item);
    }
}