using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace registroHoras.Models
{
    public partial class RegistroEntradum
    {
        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Usuario { get; set; }
        public string? Estado { get; set; }
    }
}
