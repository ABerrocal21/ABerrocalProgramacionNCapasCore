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
        public static ML.Result GetbyIdVenta(int idVenta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from ventaProductoLINQ in context.VentaProductos
                                 join ventaLINQ in context.Venta on ventaProductoLINQ.IdVenta equals ventaLINQ.IdVenta
                                 join productoLINQ in context.Productos on ventaProductoLINQ.IdProducto equals productoLINQ.IdProducto
                                 where ventaProductoLINQ.IdVenta == idVenta
                                 select new
                                 {
                                     IdVentaProducto = ventaProductoLINQ.IdVentaProducto,
                                     Cantidad = ventaProductoLINQ.Cantidad,
                                     IdVenta = ventaProductoLINQ.IdVenta,
                                     Total = ventaLINQ.Total,
                                     IdProducto = ventaProductoLINQ.IdProducto,
                                     ProductoNombre = productoLINQ.Nombre,
                                     PrecioUnitario = productoLINQ.PrecioUnitorio,
                                     Descripcion = productoLINQ.Descripcion,
                                     Imagen = productoLINQ.Imagen,

                                 });

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.VentaProducto ventaProducto = new ML.VentaProducto();

                            ventaProducto.IdVentaProducto = item.IdVentaProducto;
                            ventaProducto.Venta = new ML.Venta();
                            ventaProducto.Venta.IdVenta = item.IdVenta.Value;
                            ventaProducto.Venta.Total = item.Total;
                            ventaProducto.Producto = new ML.Producto();
                            ventaProducto.Producto.IdProducto = item.IdProducto.Value;
                            ventaProducto.Producto.Nombre = item.ProductoNombre;
                            ventaProducto.Producto.PrecioUnitario = item.PrecioUnitario;
                            ventaProducto.Producto.Descripcion = item.Descripcion;
                            ventaProducto.Producto.Imagen = item.Imagen;
                            ventaProducto.Cantidad = item.Cantidad;

                            result.Objects.Add(ventaProducto);

                        }

                        result.Correct = true;
                    }
                    else 
                    { 
                        result.Correct = false;
                        result.Message = "No existen registros";
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
