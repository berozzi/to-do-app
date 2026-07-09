using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ToDoApp.Services
{
    class TaskExport : ITaskExport
    {
        readonly string filePath = "tasks.json";
        public TaskExport() { }
        
        // exporting tasks to JSON
        public bool ExportTasks(IEnumerable<TodoItem> tasks)
        {
            ArgumentNullException.ThrowIfNull(tasks);

            try
            {
                string jsonString = JsonSerializer.Serialize(tasks);
                File.WriteAllText(filePath, jsonString);
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Brak uprawnień do zapisu pliku: {ex.Message}");
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Folder docelowy nie istnieje: {ex.Message}");
                return false;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Błąd zapisu pliku (może być otwarty w innym programie?): {ex.Message}");
                return false;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Błąd podczas konwersji danych do JSON: {ex.Message}");
                return false;
            }
        }
        // getting text from JSON file
        public List<TodoItem> GetTasksFromFile()
        {
            List<TodoItem>? tasks = JsonSerializer.Deserialize<List<TodoItem>>(File.ReadAllText(filePath));
            foreach (var task in tasks ?? [])
            {
                if (IsCompleted(task))
                {
                    Console.WriteLine($"Zadanie '{task.TaskName}' jest przeterminowane. Usuwamy.");
                    tasks?.Remove(task);
                }
            }
            return tasks ?? []; // return an empty list if tasks is null
        }

        bool IsCompleted(TodoItem task)
        {
            return task.DueDate < DateTime.Now;
        }
    }
}
