using System;
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
            Debug.WriteLine("Searchbox text changed");
            string searchTerm = SearchBox.Text;
            SearchItems(searchTerm);
        }
        
        public void SearchItems(string searchTerm)
        {
            var query = searchTerm.ToLower();
            itemsData.SearchResults.Clear();
            foreach (var item in itemsData.SearchEntries)
            {
                if ( Fuzz.WeightedRatio(query, item.short_name) > 80 || Fuzz.WeightedRatio(query, item.full_name) > 80)
                {
                    itemsData.SearchResults.Add(new DisplayItem(itemsData.ItemsDict[item.id], PersistentItemManager.Get(item.id)));
                }
            }
        }

        private void Image_OnFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Console.WriteLine("Failed to load image: " + e.ErrorException.Message);
        }
        
    }
}