using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly TaskManager _manager;
    
    public List<TaskItem> Tasks { get; set; } = new();

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(TaskManager manager, ILogger<IndexModel> logger)
    {
        _manager = manager;
        _logger = logger;
    }
    public Guid? HighlightId { get; set; }

    public void OnGet(Guid? highlightId = null)
    {
        HighlightId = highlightId;
        Tasks = _manager.GetTasks();
    }

    // HTMX обработчики для плавного обновления
    public PartialViewResult OnPostAddTaskHtmx(string title)
    {
        var task = _manager.AddTask(title);

        if (task == null)
            return new PartialViewResult();

        return Partial("Shared/_TaskItemPartial", task);
    }

    public PartialViewResult OnPostToggleTaskHtmx(Guid id)
    {
        var task = _manager.ToggleTaskDoneAndReturn(id);

        if (task == null)
            return new PartialViewResult();

        return Partial("Shared/_TaskItemPartial", task);
    }

    public IActionResult OnPostRemoveTaskHtmx(Guid id)
    {
        _manager.RemoveTask(id);
        return StatusCode(200);
    }

    // Старые обработчики для обратной совместимости
    public TaskItem? ToggleTaskDoneAndReturn(Guid id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
            return null;

        task.IsDone = !task.IsDone;
        _manager.ToggleTaskDone(id);

        return task;
    }
    public IActionResult OnPostAddTask(string title)
    {
        var task = _manager.AddTask(title);

        if (task == null)
            return BadRequest();

        return RedirectToPage();
    }

    public IActionResult OnPostToggleTask(Guid id)
    {
        var task = _manager.ToggleTaskDoneAndReturn(id);

        if (task == null)
            return NotFound();

        return RedirectToPage();
    }

    public IActionResult OnPostRemoveTask(Guid id)
    {
        _manager.RemoveTask(id);

        return RedirectToPage();
    }
}