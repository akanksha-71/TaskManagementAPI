using TaskManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        Task<List<TodoTask>> GetTasks(string status, DateTime? dueDate, int pageNumber, int pageSize);
        Task<TodoTask> GetTaskById(int id);
        Task<TodoTask> CreateTask(TodoTask task);
        Task<TodoTask> UpdateTask(int id, TodoTask task);
        Task<bool> DeleteTask(int id);
    }
}
