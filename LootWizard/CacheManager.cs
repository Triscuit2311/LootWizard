using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using ImageMagick;

namespace LootWizard;

public class CacheManager : IDisposable
{
    public static async Task Update( QuestsData questsData, ItemsData itemsData)
    {
        DateTime lastFetchTime = GetLastFetchTime();
        bool isCacheValid = DateTime.Now - lastFetchTime < CacheValidityDuration;

        string itemsJson, questsJson;

        if (!File.Exists(ItemsCacheFile) || !File.Exists(QuestsCacheFile) || !isCacheValid)
        {
            Console.WriteLine("Cache expired or invalid, hitting API for data");
            using (var httpClient = new HttpClient())
            {
                itemsJson = await FetchData(httpClient, items_query);
                File.WriteAllText(ItemsCacheFile, itemsJson);

                questsJson = await FetchData(httpClient, quests_query);
                File.WriteAllText(QuestsCacheFile, questsJson);
            }

            File.WriteAllText(LastFetchFile, DateTime.Now.ToString());
        }
        else
        {
            Console.WriteLine("Cache valid, using cached data");
            itemsJson = File.ReadAllText(ItemsCacheFile);
            questsJson = File.ReadAllText(QuestsCacheFile);
        }


        Console.WriteLine("Populating Quest Objects");
        var quests = CreateTasksFromJson(questsJson);

        questsData.QuestList = new List<Quest>(quests);
        
        Console.WriteLine("Populating Item Objects");
        var items = CreateItemsFromJson(itemsJson);
        
        PersistentItemManager.LoadUpdate();

        Console.WriteLine("Checking Image Cache..");
        int missing_imgs = 0;

        var default_color = Colors.Aqua;
        
        
        foreach (var item in items)
        {
            // add persistant data if needed
            PersistentItemManager.AddDoNotUpdate(item.id,
                new PersistentItemData(item.id, default_color , false));

            // Updating items dict
            itemsData.ItemsDict.Add(item.id,item);
            
            itemsData.SearchResults.Add(new DisplayItem(item,PersistentItemManager.Get(item.id)));
            
            // updating item search
            itemsData.SearchEntries.Add(new ItemsData.SearchEntry(
                item.searchable_short,
                item.searchable_full,
                item.id));
            
            if (!CheckImageCache(item))
            {
                missing_imgs++;
            }
        }
        
        Console.WriteLine($"Missing images: {missing_imgs}/{items.Count}");

        if (missing_imgs > 0)
        {
            Console.WriteLine("Downloading missing images, this could take a few minutes.");
            using (var httpClient = new HttpClient())
            {
                foreach (var item in items)
                {
                    await CacheItemImage(httpClient, item);
                }
            }

            Console.WriteLine("Images cached.");
        }
    }

    private const string ItemsCacheFile = "items_cache.json";
    private const string QuestsCacheFile = "quests_cache.json";
    private const string LastFetchFile = "last_fetch_time.txt";
    private static TimeSpan CacheValidityDuration = TimeSpan.FromHours(24);

    private static async Task CacheItemImage(HttpClient httpClient, Item item)
    {
        string imgDirectory = "img";
        string imgPath = Path.Combine(imgDirectory, $"{item.id}-icon.jpeg");

        if (!File.Exists(imgPath))
        {
            if (!Directory.Exists(imgDirectory))
            {
                Directory.CreateDirectory(imgDirectory);
            }

            byte[] imageData = await httpClient.GetByteArrayAsync(item.icon_link);

            // Convert WebP to JPEG
            using (var webpImage = new MagickImage(imageData))
            {
                webpImage.Format = MagickFormat.Jpeg;
                webpImage.Write(imgPath); // This saves the image in JPEG format
            }
        }
    }

    private static bool CheckImageCache(Item item)
    {
        string imgDirectory = "img";
        string imgPath = Path.Combine(imgDirectory, $"{item.id}-icon.jpeg");

        return File.Exists(imgPath);
    }

    static Dictionary<string, string> items_query = new Dictionary<string, string>()
    {
        {
            "query", """
                     {
                       items {
                           id
                           name
                           shortName
                           iconLink
                           avg24hPrice
                           width
                           height
                         usedInTasks{
                           id
                         }
                         types
                         properties{
                           __typename
                         }
                       }
                     }
                     """
        }
    };

    static Dictionary<string, string> quests_query = new Dictionary<string, string>()
    {
        {
            "query", """
                     {tasks{
                       id
                       name
                       trader{
                         name
                       }
                       objectives{
                         type
                         ...on TaskObjectiveItem{
                     			item{
                             id
                           }
                           count
                         }
                       }
                     }}
                     """
        }
    };


    public static List<Quest> CreateTasksFromJson(string json)
    {
        List<Quest> quests = new List<Quest>();

        using (JsonDocument doc = JsonDocument.Parse(json))
        {
            if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement) &&
                dataElement.TryGetProperty("tasks", out JsonElement tasksArray))
            {
                foreach (JsonElement taskElement in tasksArray.EnumerateArray())
                {
                    try
                    {
                        if (taskElement.TryGetProperty("objectives", out JsonElement objectives))
                        {
                            if (objectives.EnumerateArray().Any(obj =>
                                    obj.TryGetProperty("type", out JsonElement type) &&
                                    type.GetString() == "giveItem"))
                            {
                                Quest task = new Quest(taskElement);
                                quests.Add(task);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            else
            {
                // Handle the case where the necessary properties are not found
                Console.WriteLine("The required properties ('data' and 'tasks') are not present in the JSON.");
            }
        }


        return quests;
    }

    public static List<Item> CreateItemsFromJson(string json)
    {
        List<Item> items = new List<Item>();

        using (JsonDocument doc = JsonDocument.Parse(json))
        {
            // Check if the root element has the 'data' property and it contains 'items'
            if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement) &&
                dataElement.TryGetProperty("items", out JsonElement itemsArray))
            {
                foreach (JsonElement itemElement in itemsArray.EnumerateArray())
                {
                    try
                    {
                        items.Add(new Item(itemElement));
                    }
                    catch (Exception ex)
                    {
                        // Handle other potential exceptions
                        //Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }
                }
            }
            else
            {
                // Handle the case where the necessary properties are not found
                Console.WriteLine("The required item properties are not present in the JSON.");
            }
        }

        return items;
    }


    private static async Task<string> FetchData(HttpClient httpClient, Dictionary<string, string> query)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", query);
        return await httpResponse.Content.ReadAsStringAsync();
    }

    private static DateTime GetLastFetchTime()
    {
        if (File.Exists(LastFetchFile))
        {
            string lastFetchTimeString = File.ReadAllText(LastFetchFile);
            if (DateTime.TryParse(lastFetchTimeString, out DateTime lastFetchTime))
            {
                return lastFetchTime;
            }
        }

        return DateTime.MinValue;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}