using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using registroHoras.Models;
using ServiceReferenceJavaSOAP;
using ServiceReferenceSOAP;

namespace registroHoras.Pages
{
    public class EditModel : PageModel
    {
        private readonly registroHoras.Models.pruebaContext _context;
        public List<SelectListItem> Users { get; set; }
        public SelectList OptionsBox { get; set; }
        
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();
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
            //SOAP Opciones
            DatosRequest request = new();
            var client = new ServicioSoapClient(ServicioSoapClient.EndpointConfiguration.ServicioSoap, "https://localhost:44335/Servicio.asmx");
            var opciones = client.Datos(request).Body;
            var opcionesArr = opciones.DatosResult.ToArray();
            foreach (string op in opcionesArr)
            {
                Options[op[..1]] = op;
            }
            OptionsBox = new (Options.OrderBy(x => x.Value), "Key", "Value", "");
            //SOAP Usuarios
            findAllRequest requestFindAll = new();
            var clientJava = new ServicioWebClient(ServicioWebClient.EndpointConfiguration.ServicioWebPort, "http://localhost:8080/CRUD/ServicioWeb");
            var usuarios = clientJava.findAll(requestFindAll).@return;
            Users = usuarios.Select(a =>
                                new SelectListItem
                                {
                                    Value = a.nombre,
                                    Text = a.nombre
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
        public async Task<IActionResult> OnPostAsync(string nombre, string opcion)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (RegistroEntradum.Fecha == null)
            {
                RegistroEntradum.Fecha = DateTime.Now;
            }
            RegistroEntradum.Estado = opcion;
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
