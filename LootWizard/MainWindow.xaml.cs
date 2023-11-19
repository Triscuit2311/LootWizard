using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FuzzySharp;

namespace LootWizard;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly ItemsData itemsData = new();
    private readonly QuestsData questsData = new();

    public MainWindow()
    {
        InitializeComponent();

        PriceFilterType.SelectedIndex = 0;
        SearchBox.Text = "";
        // Bind the source
        ItemsList.ItemsSource = itemsData.SearchResults;
        Loaded += MainWindow_Loaded;
        Closed += MainWindow_Closed;
    }


    private async Task LoadCache()
    {
        using (var cacheManager = new CacheManager())
        {
            await CacheManager.Update(questsData, itemsData);
        }
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Run the async function
        await LoadCache();
    }

    private void MainWindow_Closed(object sender, EventArgs e)
    {
        PersistentItemManager.Save();
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchTerm = SearchBox.Text;
        SearchItems(searchTerm, LootFilters.ActiveFilters);
    }

    private void SearchItems(string searchTerm, List<LootFilter> filters)
    {
        itemsData.SearchResults.Clear();
        var query = searchTerm.ToLower();
        foreach (var entry in itemsData.SearchEntries)
        {
            var item = itemsData.ItemsDict[entry.id];

            var flag = filters.All(filter => filter.MeetsCriteria(item));

            if (!flag) continue;

            if (searchTerm.Length < 1)
            {
                itemsData.SearchResults.Add(new DisplayItem(item, PersistentItemManager.Get(entry.id)));
                continue;
            }

            if (Fuzz.WeightedRatio(query, entry.short_name) > 80 || Fuzz.WeightedRatio(query, entry.full_name) > 80)
                itemsData.SearchResults.Add(new DisplayItem(item, PersistentItemManager.Get(entry.id)));
        }
    }


    private void Image_OnFailed(object sender, ExceptionRoutedEventArgs e)
    {
        Console.WriteLine("Failed to load image: " + e.ErrorException.Message);
    }

    private void ApplyFilters(object sender, RoutedEventArgs e)
    {
        LootFilters.ActiveFilters.Clear();

        // Price or Price-per-slot filter
        if (int.TryParse(PriceFilterBox.Text, out var price))
        {
            if (PriceFilterType.SelectedIndex == 0) // Filter by Price
                LootFilters.ActiveFilters.Add(LootFilters.FilterByPrice(price));
            else if (PriceFilterType.SelectedIndex == 1) // Filter by Price per Slot
                LootFilters.ActiveFilters.Add(LootFilters.FilterByPricePerSlot(price));
        }

        // Favorites filter
        if (FavoritesFilterCheckBox.IsChecked == true) LootFilters.ActiveFilters.Add(LootFilters.FilterByFavorites);

        // Selected filter
        if (SelectedFilterCheckBox.IsChecked == true) LootFilters.ActiveFilters.Add(LootFilters.FilterBySelected);

        var searchTerm = SearchBox.Text;
        SearchItems(searchTerm, LootFilters.ActiveFilters);

        // Apply filters to your items data and update the UI accordingly
    }


    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        LootConfigManager.Compile(itemsData, questsData);
    }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleButton toggleButton) return;
        var displayItem = toggleButton.DataContext as DisplayItem? ?? default;
        itemsData.SelectedItems[displayItem.item.id] = displayItem.item;
    }

    private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleButton toggleButton) return;
        var displayItem = toggleButton.DataContext as DisplayItem? ?? default;
        itemsData.SelectedItems.Remove(displayItem.item.id);
    }
}