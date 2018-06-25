using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFacturacion.Models;

namespace WebApiFacturacion.EntityConections
{
    public class Fabricante
    {
        public IList<Fabricantes> ObtenerFabricantes()
        {
            IList<Fabricantes> fabricantes;
            using (var context = new FacturaContext())
            {
                fabricantes = context.Fabricantes.ToList();
                return fabricantes;
            }
        }
    }
}
