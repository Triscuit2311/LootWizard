using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FuzzySharp;

namespace LootWizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        QuestsData questsData = new QuestsData();
        ItemsData itemsData = new ItemsData();
        
        
        
        private async Task LoadCache()
        {
            using (CacheManager cacheManager = new CacheManager())
            {
                await CacheManager.Update(questsData, itemsData);
            }
        }
        
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Run the async function
            await LoadCache();
        }
        public MainWindow()
        {
            InitializeComponent();

            PriceFilterType.SelectedIndex = 0;
            
            // Bind the source
            ItemsList.ItemsSource = itemsData.SearchResults;
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            PersistentItemManager.Save();
        }

        
        
        
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = SearchBox.Text;
            SearchItems(searchTerm, LootFilters.ActiveFilters);
        }
        
        public void SearchItems(string searchTerm, List<LootFilter> filters)
        {
            
            // if we delete all content, we do not filter
            if (searchTerm.Length < 1)
            {
                foreach (var entry in itemsData.SearchEntries)
                {
                    bool flag = true;
                    var item = itemsData.ItemsDict[entry.id];
                    foreach (var filter in filters)
                    {
                        if (!filter.MeetsCriteria(item))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        continue;
                    }
                    
                    itemsData.SearchResults.Add(new DisplayItem(itemsData.ItemsDict[item.id],
                        PersistentItemManager.Get(item.id)));
                }
                return;
            }
            
            var query = searchTerm.ToLower();
            itemsData.SearchResults.Clear();
            foreach (var entry in itemsData.SearchEntries)
            {
                bool flag = true;
                var item = itemsData.ItemsDict[entry.id];
                foreach (var filter in filters)
                {
                    if (!filter.MeetsCriteria(item))
                    {
                        flag = false;
                        break;
                    }
                }

                if (!flag)
                {
                    continue;
                }
                
                if ( Fuzz.WeightedRatio(query, entry.short_name) > 80 || Fuzz.WeightedRatio(query, entry.full_name) > 80)
                {
                    itemsData.SearchResults.Add(new DisplayItem(item, PersistentItemManager.Get(entry.id)));
                }
            }
        }
        

        private void Image_OnFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Console.WriteLine("Failed to load image: " + e.ErrorException.Message);
        }
        
        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            LootFilters.ActiveFilters.Clear();

            // Price or Price-per-slot filter
            if (int.TryParse(PriceFilterBox.Text, out int price))
            {
                if (PriceFilterType.SelectedIndex == 0) // Filter by Price
                {
                    LootFilters.ActiveFilters.Add(LootFilters.FilterByPrice(price));
                }
                else if (PriceFilterType.SelectedIndex == 1) // Filter by Price per Slot
                {
                    LootFilters.ActiveFilters.Add(LootFilters.FilterByPricePerSlot(price));
                }
            }

            // Favorites filter
            if (FavoritesFilterCheckBox.IsChecked == true)
            {
                LootFilters.ActiveFilters.Add(LootFilters.FilterByFavorites);
            }

            // Selected filter
            if (SelectedFilterCheckBox.IsChecked == true)
            {
                LootFilters.ActiveFilters.Add(LootFilters.FilterBySelected);
            }

            string searchTerm = SearchBox.Text;
            SearchItems(searchTerm, LootFilters.ActiveFilters);
            
            // Apply filters to your items data and update the UI accordingly
        }

        
    }
}