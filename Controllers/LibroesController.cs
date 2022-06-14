using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca_EntityFramework;
using Biblioteca_EntityFramework.Models;

namespace Biblioteca_EntityFramework.Controllers
{
    public class LibroesController : Controller
    {
        private readonly LibraryContext _context;

        public LibroesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Libroes
        public async Task<IActionResult> Index()
        {
              return _context.Libri != null ? 
                          View(await _context.Libri.ToListAsync()) :
                          Problem("Entity set 'LibraryContext.Libri'  is null.");
        }

        // GET: Libroes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Libri == null)
            {
                return NotFound();
            }

            var libro = await _context.Libri.Where(l => l.ID == id)
                .Include(l => l.Autori)
                .FirstOrDefaultAsync();

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libroes/Create
        public IActionResult Create()
        {
            Libro myLibro = new Libro();
            myLibro.Data = DateTime.Now.Date;
            myLibro.Autori = new List<Autore>();
            ViewData["listaAutoriAll"] = _context.Autori.ToList<Autore>();
            return View(myLibro);
        }

        // POST: Libroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titolo,Data,stato,ISBN,numeroDiPagine,Autori")] Libro libro, string[] AutoriSelect)
        {
            if(libro.Autori == null)
            {
                libro.Autori = new List<Autore>();
            }
            foreach(var item in AutoriSelect)
            {
                var array = item.Split(','); 
                Autore autoreDaSelect = (Autore)_context.Autori.Where(m => m.Nome == array[0] && m.Cognome == array[1]).First();
                libro.Autori.Add(autoreDaSelect);
            }

            /*blocco di codice che calcola la condizione per aggirare la validazione per valutare gli autori inseriti da select*/
            bool noAuthorValidation = true;

            foreach(var item in ModelState.Keys)
            {
                if(item != "Autori" && ModelState.GetValidationState(item) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    noAuthorValidation = false;
                    break;
                }
            }
            /*blocco di codice che calcola la condizione per aggirare la validazione per valutare gli autori inseriti da select*/

            if (ModelState.IsValid || (noAuthorValidation && libro.Autori.Count != 0))
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Create));
        }

        // GET: Libroes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Libri == null)
            {
                return NotFound();
            }

            var libro = await _context.Libri.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // POST: Libroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,Titolo,Data,stato,ISBN,numeroDiPagine")] Libro libro)
        {
            if (id != libro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.ID))
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
            return View(libro);
        }

        // GET: Libroes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Libri == null)
            {
                return NotFound();
            }

            var libro = await _context.Libri
                .FirstOrDefaultAsync(m => m.ID == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Libri == null)
            {
                return Problem("Entity set 'LibraryContext.Libri'  is null.");
            }
            var libro = await _context.Libri.FindAsync(id);
            if (libro != null)
            {
                _context.Libri.Remove(libro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(long id)
        {
          return (_context.Libri?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
