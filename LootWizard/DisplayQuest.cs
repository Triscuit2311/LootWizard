namespace LootWizard;

public class DisplayQuest
{
    public Quest quest;

    public DisplayQuest(Quest quest)
    {
        this.quest = quest;
        
        
        traderName = quest.trader_name[0].ToString().ToUpper() + quest.trader_name.Substring(1);
        questName = quest.name;
    }
    
    public string traderName;
    public string questName;
}