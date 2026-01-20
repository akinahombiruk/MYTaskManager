namespace MYTaskManager.Models.ViewModel
{
    public class DashBoardViewModel
    {
       
            public int TotalTasks { get; set; }
            public int PendingTasks { get; set; }
            public int CompletedTasks { get; set; }
            public int OverdueTasks { get; set; }
            public int TotalProjects { get; set; }
            public int ActiveProjects { get; set; }
            public List<TaskViewModel> UpcomingTasks { get; set; } = new List<TaskViewModel>();
            public List<ActivityViewModel> RecentActivities { get; set; } = new List<ActivityViewModel>();
        

    }
}
