using System.Linq;
using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public static class QuestCommand
    {
        [Command(commandName: "quest", description: "Opens a quest by its ID")]
        public static void QuestCommandProcess(string argument)
        {
            var questIds = argument.Split(',')
                .Select(id => int.TryParse(id.Trim(), out var result) ? result : (int?)null)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList(); 
            
            if (questIds.Count == 0)
            {
                Chat.Instance.AddText("Invalid Quest ID");
                return;
            }
            
            UIQuest.ShowQuests(questIds, questIds);
        }
    }
}