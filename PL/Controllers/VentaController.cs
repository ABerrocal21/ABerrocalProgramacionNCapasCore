using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        const string SessionCarrito = "_Carrito";
        
        [HttpGet]
        public IActionResult ProductoGetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();

            ML.Result result = BL.Producto.GetAll(producto);

            ML.Result resultDepartamento = BL.Departamento.GetAll();
            ML.Result resultArea = BL.Area.GetAll();

            if (result.Correct)
            {
                producto.Productos = result.Objects;
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                producto.Departamento.Area.Areas = resultArea.Objects;
                
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al obtener la información" + result.Message;
            }
            
            return View(producto);
        }

        public IActionResult Carrito(int? idProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            var session = HttpContext.Session.Get<List<object>>(SessionCarrito);

            if (idProducto != null)
            {
                if (HttpContext.Session.Get<List<object>>(SessionCarrito) == null)
                {
                    ML.VentaProducto ventaProducto = new ML.VentaProducto();
                    ML.Producto producto = new ML.Producto();
                    producto.IdProducto = idProducto.Value;
                    ventaProducto.Producto = new ML.Producto();
                    ventaProducto.Producto.IdProducto = idProducto.Value;
                    ventaProducto.Cantidad = 1;

                    ML.Result resultProducto = BL.Producto.GetById(producto);

                    if (resultProducto.Correct)
                    {
                        ventaProducto.Producto = (ML.Producto)resultProducto.Object;
                        result.Objects.Add(ventaProducto);
                    }

                    HttpContext.Session.Set<List<object>>(SessionCarrito, result.Objects);

                    session = HttpContext.Session.Get<List<object>>(SessionCarrito);

                }
                else
                {
                    result.Objects = HttpContext.Session.Get<ML.VentaProducto>(SessionCarrito).ToList<object>();
                                                  
                    bool Existe = false;
                    var indice = 0;

                    foreach (ML.VentaProducto ventaProducto in result.Objects)
                    {
                        if (ventaProducto.Producto.IdProducto == idProducto)
                        {
                            Existe = true;
                            indice = result.Objects.IndexOf(ventaProducto);
                        }
                    }

                    if (Existe)
                    {
                        foreach (ML.VentaProducto ventaProducto in result.Objects)
                        {
                            ventaProducto.Cantidad = ventaProducto.Cantidad + 1;
                        }
                    }
                    else
                    {
                        ML.VentaProducto ventaProducto = new ML.VentaProducto();
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = idProducto.Value;
                        ventaProducto.Producto = new ML.Producto();
                        ventaProducto.Producto.IdProducto = idProducto.Value;
                        ventaProducto.Cantidad = 1;

                        ML.Result resultProducto = BL.Producto.GetById(producto);

                        if (resultProducto.Correct)
                        {
                            ventaProducto.Producto = (ML.Producto)resultProducto.Object;
                            result.Objects.Add(ventaProducto);
                        }

                        HttpContext.Session.Set<List<object>>(SessionCarrito, result.Objects);
                    }
                }

            }
            else
            {
                if (HttpContext.Session.Get<List<object>>(SessionCarrito) != null)
                {
                    result.Objects = HttpContext.Session.Get<ML.VentaProducto>(SessionCarrito).ToList<object>();
                }
               
            }
            return View(result);
        }

        public IActionResult Sumar(int idProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = HttpContext.Session.Get<ML.VentaProducto>(SessionCarrito).ToList<object>();

            foreach (ML.VentaProducto ventaProducto in result.Objects)
            {
                if (ventaProducto.Producto.IdProducto == idProducto)
                {
                    ventaProducto.Cantidad = ventaProducto.Cantidad + 1;
                }
                
            }

            HttpContext.Session.Set<List<object>>(SessionCarrito, result.Objects);

            return View("Carrito", result);

        }

        public IActionResult Restar(int IdProducto)
        {
            ML.Result result = new ML.Result();

            result.Objects = HttpContext.Session.Get<ML.VentaProducto>(SessionCarrito).ToList<object>();

            foreach (ML.VentaProducto ventaProducto in result.Objects)
            {

                if (ventaProducto.Producto.IdProducto == IdProducto)
                {
                    ventaProducto.Cantidad -= 1;
                }
            }
            HttpContext.Session.Set<List<object>>(SessionCarrito, result.Objects);

            return View("Carrito", result);
        }

        public IActionResult Eliminar(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = HttpContext.Session.Get<ML.VentaProducto>(SessionCarrito).ToList<object>();
            var indice = 0; 

            foreach (ML.VentaProducto ventaProducto in result.Objects)
            {
                if (ventaProducto.Producto.IdProducto == IdProducto)
                {
                    indice = result.Objects.IndexOf(ventaProducto);
                }
            }

            result.Objects.RemoveAt(indice);
            HttpContext.Session.Set<List<object>>(SessionCarrito, result.Objects);
            return View("Carrito", result);
        }

        public decimal GetTotal(List<object> Objects)
        {
            decimal Total = 0;

            foreach (ML.VentaProducto ventaProducto in Objects)
            {
                Total += ventaProducto.Producto.PrecioUnitario * ventaProducto.Cantidad;
            }

            return Total;
        }

        public ActionResult Procesar()
        {
            ML.Result result = new ML.Result();
            result.Objects = HttpContext.Session.Get<ML.VentaProducto>(SessionCarrito).ToList<object>();

            ML.Venta venta = new ML.Venta();

            venta.Usuario = new ML.UserIdentity();
            venta.Usuario.UserName = "AndrewBerrocal";

            venta.MetodoPago = new ML.MetodoPago();
            venta.MetodoPago.IdMetodoPago = 1;

            venta.Total = GetTotal(result.Objects);

            ML.Result result2 = BL.Venta.Add(venta, result.Objects);

            venta.IdVenta = ((ML.Venta)result2.Object).IdVenta;
            venta.Ventas = new List<object>();
            ML.Producto producto = new ML.Producto();

            foreach (ML.VentaProducto item in result.Objects)
            {
                venta.Ventas.Add(item);
            }

            return RedirectToAction("GetById");

        }

        public ActionResult ModalCompra()
        {
            ViewBag.Message = "¿Deseas finalizar tu compra?";
            return PartialView("Modal");
        }


        public IActionResult GetById(List<object> Venta)
        {
            //ML.Result result = BL.VentaProducto.GetbyIdVenta(Venta.IdVenta);
            ML.Result result = new ML.Result();
            result.Objects = HttpContext.Session.Get<ML.VentaProducto>(SessionCarrito).ToList<object>();
            ML.VentaProducto ventaProducto = new ML.VentaProducto();

            //if (result.Correct)
            //{
            //    ventaProducto.VentaProductos = result.Objects;
            //}
            //else
            //{
            //    ViewBag.Message = result.Message;
            //}
            ventaProducto.VentaProductos = result.Objects;
            ventaProducto.Venta = new ML.Venta();
            ventaProducto.Venta.Total = GetTotal(result.Objects);

            return View("Detalle", ventaProducto);
        }
    }
}
