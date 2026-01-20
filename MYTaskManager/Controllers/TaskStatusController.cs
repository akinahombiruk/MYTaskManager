using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYTaskManager.Models;

namespace MYTaskManager.Controllers
{
    public class TaskStatusController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public TaskStatusController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: TaskStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskStatuses.ToListAsync());
        }

        // GET: TaskStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskStatus = await _context.TaskStatuses
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (taskStatus == null)
            {
                return NotFound();
            }

            return View(taskStatus);
        }

        // GET: TaskStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,StatusName")] MYTaskManager.Models.TaskStatus taskStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskStatus);
        }

        // GET: TaskStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskStatus = await _context.TaskStatuses.FindAsync(id);
            if (taskStatus == null)
            {
                return NotFound();
            }
            return View(taskStatus);
        }

        // POST: TaskStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,StatusName")] MYTaskManager.Models.TaskStatus taskStatus)
        {
            if (id != taskStatus.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskStatusExists(taskStatus.StatusId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskStatus);
        }

        // GET: TaskStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskStatus = await _context.TaskStatuses
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (taskStatus == null)
            {
                return NotFound();
            }

            return View(taskStatus);
        }

        // POST: TaskStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskStatus = await _context.TaskStatuses.FindAsync(id);
            if (taskStatus != null)
            {
                _context.TaskStatuses.Remove(taskStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskStatusExists(int id)
        {
            return _context.TaskStatuses.Any(e => e.StatusId == id);
        }
    }
}
