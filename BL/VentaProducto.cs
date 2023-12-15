using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class VentaProducto
    {   
        public static ML.Result Add(ML.VentaProducto ventaProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    DL.VentaProducto query = new DL.VentaProducto();

                    query.IdVentaProducto = ventaProducto.IdVentaProducto;
                    query.Cantidad = ventaProducto.Cantidad;
                    query.IdProducto = ventaProducto.Venta.IdVenta;

                    context.VentaProductos.Add(query);
                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrio un Error al insertar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct= false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
