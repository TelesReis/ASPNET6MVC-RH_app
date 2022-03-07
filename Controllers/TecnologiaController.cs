#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class TecnologiaController : Controller
    {
        private readonly MvcMovieContext _context;

        public TecnologiaController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Tecnologia
        // public async Task<IActionResult> Index()
        // {
            
        //     return View(await _context.Tecnologia.ToListAsync());
        // }

        public async Task<IActionResult> Index(string searchString)
        {
            var tecnologia = from t in _context.Tecnologia
                 select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tecnologia = tecnologia.Where(s => s.nome!.Contains(searchString));
            }

            return View(await tecnologia.ToListAsync());
        }


        // GET: Tecnologia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tecnologia = await _context.Tecnologia
                .FirstOrDefaultAsync(t => t.tecnologia_id == id);
            if (Tecnologia == null)
            {
                return NotFound();
            }

            return View(Tecnologia);
        }

        // GET: Tecnologia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnologia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome")] Tecnologia Tecnologia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Tecnologia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Tecnologia);
        }

        // GET: Tecnologia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tecnologia = await _context.Tecnologia.FindAsync(id);
            if (Tecnologia == null)
            {
                return NotFound();
            }
            return View(Tecnologia);
        }

        // POST: Tecnologia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("tecnologia_id, nome")] Tecnologia Tecnologia)
        {
            if (id != Tecnologia.tecnologia_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Tecnologia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnologiaExists(Tecnologia.tecnologia_id))
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
            return View(Tecnologia);
        }

        // GET: Tecnologia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tecnologia = await _context.Tecnologia
                .FirstOrDefaultAsync(t => t.tecnologia_id == id);
            if (Tecnologia == null)
            {
                return NotFound();
            }

            return View(Tecnologia);
        }

        // POST: Tecnologia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Tecnologia = await _context.Tecnologia.FindAsync(id);
            _context.Tecnologia.Remove(Tecnologia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnologiaExists(int id)
        {
            return _context.Tecnologia.Any(t => t.tecnologia_id == id);
        }
    }
}
