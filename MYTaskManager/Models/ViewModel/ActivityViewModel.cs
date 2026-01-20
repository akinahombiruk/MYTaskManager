namespace MYTaskManager.Models.ViewModel
{
    public class ActivityViewModel
    {
     
            public int TaskId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string ActivityType { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public DateTime Timestamp { get; set; }
            public string StatusName { get; set; } = string.Empty;
        
    }
}
