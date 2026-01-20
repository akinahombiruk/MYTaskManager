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
    public class TaskAttachmentsController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public TaskAttachmentsController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: TaskAttachments
        public async Task<IActionResult> Index()
        {
            var taskManagerDbContext = _context.TaskAttachments.Include(t => t.Task).Include(t => t.UploadedByNavigation);
            return View(await taskManagerDbContext.ToListAsync());
        }

        // GET: TaskAttachments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskAttachment = await _context.TaskAttachments
                .Include(t => t.Task)
                .Include(t => t.UploadedByNavigation)
                .FirstOrDefaultAsync(m => m.AttachmentId == id);
            if (taskAttachment == null)
            {
                return NotFound();
            }

            return View(taskAttachment);
        }

        // GET: TaskAttachments/Create
        public IActionResult Create()
        {
            ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId");
            ViewData["UploadedBy"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: TaskAttachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttachmentId,TaskId,FileName,FilePath,UploadedBy,UploadedAt")] TaskAttachment taskAttachment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskAttachment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId", taskAttachment.TaskId);
            ViewData["UploadedBy"] = new SelectList(_context.Users, "UserId", "UserId", taskAttachment.UploadedBy);
            return View(taskAttachment);
        }

        // GET: TaskAttachments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskAttachment = await _context.TaskAttachments.FindAsync(id);
            if (taskAttachment == null)
            {
                return NotFound();
            }
            ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId", taskAttachment.TaskId);
            ViewData["UploadedBy"] = new SelectList(_context.Users, "UserId", "UserId", taskAttachment.UploadedBy);
            return View(taskAttachment);
        }

        // POST: TaskAttachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttachmentId,TaskId,FileName,FilePath,UploadedBy,UploadedAt")] TaskAttachment taskAttachment)
        {
            if (id != taskAttachment.AttachmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskAttachment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskAttachmentExists(taskAttachment.AttachmentId))
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
            ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "TaskId", taskAttachment.TaskId);
            ViewData["UploadedBy"] = new SelectList(_context.Users, "UserId", "UserId", taskAttachment.UploadedBy);
            return View(taskAttachment);
        }

        // GET: TaskAttachments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskAttachment = await _context.TaskAttachments
                .Include(t => t.Task)
                .Include(t => t.UploadedByNavigation)
                .FirstOrDefaultAsync(m => m.AttachmentId == id);
            if (taskAttachment == null)
            {
                return NotFound();
            }

            return View(taskAttachment);
        }

        // POST: TaskAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskAttachment = await _context.TaskAttachments.FindAsync(id);
            if (taskAttachment != null)
            {
                _context.TaskAttachments.Remove(taskAttachment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskAttachmentExists(int id)
        {
            return _context.TaskAttachments.Any(e => e.AttachmentId == id);
        }
    }
}
