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
    public class CandidatoTecnologiaController : Controller
    {
        private readonly MvcMovieContext _context;

        public CandidatoTecnologiaController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: CandidatoTecnologia
        public async Task<IActionResult> Index()
        {
            return View(await _context.CandidatoTecnologia.ToListAsync());
        }

        // GET: CandidatoTecnologia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatoTecnologia = await _context.CandidatoTecnologia
                .FirstOrDefaultAsync(cT => cT.candidato_tecnologia_id == id);
            if (candidatoTecnologia == null)
            {
                return NotFound();
            }

            return View(candidatoTecnologia);
        }

        // GET: CandidatoTecnologia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CandidatoTecnologia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("candidato_id, tecnologia_id")] CandidatoTecnologia candidatoTecnologia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidatoTecnologia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidatoTecnologia);
        }

        // GET: CandidatoTecnologia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatoTecnologia = await _context.CandidatoTecnologia.FindAsync(id);
            if (candidatoTecnologia == null)
            {
                return NotFound();
            }
            return View(candidatoTecnologia);
        }

        // POST: CandidatoTecnologia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("candidato_tecnologia_id, candidato_id, tecnologia_id")] CandidatoTecnologia candidatoTecnologia)
        {
            if (id != candidatoTecnologia.candidato_tecnologia_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidatoTecnologia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatoTecnologiaExists(candidatoTecnologia.candidato_tecnologia_id))
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
            return View(candidatoTecnologia);
        }

        // GET: CandidatoTecnologia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidatoTecnologia = await _context.CandidatoTecnologia
                .FirstOrDefaultAsync(vT => vT.candidato_tecnologia_id == id);
            if (candidatoTecnologia == null)
            {
                return NotFound();
            }

            return View(candidatoTecnologia);
        }

        // POST: CandidatoTecnologia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidatoTecnologia = await _context.CandidatoTecnologia.FindAsync(id);
            _context.CandidatoTecnologia.Remove(candidatoTecnologia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatoTecnologiaExists(int id)
        {
            return _context.CandidatoTecnologia.Any(cT => cT.candidato_tecnologia_id == id);
        }
    }
}
