using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using ToDoApp.Services;

namespace ToDoApp
{
    internal class TaskDataView : INotifyPropertyChanged
    {
        readonly ITaskExport _taskExport;
        public ObservableCollection<TodoItem> Items { get; set; } 
        private string _taskName = string.Empty;
        private string _taskDescription = string.Empty;
        private DateTime _dueDate;
        private PriorityLevel _priority;
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ExportCommand { get; }

        TodoItem _selectedTask;

        public TaskDataView(ITaskExport taskExport)
        {
            _taskExport = taskExport;
            Items = new ObservableCollection<TodoItem>(_taskExport.GetTasksFromFile());
            AddCommand = new RelayCommand<object>(_ => AddTask());
            DeleteCommand = new RelayCommand<TodoItem>(DeleteTask);
            EditCommand = new RelayCommand<int>(id => EditTask(id, TaskName, TaskDescription, DueDate, Priority));
            ExportCommand = new RelayCommand<object>(_ => _taskExport.ExportTasks(Items));
        }
        // Method to add a new task to the list
        void AddTask()
        {
            var newItem = new TodoItem
            {
                Id = Items.Count + 1,
                TaskName = TaskName,
                TaskDescription = TaskDescription,
                DueDate = DateTime.Now.AddDays(7), // Example due date
                Priority = Priority
            };
            Items.Add(newItem);
            Console.WriteLine("Dodano item o nazwie: " + newItem.TaskName);
        }
        // Deleting task from the list
        void DeleteTask(TodoItem item)
        {
            if (item != null && Items.Contains(item))
            {
                Items.Remove(item);
            }
        }
        // Editing task in the list based on the provided id and new values
        void EditTask(int id, string taskName, string taskDescription, DateTime dueDate, PriorityLevel priority)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.TaskName = TaskName;
                item.TaskDescription = TaskDescription;
                item.DueDate = DueDate;
                item.Priority = Priority;
                OnPropertyChanged(nameof(Items));
            }
        }

        // Properties for binding (getters and setters)
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                if (_taskName != value)
                {
                    _taskName = value;
                    OnPropertyChanged(nameof(TaskName));
                }
            }
        }
        public string TaskDescription
        {
            get { return _taskDescription; }
            set
            {
                if (_taskDescription != value)
                {
                    _taskDescription = value;
                    OnPropertyChanged(nameof(TaskDescription));
                }
            }
        }
        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                if (_dueDate != value)
                {
                    _dueDate = value;
                    OnPropertyChanged(nameof(DueDate));
                }
            }
        }
        public PriorityLevel Priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }
        public TodoItem SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
