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
    public class TaskPrioritiesController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public TaskPrioritiesController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: TaskPriorities
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskPriorities.ToListAsync());
        }

        // GET: TaskPriorities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskPriority = await _context.TaskPriorities
                .FirstOrDefaultAsync(m => m.PriorityId == id);
            if (taskPriority == null)
            {
                return NotFound();
            }

            return View(taskPriority);
        }

        // GET: TaskPriorities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskPriorities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriorityId,PriorityName")] TaskPriority taskPriority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskPriority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskPriority);
        }

        // GET: TaskPriorities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskPriority = await _context.TaskPriorities.FindAsync(id);
            if (taskPriority == null)
            {
                return NotFound();
            }
            return View(taskPriority);
        }

        // POST: TaskPriorities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PriorityId,PriorityName")] TaskPriority taskPriority)
        {
            if (id != taskPriority.PriorityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskPriority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskPriorityExists(taskPriority.PriorityId))
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
            return View(taskPriority);
        }

        // GET: TaskPriorities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskPriority = await _context.TaskPriorities
                .FirstOrDefaultAsync(m => m.PriorityId == id);
            if (taskPriority == null)
            {
                return NotFound();
            }

            return View(taskPriority);
        }

        // POST: TaskPriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskPriority = await _context.TaskPriorities.FindAsync(id);
            if (taskPriority != null)
            {
                _context.TaskPriorities.Remove(taskPriority);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskPriorityExists(int id)
        {
            return _context.TaskPriorities.Any(e => e.PriorityId == id);
        }
    }
}
