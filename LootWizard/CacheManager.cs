using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
//using ImageMagick;

//using ImageMagick;
namespace LootWizard;

public class CacheManager : IDisposable
{
    
    // private static async Task CacheItemImage(HttpClient httpClient, Item item)
    // {
    //     string imgDirectory = "img";
    //     string imgPath = Path.Combine(imgDirectory, $"{item.id}-icon.jpeg");
    //
    //     if (!File.Exists(imgPath))
    //     {
    //         if (!Directory.Exists(imgDirectory))
    //         {
    //             Directory.CreateDirectory(imgDirectory);
    //         }
    //
    //         byte[] imageData = await httpClient.GetByteArrayAsync(item.icon_link);
    //
    //         // Convert WebP to JPEG
    //         using (var webpImage = new MagickImage(imageData))
    //         {
    //             webpImage.Format = MagickFormat.Jpeg;
    //             webpImage.Write(imgPath); // This saves the image in JPEG format
    //         }
    //     }
    // }
    //
    
    private const string ItemsCacheFile = "items_cache.json";
    private const string QuestsCacheFile = "quests_cache.json";
    private const string LastFetchFile = "last_fetch_time.txt";
    private static readonly TimeSpan CacheValidityDuration = TimeSpan.FromHours(24);

    private static readonly Dictionary<string, string> items_query = new()
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

    private static readonly Dictionary<string, string> quests_query = new()
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

    public void Dispose()
    {
    }

    public static async Task Update(QuestsData questsData, ItemsData itemsData)
    {
        var lastFetchTime = GetLastFetchTime();
        var isCacheValid = DateTime.Now - lastFetchTime < CacheValidityDuration;

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


        Console.WriteLine("Populating Item Objects");
        var items = CreateItemsFromJson(itemsJson);

        PersistentItemManager.LoadUpdate();

        var defaultColor = Colors.Aqua;


        foreach (var item in items)
        {
            // add persistant data if needed
            PersistentItemManager.AddDoNotUpdate(item.id,
                new PersistentItemData(item.id, defaultColor, false));
            if (PersistentItemManager.Get(item.id).selected) itemsData.SelectedItems[item.id] = item;

            // Updating items dict
            ItemsData.ItemsDict.Add(item.id, item);

            // Init list for display
            itemsData.SearchResults.Add(new DisplayItem(item, PersistentItemManager.Get(item.id)));

            // updating item search
            itemsData.SearchEntries.Add(new ItemsData.SearchEntry(
                item.searchable_short,
                item.searchable_full,
                item.id));
        }

        
      //  Console.WriteLine("STARTING IMAGE CACHE");
     //   using (var httpClient = new HttpClient())
      //  //{
      //      foreach (var item in items)
      //      {
      //          await CacheItemImage(httpClient, item);
     //       }
     //   }
        



        Console.WriteLine("Populating Quest Objects");
        var quests = CreateTasksFromJson(questsJson);

        QuestsData.QuestList = new Dictionary<string, Quest>();

        PersistentQuestManager.LoadUpdate();

        foreach (var quest in quests)
        {
            QuestsData.QuestList.Add(quest.id, quest);

            // add persistant data if needed
            PersistentQuestManager.AddDoNotUpdate(quest.id,
                new PersistentQuestData(quest.id, false, new Dictionary<string, int>()));
            questsData.DisplayQuests.Add(new DisplayQuest(quest, PersistentQuestManager.Get(quest.id)));
        }

        questsData.BuildQuestPools();
    }

    public static List<Quest> CreateTasksFromJson(string json)
    {
        var quests = new List<Quest>();

        using (var doc = JsonDocument.Parse(json))
        {
            if (doc.RootElement.TryGetProperty("data", out var dataElement) &&
                dataElement.TryGetProperty("tasks", out var tasksArray))
                foreach (var taskElement in tasksArray.EnumerateArray())
                    try
                    {
                        if (taskElement.TryGetProperty("objectives", out var objectives))
                            if (objectives.EnumerateArray().Any(obj =>
                                    obj.TryGetProperty("type", out var type) &&
                                    type.GetString() == "giveItem"))
                            {
                                var task = new Quest(taskElement);
                                quests.Add(task);
                            }
                    }
                    catch (Exception e)
                    {
                    }
            else
                // Handle the case where the necessary properties are not found
                Console.WriteLine("The required properties ('data' and 'tasks') are not present in the JSON.");
        }

        return quests;
    }

    public static List<Item> CreateItemsFromJson(string json)
    {
        var items = new List<Item>();
        const int ContextLength = 50; // Length of JSON context to display in case of an error

        using (var doc = JsonDocument.Parse(json))
        {
            if (doc.RootElement.TryGetProperty("data", out var dataElement) &&
                dataElement.TryGetProperty("items", out var itemsArray))
            {
                foreach (var itemElement in itemsArray.EnumerateArray())
                {
                    try
                    {
                        items.Add(new Item(itemElement));
                    }
                    catch (Exception ex)
                    {
                        // Extract the JSON snippet around the error location
                        string jsonContext = ExtractJsonContext(itemElement.GetRawText(), ContextLength);
                        Console.WriteLine($"Error parsing item: {ex.Message}");
                        Console.WriteLine($"JSON context: {jsonContext}");
                    }
                }
            }
            else
            {
                Console.WriteLine("The required item properties are not present in the JSON.");
            }
        }

        return items;
    }

    private static string ExtractJsonContext(string jsonSnippet, int contextLength)
    {
        // Ensure the snippet length is within the bounds of the actual JSON string
        return jsonSnippet.Length <= contextLength * 2
            ? jsonSnippet
            : jsonSnippet.Substring(0, contextLength) + "..." + jsonSnippet.Substring(jsonSnippet.Length - contextLength);
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
            var lastFetchTimeString = File.ReadAllText(LastFetchFile);
            if (DateTime.TryParse(lastFetchTimeString, out var lastFetchTime)) return lastFetchTime;
        }

        return DateTime.MinValue;
    }
}