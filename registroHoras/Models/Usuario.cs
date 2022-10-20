using System;
using System.Collections.Generic;

namespace registroHoras.Models
{
    public partial class Usuario
    {
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apllido { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
    }
}
