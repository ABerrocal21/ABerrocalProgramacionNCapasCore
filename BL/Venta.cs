using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Venta
    {
        public static ML.Result Add(ML.Venta venta, List<object> objects) 
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    DL.Ventum query = new DL.Ventum();

                    query.IdVenta = venta.IdVenta;
                    query.IdCliente = venta.Usuario.IdUsuario;
                    query.IdMetodoPago = venta.MetodoPago.IdMetodoPago;

                    context.Venta.Add(query);
                    int rowsAffected = context.SaveChanges();

                    foreach (ML.VentaProducto ventaProducto in objects)
                    {
                        ventaProducto.Venta = new ML.Venta();
                        ventaProducto.Venta.IdVenta = venta.IdVenta;

                        BL.VentaProducto.Add(ventaProducto);
                    }

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrió un error al insertar el registro en la tabla Venta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }



            return result;
        }
    }
}
