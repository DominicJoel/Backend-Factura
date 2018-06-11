using System;
using System.Collections.Generic;

namespace WebApiFacturacion.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Pass { get; set; }
        public int IdRole { get; set; }

        public Roles IdRoleNavigation { get; set; }
    }
}
