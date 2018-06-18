using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiFacturacion.Models;

namespace WebApiFacturacion.EntityConections
{
    public class Usuarios
    {
       public IList<Usuario> ObtenerUsuario()
        {
            IList<Usuario> Usuario;
            using (var context = new FacturaContext())
            {
                Usuario = context.Usuario.ToList();

                return Usuario;
            }

        }


       public Usuario UsuarioExiste( Usuario value)
        {
            using (var context = new FacturaContext())
            {

                 Usuario nombre = context.Usuario.FromSql($" select * from usuario where correo = {value.Correo} and pass = {value.Pass}").FirstOrDefault();

                if (nombre == null )
                {
                    return null;
                }
                else
                {
                    nombre.Pass = ":)";//Esta mascara es para proteger la pass del usuario
                    return nombre;
                }
                    
            }
        } 

    }

}
