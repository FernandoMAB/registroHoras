using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using registroHoras.Models;

namespace registroHoras.Pages
{
    public class CreateModel : PageModel
    {
        private readonly registroHoras.Models.pruebaContext _context;
        public List<SelectListItem> Users { get; set; }

        public CreateModel(registroHoras.Models.pruebaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Users = _context.Usuarios.Select(a => 
                                new SelectListItem
                                {
                                    Value = a.Nombre,
                                    Text = a.Nombre
                                }).ToList();
            return Page();
        }

        [BindProperty]
        public RegistroEntradum RegistroEntradum { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
            _context.RegistroEntrada.Add(RegistroEntradum);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
