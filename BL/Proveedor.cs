using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from proveedorLINQ in context.Proveedors
                                 select new
                                 {
                                     IdProveedor = proveedorLINQ.IdProveedor,
                                     Nombre = proveedorLINQ.Nombre,
                                     Telefono = proveedorLINQ.Telefono,
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();
                            proveedor.IdProveedor = item.IdProveedor;
                            proveedor.Nombre = item.Nombre;
                            proveedor.Telefono = item.Telefono;

                            result.Objects.Add(proveedor);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.Ex = e;
                result.Message = "No se encontraron registros. " + result.Ex;
            }

            return result;

        }
    }
}
