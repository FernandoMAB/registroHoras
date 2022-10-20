﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using registroHoras.Models;

namespace registroHoras.Pages
{
    public class EditModel : PageModel
    {
        private readonly registroHoras.Models.pruebaContext _context;
        public List<SelectListItem> Users { get; set; }

        public EditModel(registroHoras.Models.pruebaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegistroEntradum RegistroEntradum { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.RegistroEntrada == null)
            {
                return NotFound();
            }
            Users = _context.Usuarios.Select(a =>
                    new SelectListItem
                    {
                        Value = a.Nombre,
                        Text = a.Nombre
                    }).ToList();
            var registroentradum =  await _context.RegistroEntrada.FirstOrDefaultAsync(m => m.Id == id);
            if (registroentradum == null)
            {
                return NotFound();
            }
            RegistroEntradum = registroentradum;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string nombre)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (RegistroEntradum.Fecha == null)
            {
                RegistroEntradum.Fecha = DateTime.Now;
            }

            RegistroEntradum.Usuario = nombre;
            _context.Attach(RegistroEntradum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroEntradumExists(RegistroEntradum.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RegistroEntradumExists(long id)
        {
          return _context.RegistroEntrada.Any(e => e.Id == id);
        }
    }
}
