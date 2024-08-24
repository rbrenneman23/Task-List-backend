using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using task_api.Repositories;
using Task = task_api.Models.Task;

namespace task_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private readonly ITaskRepository _taskRepository;

    public TaskController(ILogger<TaskController> logger, ITaskRepository repository)
    {
        _logger = logger;
        _taskRepository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Task>> GetTasks()
    {
        return Ok(_taskRepository.GetAllTasks());
    }

    [HttpGet]
    [Route("{taskId:int}")]
    public ActionResult<Task> GetTaskById(int taskId)
    {
        var task = _taskRepository.GetTaskById(taskId);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public ActionResult<Task> CreateTask(Task task)
    {
        if (!ModelState.IsValid || task == null)
        {
            return BadRequest();
        }
        var newTask = _taskRepository.CreateTask(task);
        return Created(nameof(GetTaskById), newTask);
    }

    [HttpPut]
    [Route("{taskId:int}")]
    public ActionResult<Task> UpdateTask(int taskId, Task task)
    {
        if (taskId != task.TaskId)
        {
            return BadRequest("Task ID mismatch.");
        }

        if (!ModelState.IsValid || task == null)
        {
            return BadRequest(ModelState);
        }

        var updatedTask = _taskRepository.UpdateTask(task);
        if (updatedTask == null)
        {
            return NotFound();
        }

        return Ok(updatedTask);
    }

    [HttpDelete]
    [Route("{taskId:int}")]
    public ActionResult DeleteTask(int taskId)
    {
        _taskRepository.DeleteTaskById(taskId);
        return NoContent();
    }
}