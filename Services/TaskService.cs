using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoTask>> GetTasks(string status, DateTime? dueDate, int pageNumber, int pageSize)
        {
            IQueryable<TodoTask> query = _context.Tasks;

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (dueDate.HasValue)
            {
                query = query.Where(t => t.DueDate <= dueDate.Value);
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<TodoTask> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TodoTask> CreateTask(TodoTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TodoTask> UpdateTask(int id, TodoTask task)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null) return null;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate;

            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
