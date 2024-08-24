using task_api.Migrations;
using Task = task_api.Models.Task;

namespace task_api.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Task> GetAllTasks()
    {
        return _context.Task.ToList();
    }

    public Task GetTaskById(int taskId)
    {
        return _context.Task.SingleOrDefault(c => c.TaskId == taskId);
    }

    public Task CreateTask(Task newTask)
    {
        _context.Task.Add(newTask);
        _context.SaveChanges();
        return newTask;
    }

    public Task UpdateTask(Task newTask)
    {
        var originalTask = _context.Task.Find(newTask.TaskId);
        if (originalTask != null)
        {
            originalTask.Title = newTask.Title;
            originalTask.Completed = newTask.Completed;
            _context.SaveChanges();
        }
        return originalTask;
    }

    public void DeleteTaskById(int taskId)
    {
        var task = _context.Task.Find(taskId);
        if (task != null)
        {
            _context.Task.Remove(task);
            _context.SaveChanges();
        }
    }
}