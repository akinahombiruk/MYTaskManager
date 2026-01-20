using System;

namespace MYTaskManager.Models.ViewModel
{
    public static class DashboardHelper
    {
        public static string GetPriorityBadgeClass(string priority)
        {
            if (string.IsNullOrEmpty(priority)) return "bg-secondary";

            return priority.ToLower() switch
            {
                "urgent" => "bg-danger",
                "high" => "bg-warning",
                "medium" => "bg-info",
                "low" => "bg-secondary",
                _ => "bg-secondary"
            };
        }

        public static string GetStatusBadgeClass(string status)
        {
            if (string.IsNullOrEmpty(status)) return "bg-secondary";

            return status.ToLower() switch
            {
                "completed" => "bg-success",
                "in progress" => "bg-primary",
                "pending" => "bg-warning",
                "on hold" => "bg-secondary",
                "cancelled" => "bg-danger",
                _ => "bg-secondary"
            };
        }

        public static string GetDueDateBadgeClass(DateOnly? dueDate)
        {
            if (!dueDate.HasValue) return "bg-secondary";

            var daysUntilDue = (dueDate.Value.ToDateTime(TimeOnly.MinValue) - DateTime.Today).Days;
            return daysUntilDue switch
            {
                < 0 => "bg-danger",
                0 => "bg-warning",
                1 => "bg-info",
                _ => "bg-secondary"
            };
        }

        public static string GetDueDateText(DateOnly? dueDate)
        {
            if (!dueDate.HasValue) return "No due date";

            var daysUntilDue = (dueDate.Value.ToDateTime(TimeOnly.MinValue) - DateTime.Today).Days;
            return daysUntilDue switch
            {
                < 0 => "Overdue",
                0 => "Due Today",
                1 => "Due Tomorrow",
                _ => $"Due in {daysUntilDue} days"
            };
        }
    }
}