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
    public class VagaTecnologiaController : Controller
    {
        private readonly MvcMovieContext _context;

        public VagaTecnologiaController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.VagaTecnologia.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vagaTecnologia = await _context.VagaTecnologia
                .FirstOrDefaultAsync(vT => vT.vaga_tecnologia_id == id);
            if (vagaTecnologia == null)
            {
                return NotFound();
            }

            return View(vagaTecnologia);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("peso, vaga_id, tecnologia_id")] VagaTecnologia vagaTecnologia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vagaTecnologia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vagaTecnologia);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vagaTecnologia = await _context.VagaTecnologia.FindAsync(id);
            if (vagaTecnologia == null)
            {
                return NotFound();
            }
            return View(vagaTecnologia);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("vaga_tecnologia_id, peso, vaga_id, tecnologia_id")] VagaTecnologia vagaTecnologia)
        {
            if (id != vagaTecnologia.vaga_tecnologia_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vagaTecnologia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VagaTecnologiaExists(vagaTecnologia.vaga_tecnologia_id))
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
            return View(vagaTecnologia);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vagaTecnologia = await _context.VagaTecnologia
                .FirstOrDefaultAsync(vT => vT.vaga_tecnologia_id == id);
            if (vagaTecnologia == null)
            {
                return NotFound();
            }

            return View(vagaTecnologia);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vagaTecnologia = await _context.VagaTecnologia.FindAsync(id);
            _context.VagaTecnologia.Remove(vagaTecnologia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VagaTecnologiaExists(int id)
        {
            return _context.VagaTecnologia.Any(vT => vT.vaga_tecnologia_id == id);
        }
    }
}
