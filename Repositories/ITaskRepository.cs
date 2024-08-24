using Task = task_api.Models.Task;

namespace task_api.Repositories;

public interface ITaskRepository
{
    IEnumerable<Task> GetAllTasks();
    Task GetTaskById(int taskId);
    Task CreateTask(Task newTask);
    Task UpdateTask(Task newTask);
    void DeleteTaskById(int taskId);
}