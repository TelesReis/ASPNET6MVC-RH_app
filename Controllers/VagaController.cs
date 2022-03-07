#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using System.Data.Common;

namespace MvcMovie.Controllers
{
    public class VagaController : Controller
    {
        private readonly MvcMovieContext _context;

        public VagaController(MvcMovieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Relatorio(int? id)
        {
            List<Relatorio> groups = new List<Relatorio>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    string query = "select " +
"c.nome, " +
"sum(vt.peso)  " +
"from " +
"vaga v " +
"inner join candidato c " +
"on " +
"v.vaga_id = c.vaga_id " +
"inner join vaga_tecnologia vt " +
"on " +
"v.vaga_id = vt.vaga_id " +
"and c.vaga_id = vt.vaga_id " +
"inner join candidato_tecnologia ct " +
"on " +
"c.candidato_id = ct.candidato_id " +
"and vt.tecnologia_id = ct.tecnologia_id " +
"where " +
"v.vaga_id = " + id +
" group by " +
"c.nome ; "; ;
                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Relatorio { nome = reader.GetString(0), peso = reader.GetDecimal(1) };
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return View("Relatorio", groups);
        }

        // GET: Vaga
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vaga.ToListAsync());
        }

        // GET: Vaga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga
                .FirstOrDefaultAsync(v => v.vaga_id == id);
            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        // GET: Vaga/Create
        public async Task<IActionResult> Create()
        {
            CreateVaga cv = new CreateVaga();
            cv.tecs = await _context.Tecnologia.ToListAsync();

            cv.vagasTec = new List<TecVagaNome>();
            return View(cv);
        }
        public IActionResult AddTecGET(CreateVaga cv)
        {

            return View("Create", cv);
        }

        [HttpPost]
        public IActionResult AddTec(CreateVaga cv)
        {
            cv.tecs = _context.Tecnologia.ToList();
            if (cv.vagasTec == null)
            {

                cv.vagasTec = new List<TecVagaNome>();
            }
            if (cv.vagaTec != null)
            {
                Tecnologia tec = _context.Tecnologia.Find(cv.vagaTec.vagaTec.tecnologia_id);
                cv.vagaTec.nome = tec.nome;
                cv.vagasTec.Add(cv.vagaTec);
                cv.vagaTec = new TecVagaNome();

            }
            return View("Create", cv);
        }

        // POST: Vaga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVaga createVaga)
        {

            if (ModelState.IsValid)
            {

                _context.Add(createVaga.vaga);
                await _context.SaveChangesAsync();
                foreach (TecVagaNome vagaTec in createVaga.vagasTec)
                {
                    vagaTec.vagaTec.vaga_id = createVaga.vaga.vaga_id;
                    _context.Add(vagaTec.vagaTec);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(createVaga.vaga);
        }

        // GET: Vaga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }
            return View(vaga);
        }

        // POST: Vaga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("vaga_id, nome")] Vaga vaga)
        {
            if (id != vaga.vaga_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VagaExists(vaga.vaga_id))
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
            return View(vaga);
        }

        // GET: Vaga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga
                .FirstOrDefaultAsync(v => v.vaga_id == id);
            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        // POST: Vaga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaga = await _context.Vaga.FindAsync(id);
            _context.Vaga.Remove(vaga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool VagaExists(int id)
        {
            return _context.Vaga.Any(v => v.vaga_id == id);
        }
    }
}
