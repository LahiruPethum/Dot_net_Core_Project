 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class QnAsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QnAsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QnAs
        public async Task<IActionResult> Index() 
        {
            return View(await _context.QnA.ToListAsync());
        }

        
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Post: QnAs
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.QnA.Where(j=>j.Question.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: QnAs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qnA = await _context.QnA
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qnA == null)
            {
                return NotFound();
            }

            return View(qnA);
        }

        // GET: QnAs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QnAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] QnA qnA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qnA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qnA);
        }

        // GET: QnAs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qnA = await _context.QnA.FindAsync(id);
            if (qnA == null)
            {
                return NotFound();
            }
            return View(qnA);
        }

        // POST: QnAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] QnA qnA)
        {
            if (id != qnA.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qnA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QnAExists(qnA.Id))
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
            return View(qnA);
        }

        // GET: QnAs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qnA = await _context.QnA
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qnA == null)
            {
                return NotFound();
            }

            return View(qnA);
        }

        // POST: QnAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qnA = await _context.QnA.FindAsync(id);
            _context.QnA.Remove(qnA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QnAExists(int id)
        {
            return _context.QnA.Any(e => e.Id == id);
        }
    }
}
