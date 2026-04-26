public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; }

    public bool IsDone { get; set; }

    public TaskItem(string title)
    {
        Title = title;
        IsDone = false;
    }
}