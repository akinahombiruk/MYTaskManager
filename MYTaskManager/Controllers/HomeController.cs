using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MYTaskManager.Models;
using MYTaskManager.Models.ViewModel;

namespace MYTaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public HomeController(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get today's date as DateOnly for comparison
            var today = DateOnly.FromDateTime(DateTime.Today);
            var nextWeek = today.AddDays(7);

            var dashboardData = new DashBoardViewModel
            {
                // Task Statistics
                TotalTasks = await _context.Tasks.CountAsync(),
                PendingTasks = await _context.Tasks
                    .Include(t => t.Status)
                    .CountAsync(t => t.Status != null && t.Status.StatusName == "Pending"),
                CompletedTasks = await _context.Tasks
                    .Include(t => t.Status)
                    .CountAsync(t => t.Status != null && t.Status.StatusName == "Completed"),
                OverdueTasks = await _context.Tasks
                    .Include(t => t.Status)
                    .CountAsync(t => t.DueDate.HasValue &&
                                   t.DueDate < today &&
                                   t.Status != null && t.Status.StatusName != "Completed"),

                // Upcoming Tasks (due in next 7 days)
                UpcomingTasks = await _context.Tasks
                    .Include(t => t.Status)
                    .Include(t => t.Priority)
                    .Include(t => t.Project)
                    .Include(t => t.AssignedToNavigation)
                    .Where(t => t.DueDate.HasValue &&
                               t.DueDate >= today &&
                               t.DueDate <= nextWeek &&
                               t.Status != null && t.Status.StatusName != "Completed")
                    .OrderBy(t => t.DueDate)
                    .Take(5)
                    .Select(t => new TaskViewModel
                    {
                        TaskId = t.TaskId,
                        Title = t.Title ?? "No Title",
                        DueDate = t.DueDate,
                        PriorityName = t.Priority != null ? t.Priority.PriorityName : "Not Set",
                        ProjectName = t.Project != null ? t.Project.ProjectName : "No Project",
                        StatusName = t.Status != null ? t.Status.StatusName : "Not Set",
                        AssignedToName = t.AssignedToNavigation != null ? t.AssignedToNavigation.FullName : "Unassigned"
                    })
                    .ToListAsync(),

                // Recent Activities (recently created tasks)
                RecentActivities = await _context.Tasks
                    .Include(t => t.Status)
                    .Include(t => t.CreatedByNavigation)
                    .Where(t => t.CreatedAt.HasValue)
                    .OrderByDescending(t => t.CreatedAt)
                    .Take(5)
                    .Select(t => new ActivityViewModel
                    {
                        TaskId = t.TaskId,
                        Title = t.Title ?? "No Title",
                        ActivityType = "Created",
                        UserName = t.CreatedByNavigation != null ? t.CreatedByNavigation.FullName : "System",
                        Timestamp = t.CreatedAt.Value,
                        StatusName = t.Status != null ? t.Status.StatusName : "Not Set"
                    })
                    .ToListAsync(),

                // Project Statistics
                TotalProjects = await _context.Projects.CountAsync(),
                ActiveProjects = await _context.Projects
                    .CountAsync(p => p.EndDate == null || p.EndDate >= today)
            };

            return View(dashboardData);
        }

        public async Task<IActionResult> TodayTasks()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var tasks = await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Project)
                .Include(t => t.AssignedToNavigation)
                .Where(t => t.DueDate == today)
                .OrderBy(t => t.Priority != null ? t.Priority.PriorityId : 0)
                .ToListAsync();

            return View(tasks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}