using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LootWizard;

public class QuestsData
{
    
    public static Dictionary<string, Quest> QuestList = new();
    public List<Quest> SelectedQuests = new();
    public ObservableCollection<DisplayQuest> DisplayQuests = new();

    public ObservableCollection<DisplayQuest> PraporQuests = new();
    public ObservableCollection<DisplayQuest> TherapistQuests = new();
    public ObservableCollection<DisplayQuest> SkierQuests = new();
    public ObservableCollection<DisplayQuest> FenceQuests = new();
    public ObservableCollection<DisplayQuest> PeacekeeperQuests = new();
    public ObservableCollection<DisplayQuest> MechanicQuests = new();
    public ObservableCollection<DisplayQuest> RagmanQuests = new();
    public ObservableCollection<DisplayQuest> JaegerQuests = new();
    public ObservableCollection<DisplayQuest> LightkeeperQuests = new();

    public void BuildQuestPools()
    {
        foreach (var quest in DisplayQuests)
        {
            switch (quest.traderName)
            {
                case "Prapor":
                    PraporQuests.Add(quest);
                    break;
                case "Therapist":
                    TherapistQuests.Add(quest);
                    break;
                case "Skier":
                    SkierQuests.Add(quest);
                    break;
                case "Fence":
                    FenceQuests.Add(quest);
                    break;
                case "Peacekeeper":
                    PeacekeeperQuests.Add(quest);
                    break;
                case "Mechanic":
                    MechanicQuests.Add(quest);
                    break;
                case "Ragman":
                    RagmanQuests.Add(quest);
                    break;
                case "Jaeger":
                    JaegerQuests.Add(quest);
                    break;
                case "Lightkeeper":
                    LightkeeperQuests.Add(quest);
                    break;
                default:
                    break;
            }
        }
    }

}