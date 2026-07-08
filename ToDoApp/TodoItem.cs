// ToDoApp\TodoItem.cs (no code changes required for ENC0097)
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ToDoApp
{
    internal class TodoItem
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string TaskDescription { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public PriorityLevel Priority { get; set; }

    }
    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
