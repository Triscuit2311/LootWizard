using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
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
        ItemsSearchBox.Text = "";
        
        
        // Bind the sources
        ItemsList.ItemsSource = itemsData.SearchResults;
        
        QuestsListPrapor.ItemsSource = questsData.PraporQuests;
        QuestsListTherapist.ItemsSource = questsData.TherapistQuests;
        QuestsListSkier.ItemsSource = questsData.SkierQuests;
        QuestsListFence.ItemsSource = questsData.FenceQuests;
        QuestsListPeacekeeper.ItemsSource = questsData.PeacekeeperQuests;
        QuestsListMechanic.ItemsSource = questsData.MechanicQuests;
        QuestsListRagman.ItemsSource = questsData.RagmanQuests;
        QuestsListJaeger.ItemsSource = questsData.JaegerQuests;
        
        QuestsListLightkeeper.ItemsSource = questsData.LightkeeperQuests;
        
        
        
        PersistentItemManager.ItemChanged += OnPersistentItemChanged;

        Loaded += MainWindow_Loaded;
        Closed += MainWindow_Closed;
    }

    private void OnPersistentItemChanged(object sender, ItemChangedEventArgs e)
    {
        ApplyFilters(sender, new RoutedEventArgs());
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
        PersistentQuestManager.Save();
    }

    private void ItemsSearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchTerm = ItemsSearchBox.Text;
        SearchItems(searchTerm, LootFilters.ActiveFilters);
    }

    private void SearchItems(string searchTerm, List<LootFilter> filters)
    {
        itemsData.SearchResults.Clear();
        var query = searchTerm.ToLower();
        foreach (var entry in itemsData.SearchEntries)
        {
            var item = ItemsData.ItemsDict[entry.id];

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

    public void ApplyFilters(object sender, RoutedEventArgs e)
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

        var searchTerm = ItemsSearchBox.Text;
        SearchItems(searchTerm, LootFilters.ActiveFilters);
    }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleButton toggleButton) return;
        var displayItem = toggleButton.DataContext as DisplayItem? ?? default;
        itemsData.SelectedItems[displayItem.item.id] = displayItem.item;
        LootConfigManager.Compile(itemsData, questsData);
        PersistentItemManager.SetSelected(displayItem.item.id, true);
    }

    private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleButton toggleButton) return;
        var displayItem = toggleButton.DataContext as DisplayItem? ?? default;
        itemsData.SelectedItems.Remove(displayItem.item.id);
        LootConfigManager.Compile(itemsData, questsData);
        PersistentItemManager.SetSelected(displayItem.item.id, false);
    }

    

    private void QuestItemInc_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button but) return;
        var displayItem = but.DataContext as QuestItem?? default;

        var item = ItemsData.ItemsDict[displayItem.Id];

        displayItem.QuantityFound++;
        
        foreach (var qid in item.quest_ids)
        {
            if (!PersistentQuestManager.Has(qid)) continue;
            PersistentQuestManager.UpdateItemCount(qid,item.id, displayItem.QuantityFound);
        }
        
    }

    private void QuestItemDec_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button but) return;
        var displayItem = but.DataContext as QuestItem?? default;

        var item = ItemsData.ItemsDict[displayItem.Id];

        displayItem.QuantityFound = displayItem.QuantityFound <= 1 ? 0 : displayItem.QuantityFound - 1;
        
        
        foreach (var qid in item.quest_ids)
        {
            if (!PersistentQuestManager.Has(qid)) continue;
            PersistentQuestManager.UpdateItemCount(qid,item.id, displayItem.QuantityFound);
        }
    }

    private void QuestItemToggle_OnChecked(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleButton toggleButton) return;
        var item = toggleButton.DataContext as QuestItem?? default;
        itemsData.SelectedItems[item.Id] = ItemsData.ItemsDict[item.Id];
        PersistentItemManager.SetSelected(item.Id, true);
        LootConfigManager.Compile(itemsData, questsData);
    }

    private void QuestItemToggle_OnUnchecked(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleButton toggleButton) return;
        var item = toggleButton.DataContext as QuestItem?? default;
        itemsData.SelectedItems.Remove(item.Id);
        PersistentItemManager.SetSelected(item.Id, false);
        LootConfigManager.Compile(itemsData, questsData);
    }

    private void ChangePathButton_Click(object sender, RoutedEventArgs e)
    {
        string newPath = txtOutputPath.Text;
    
        if (LootConfigManager.IsValidPath(newPath))
        {
            if (Directory.Exists(newPath) || LootConfigManager.TryCreateDirectory(newPath))
            {
                LootConfigManager.ChangeOutputPath(newPath);
                txtFeedback.Text = "Path changed successfully.";
                txtFeedback.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                txtFeedback.Text = "Directory does not exist and could not be created.";
            }
        }
        else
        {
            txtFeedback.Text = "Invalid path entered. Please enter a valid path.";
        }
    }

    private bool _use_dark_mode = false;
    private void DarkModeToggle_Click(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("In DarkModeToggle");
        var dict = new ResourceDictionary();

        if (!_use_dark_mode)
        {
            Console.WriteLine("Setting Dark Mode");

            dict.Source = new Uri("pack://application:,,,/LootWizard;component/DarkTheme.xaml");
        }
        else
        {
            Console.WriteLine("Setting Light Mode");

            dict.Source = new Uri("pack://application:,,,/LootWizard;component/LightTheme.xaml");
        }
        
        Console.WriteLine("Setting theme");

        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(dict);
        Console.WriteLine("Done");

        
        _use_dark_mode = !_use_dark_mode;
    }

    
    
}