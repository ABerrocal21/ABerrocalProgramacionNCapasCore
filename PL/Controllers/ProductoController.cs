using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();

            ML.Result result = BL.Producto.GetAll(producto);

            ML.Result resultArea = BL.Area.GetAll();
            producto.Departamento.Area.Areas = resultArea.Objects;

            producto.Productos = new List<object>();

            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            else
            {
                ViewBag.Message = result.Message;
            }

            return View(producto);
        }

        [HttpPost]
        public IActionResult GetAll(ML.Producto producto)
        {

            ML.Result result = BL.Producto.GetAll(producto);
            producto.Productos = new List<object>();

            ML.Result resultArea = BL.Area.GetAll();
            producto.Departamento.Area.Areas = resultArea.Objects;



            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            else
            {
                ViewBag.Message = result.Message;
            }

            return View(producto);
        }

        [HttpGet]
        public IActionResult Form(int? idProducto)
        {
            ML.Producto producto = new ML.Producto();

            producto.Proveedor = new ML.Proveedor();
            ML.Result resultProveedor = BL.Proveedor.GetAll();
            producto.Proveedor.Proveedores = resultProveedor.Objects;

            ML.Result resultArea = BL.Area.GetAll();

            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();         
            producto.Departamento.Area.Areas = resultArea.Objects;

            

            if (idProducto == null)
            {
                ViewBag.Action = "Agregar";
            }
            else
            {
                producto.IdProducto = idProducto.Value;
                ML.Result result = BL.Producto.GetById(producto);
                

                if (result.Correct)
                {

                    producto = ((ML.Producto)result.Object);

                    
                    resultArea = BL.Area.GetAll();
                    ML.Result resultDepartamento = BL.Departamento.GetbyArea(producto.Departamento.Area.IdArea);

                    producto = ((ML.Producto)result.Object);

                    producto.Departamento.Departamentos = resultDepartamento.Objects;
                    producto.Proveedor.Proveedores = resultProveedor.Objects;
                    producto.Departamento.Area.Areas = resultArea.Objects;

                }

                ViewBag.Action = "Actualizar";

            }
            return View(producto);
        }

        [HttpPost]
        public IActionResult Form(ML.Producto producto, IFormFile fuImagen)
        {
            if ((producto.Imagen != null && fuImagen != null)||(producto.Imagen == null && fuImagen !=null))
            {
                    if (fuImagen.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            fuImagen.CopyTo(ms);
                            producto.Imagen = ms.ToArray();
                            string s = Convert.ToBase64String(producto.Imagen);
                            // act on the Base64 data
                        }
                    }
                
            }

            if (producto.IdProducto == 0)
            {

                ML.Result result = BL.Producto.Add(producto);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha ingresado correctamente el Usuario" + result.Message;
                    return PartialView("Modal");
                }
            }
            else
            {
                ML.Result result = BL.Producto.Update(producto);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado correctamente el Usuario";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha actualizado correctamente el Usuario" + result.Message;
                    return PartialView("Modal");
                }
            }
        }

        public IActionResult Delete (int idProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.IdProducto = idProducto;
            ML.Result result = BL.Producto.Delete(producto);

            if (result.Correct)
            {
                ViewBag.Message = "Se ha borrado correctamente el Usuario";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se ha borrado correctamente el Usuario";
                return PartialView("Modal");
            }

        }

        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Departamento.GetbyArea(IdArea);
            return Json(result.Objects);
        }

    }
}
