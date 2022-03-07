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
    public class CandidatoController : Controller
    {
        private readonly MvcMovieContext _context;

        public CandidatoController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Candidato
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Candidato.ToListAsync());
        // }

        public async Task<IActionResult> Index(string searchString)
        {
            var candidato = from t in _context.Candidato
                 select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                candidato = candidato.Where(s => s.nome!.Contains(searchString));
            }

            return View(await candidato.ToListAsync());
        }

      

        // GET: Candidato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Candidato = await _context.Candidato
                .FirstOrDefaultAsync(c => c.candidato_id == id);
            if (Candidato == null)
            {
                return NotFound();
            }

            return View(Candidato);
        }

        // GET: Candidato/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.vagaSelecionada = 0;
            Candidato candidato = new Candidato();
            ModeloCandidato modeloCandidato = new ModeloCandidato();
            ViewBag.Vagas = await _context.Vaga.ToListAsync();
            ViewBag.Tecnologias = await _context.Tecnologia.ToListAsync();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Vaga vaga in ViewBag.Vagas) {

                items.Add(new SelectListItem { Text = vaga.nome, Value = vaga.vaga_id.ToString()});
            }
            ViewBag.Vagas = items;
            items = new List<SelectListItem>();
            foreach (Tecnologia tecnologia in ViewBag.Tecnologias)
            {
                items.Add(new SelectListItem { Text = tecnologia.nome, Value = tecnologia.tecnologia_id.ToString() });
            }
            ViewBag.Tecnologias = items;
            return View(modeloCandidato);
        }

        // POST: Candidato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ModeloCandidato modeloCandidato)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(modeloCandidato.candidato);
                await _context.SaveChangesAsync();
                foreach (int tecId in modeloCandidato.tecSelecionadas) {
                    CandidatoTecnologia ct = new CandidatoTecnologia();
                    ct.candidato_id = modeloCandidato.candidato.candidato_id;
                    ct.tecnologia_id = tecId;
                    _context.Add(ct);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(modeloCandidato.candidato);
        }

        // GET: Candidato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Candidato = await _context.Candidato.FindAsync(id);
            if (Candidato == null)
            {
                return NotFound();
            }
            return View(Candidato);
        }

        // POST: Candidato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("candidato_id, nome, vaga_id")] Candidato Candidato)
        {
            if (id != Candidato.candidato_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Candidato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatoExists(Candidato.candidato_id))
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
            return View(Candidato);
        }

        // GET: Candidato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Candidato = await _context.Candidato
                .FirstOrDefaultAsync(c => c.candidato_id == id);
            if (Candidato == null)
            {
                return NotFound();
            }

            return View(Candidato);
        }

        // POST: Candidato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Candidato = await _context.Candidato.FindAsync(id);
            _context.Candidato.Remove(Candidato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatoExists(int id)
        {
            return _context.Candidato.Any(c => c.candidato_id == id);
        }
    }
}
