using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using registroHoras.Models;

namespace registroHoras.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly registroHoras.Models.pruebaContext _context;

        public DetailsModel(registroHoras.Models.pruebaContext context)
        {
            _context = context;
        }

      public RegistroEntradum RegistroEntradum { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.RegistroEntrada == null)
            {
                return NotFound();
            }

            var registroentradum = await _context.RegistroEntrada.FirstOrDefaultAsync(m => m.Id == id);
            if (registroentradum == null)
            {
                return NotFound();
            }
            else 
            {
                RegistroEntradum = registroentradum;
                RegistroEntradum.Estado = RegistroEntradum.Estado switch
                {
                    "S" => "Salida",
                    "E" => "Entrada",
                    _ => "Almuerzo",
                };
            }
            return Page();
        }
    }
}
