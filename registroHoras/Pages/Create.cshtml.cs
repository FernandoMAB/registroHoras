﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using registroHoras.Models;
using ServiceReferenceJavaSOAP;
using ServiceReferenceSOAP;

namespace registroHoras.Pages
{
    public class CreateModel : PageModel
    {
        private readonly registroHoras.Models.pruebaContext _context;
        public List<SelectListItem> Users { get; set; }
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();
        public SelectList OptionsBox { get; set; }

        public CreateModel(registroHoras.Models.pruebaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //SOAP Opciones
            DatosRequest request = new();
            var client = new ServicioSoapClient(ServicioSoapClient.EndpointConfiguration.ServicioSoap, "https://localhost:44335/Servicio.asmx");
            var opciones = client.Datos(request).Body;
            var opcionesArr = opciones.DatosResult.ToArray();
            foreach (string op in opcionesArr)
            {
                Options[op[..1]] = op;
            }
            OptionsBox = new(Options.OrderBy(x => x.Value), "Key", "Value", "");
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
            return Page();
        }

        [BindProperty]
        public RegistroEntradum RegistroEntradum { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
            _context.RegistroEntrada.Add(RegistroEntradum);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
