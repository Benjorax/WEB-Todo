using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TaskManager
{
    private List<TaskItem> tasks = new List<TaskItem>();
    private readonly string filePath = "tasks.json";

    public TaskManager()
    {
        LoadTasks();
    }

    public TaskItem? AddTask(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return null;

        var task = new TaskItem(title.Trim());

        tasks.Add(task);
        SaveTasks();

        return task;
    }    
public List<TaskItem> GetTasks() => tasks;

   public bool RemoveTask(Guid id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
            return false;

        tasks.Remove(task);
        SaveTasks();

        return true;
    }

    public bool ToggleTaskDone(Guid id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
            return false;

        task.IsDone = !task.IsDone;
        SaveTasks();

        return true;
    }

    public TaskItem? ToggleTaskDoneAndReturn(Guid id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
            return null;

        task.IsDone = !task.IsDone;
        SaveTasks();

        return task;
    }

    private void SaveTasks()
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    private void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            tasks = JsonSerializer.Deserialize<List<TaskItem>>(File.ReadAllText(filePath)) ?? new List<TaskItem>();
        }
    }
}