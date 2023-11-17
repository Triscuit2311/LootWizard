using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FuzzySharp;


namespace WizardTestConsole
{
    
    internal class Program
    {
        
        public static List<Item> SearchItems(string searchTerm, ItemsData itemsData)
        {
            var query = searchTerm.ToLower();
            List<Item> results = new List<Item>();
            foreach (var item in itemsData.SearchEntries)
            {
                if ( Fuzz.WeightedRatio(query, item.short_name) > 80 || Fuzz.WeightedRatio(query, item.full_name) > 80)
                {
                    results.Add(itemsData.ItemsDict[item.id]);
                }
            }
            return results;
        }
        
        public static async Task Main(string[] args)
        {
            QuestsData questsData = new QuestsData();
            ItemsData itemsData = new ItemsData();
            
            using (CacheManager cacheManager = new CacheManager())
            {
                await CacheManager.Update(questsData, itemsData);

            }
            
            Console.WriteLine($"Loaded: [Items: {itemsData.ItemsList.Count}(L) ~ {itemsData.ItemsDict.Count}(D) | Quests: {questsData.QuestList.Count}]");
            

            while (true)
            {
                var q = Console.ReadLine();
                if (q.Equals("exit"))
                {
                    break;
                    
                }
                var res = SearchItems(q, itemsData);
                
                foreach (var item in res)
                {
                    Console.WriteLine($"\t {item.short_name} ({item.name})");
                }
            }
            
        }
        

    }
}
