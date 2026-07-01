using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace ToDoApp
{
    internal class TaskDataView : INotifyPropertyChanged
    {
        public ObservableCollection<TodoItem> Items { get; } = new();

        private string _taskName;
        private string _taskDescription;
        private DateTime _dueDate;
        private PriorityLevel _priority;
        private bool _isCompleted;
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
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public TaskDataView()
        {
            AddCommand = new RelayCommand<object>(_ => AddTask());
            DeleteCommand = new RelayCommand<TodoItem>(DeleteTask);
            EditCommand = new RelayCommand<int>(id => EditTask(id, TaskName, TaskDescription, DueDate, Priority));
        }
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

        void DeleteTask(TodoItem item)
        {
            if (item != null && Items.Contains(item))
            {
                Items.Remove(item);
            }
        }
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
