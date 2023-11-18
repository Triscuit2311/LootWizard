using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            SearchBox.Text = "";
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

        private void SearchItems(string searchTerm, List<LootFilter> filters)
        {
            itemsData.SearchResults.Clear();
            var query = searchTerm.ToLower();
            foreach (var entry in itemsData.SearchEntries)
            {
                var item = itemsData.ItemsDict[entry.id];

                var flag = filters.All(filter => filter.MeetsCriteria(item));

                if (!flag)
                {
                    continue;
                }

                if (searchTerm.Length < 1)
                {
                    itemsData.SearchResults.Add(new DisplayItem(item, PersistentItemManager.Get(entry.id)));
                    continue;
                }

                if (Fuzz.WeightedRatio(query, entry.short_name) > 80 || Fuzz.WeightedRatio(query, entry.full_name) > 80)
                {
                    itemsData.SearchResults.Add(new DisplayItem(item, PersistentItemManager.Get(entry.id)));
                }
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


        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            LootConfigManager.Compile(itemsData, questsData);
        }
    }
}