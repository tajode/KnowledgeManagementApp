using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImplementingLikeButton.Data;
using ImplementingLikeButton.Models;

namespace ImplementingLikeButton.Controllers
{
    public class KnowledgesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KnowledgesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Knowledges
        public async Task<IActionResult> Index()
        {
              return _context.Knowledge_Dbset != null ? 
                          View(await _context.Knowledge_Dbset.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Knowledge_Dbset'  is null.");
        }

        // GET: Knowledges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Knowledge_Dbset == null)
            {
                return NotFound();
            }

            var knowledge = await _context.Knowledge_Dbset
                .FirstOrDefaultAsync(m => m.Id == id);

            var creationdate = knowledge?.DateofCreation;
            var articledate = "";

            articledate = "Created on: " + creationdate;
            ViewBag.ArticleDate = articledate;

            if (knowledge == null)
            {
                return NotFound();
            }

            return View(knowledge);
        }

        // GET: Knowledges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Knowledges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateofCreation,IPAddress,KnowledgeName,Description")] Knowledge knowledge)
        {
            if (ModelState.IsValid)
            {
                var datetime = DateTime.Now.ToUniversalTime();
                knowledge.DateofCreation = datetime;

                string clientIp;
                if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                else
                    clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                knowledge.IPAddress = clientIp;

                _context.Add(knowledge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knowledge);
        }

        // GET: Knowledges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Knowledge_Dbset == null)
            {
                return NotFound();
            }

            var knowledge = await _context.Knowledge_Dbset.FindAsync(id);
            if (knowledge == null)
            {
                return NotFound();
            }
            return View(knowledge);
        }

        // POST: Knowledges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateofCreation,IPAddress,KnowledgeName,Description")] Knowledge knowledge)
        {
            if (id != knowledge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var datetime = DateTime.Now.ToUniversalTime();
                    knowledge.DateofCreation = datetime;

                    string clientIp;
                    if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                        clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                    else
                        clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                    knowledge.IPAddress = clientIp;

                    _context.Update(knowledge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowledgeExists(knowledge.Id))
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
            return View(knowledge);
        }

        // GET: Knowledges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Knowledge_Dbset == null)
            {
                return NotFound();
            }

            var knowledge = await _context.Knowledge_Dbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowledge == null)
            {
                return NotFound();
            }

            return View(knowledge);
        }

        // POST: Knowledges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Knowledge_Dbset == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Knowledge_Dbset'  is null.");
            }
            var knowledge = await _context.Knowledge_Dbset.FindAsync(id);
            if (knowledge != null)
            {
                _context.Knowledge_Dbset.Remove(knowledge);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnowledgeExists(int id)
        {
          return (_context.Knowledge_Dbset?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
