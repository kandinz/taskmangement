using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement_API.Models;
using Task = TaskManagement_API.Models.Task;

namespace TaskManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly TaskManagementDbContext taskManagementDbContext;
        public TasksController(TaskManagementDbContext taskManagementDbContext)
        {
            this.taskManagementDbContext = taskManagementDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await taskManagementDbContext.Tasks.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetTasksByUser(string email)
        {
            var user = await taskManagementDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return null;
            var result = await taskManagementDbContext.Tasks.ToListAsync();
            result = result.Where(x => x.CreateUser == user.Id).ToList();
            if (result == null) return NotFound("Task not found");
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await taskManagementDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) return NotFound("Task not found");
            return Ok(result);
        }

        //[HttpPost]
        //public IActionResult AddTask([FromBody] Task task)
        //{
        //    task.Id = new Guid();
        //    taskManagementDbContext.Add(task);
        //    taskManagementDbContext.SaveChanges();
        //    return CreatedAtAction(nameof(GetTaskById), task.Name);
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] Task model)
        {
            var task = await taskManagementDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (task == null)
            {
                task = new Task()
                {
                    Id = Guid.NewGuid(),
                    Content = model.Content,
                    Name = model.Name,
                    UserAssign = null,
                    IsComplete = false,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    CreateUser = model.CreateUser,
                };
                taskManagementDbContext.Add(task);
            }
            else
            {
                task.Content = model.Content;
                task.Name = model.Name;
                task.IsComplete = model.IsComplete;
                task.ModifyDate = DateTime.Now;
                task.ModifyUser = Guid.NewGuid();
            }

            taskManagementDbContext.SaveChanges();
            return Ok(task);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromBody] Guid id)
        {
            var task = await taskManagementDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null) return NotFound("Task not found");

            task.IsDelete = true;

            taskManagementDbContext.SaveChanges();
            return Ok(task);
        }
    }
}
