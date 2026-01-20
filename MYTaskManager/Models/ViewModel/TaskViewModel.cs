namespace MYTaskManager.Models.ViewModel
{
    public class TaskViewModel
    {

        
            public int TaskId { get; set; }
            public string Title { get; set; } = string.Empty;
            public DateOnly? DueDate { get; set; }
            public string PriorityName { get; set; } = string.Empty;
            public string ProjectName { get; set; } = string.Empty;
            public string StatusName { get; set; } = string.Empty;
            public string AssignedToName { get; set; } = string.Empty;
      

    }
}
