using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFacturacion.Models;

namespace WebApiFacturacion.EntityConections
{
    public class Contactos
    {

        public IList<Clientes> ObtenerClientes()
        {
            IList<Clientes> clientes; 
            using (var context = new FacturaContext())
            {
                clientes = context.Clientes.ToList();
                return clientes;
            }
        }
        public IList<Proveedor> ObtenerProveedor()
        {
            IList<Proveedor> proveedor;
            using (var context = new FacturaContext())
            {
                proveedor = context.Proveedor.ToList();
                return proveedor;
            }
        }



    }
}
