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
    public class IndexModel : PageModel
    {
        private readonly registroHoras.Models.pruebaContext _context;

        public IndexModel(registroHoras.Models.pruebaContext context)
        {
            _context = context;
        }

        public IList<RegistroEntradum> RegistroEntradum { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RegistroEntrada != null)
            {
                RegistroEntradum = await _context.RegistroEntrada.ToListAsync();
                foreach (var registroEntradum in RegistroEntradum)
                {
                    registroEntradum.Estado = registroEntradum.Estado switch
                    {
                        "S" => "Salida",
                        "E" => "Entrada",
                        _ => "Almuerzo",
                    };
                }
            }
        }
    }
}
