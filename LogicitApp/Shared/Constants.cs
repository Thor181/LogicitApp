using LogicitApp.Shared.Extensions;

namespace LogicitApp.Shared
{
    public class Constants
    {
        public static List<string> Statuses { get; set; } = new List<string>()
        {
            Shared.Statuses.Completed.AsString(), 
            Shared.Statuses.InProcess.AsString(), 
            Shared.Statuses.AwaitComplete.AsString()
        };
    }
}
