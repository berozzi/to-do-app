using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Services
{
    interface ITaskExport
    {
        bool ExportTasks(IEnumerable<TodoItem> tasks);
        List<TodoItem> GetTasksFromFile();
    }
}
/*
 Szczegóły implementacji eksportu danych:
- zbudowanie prostej dokumentacji
- testy jednostkowe dla zapisu i odczytu danych

Logika wygląda następująco:
- wpisujesz taska
- możesz go edytować i usunąć
- program sam z siebie po kliknięciu zapisz eksportuje liste zadan do pliku JSON (tasks.json)
- odświeża się po każdej akcji (dodanie, edycja, usunięcie)
- zadania same są czyszczone jeśli wypada poniedziałek (IsMonday czy coś podobnego)

Każdy task ma:
- ID (niewidoczne dla użytkownika)
- nazwe
- opis
- datę zakonczenia ustawioną na najbliższy poniedziałek
- priorytet (niski, średni, wysoki)
 */
