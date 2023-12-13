using ML;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    DL.Producto query = new DL.Producto();

                    query.Nombre = producto.Nombre;
                    query.PrecioUnitorio = producto.PrecioUnitario;
                    query.Stock = producto.Stock;
                    producto.Proveedor = new ML.Proveedor();
                    query.IdProveedor = producto.Proveedor.IdProveedor;
                    producto.Departamento = new ML.Departamento();
                    query.IdDepartamento = producto.Departamento.IdDepartamento;
                    query.Descripcion = producto.Descripcion;
                    query.Imagen = producto.Imagen;

                    context.Productos.Add(query);
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario insertado correctamente";
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "El usuario no pudo ser insertado correctamente  " + result.Ex;
            }

            return result;
        }
        public static ML.Result Update(ML.Producto producto)
        {
            Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from productoLINQ in context.Productos
                                       where productoLINQ.IdProducto == producto.IdProducto 
                                       select productoLINQ).SingleOrDefault();

                    if (query != null)
                    {
                        query.IdProducto = producto.IdProducto;
                        query.Nombre = producto.Nombre;
                        query.PrecioUnitorio = producto.PrecioUnitario;
                        query.Stock = producto.Stock;
                        producto.Proveedor = new ML.Proveedor();
                        query.IdProveedor = producto.Proveedor.IdProveedor;
                        producto.Departamento = new ML.Departamento();
                        query.IdDepartamento = producto.Departamento.IdDepartamento;
                        query.Descripcion = producto.Descripcion;
                        query.Imagen = producto.Imagen;

                        int RowsAffected = context.SaveChanges();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                            result.Message = "Usuario insertado correctamente";
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "El usuario no pudo ser actualizado correctamente  " + result.Ex;
            }

            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from producto in context.Productos
                                 join proveedorLINQ in context.Proveedors on producto.IdProveedor equals proveedorLINQ.IdProveedor
                                 join departamentoLINQ in context.Departamentos on producto.IdDepartamento equals departamentoLINQ.IdDepartamento
                                 join areaLINQ in context.Areas on departamentoLINQ.IdArea equals areaLINQ.IdArea
                                 select new
                                 {
                                     IdProducto = producto.IdProducto,
                                     Nombre = producto.Nombre,
                                     Precio = producto.PrecioUnitorio,
                                     Stock = producto.Stock,
                                     Proovedor = proveedorLINQ.Nombre,
                                     Departamento = departamentoLINQ.Nombre,
                                     Area = areaLINQ.Nombre,
                                     Descripcion = producto.Descripcion,
                                     Imagen = producto.Imagen,
                                 }).ToList();

                    if (query.Count > 0 )
                    {
                        result.Objects = new List<object>();
                        foreach ( var item in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = item.IdProducto;
                            producto.Nombre = item.Nombre;
                            producto.PrecioUnitario = item.Precio;
                            producto.Stock = item.Stock;
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.Nombre = item.Proovedor;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.Nombre = item.Departamento;
                            producto.Descripcion = item.Descripcion;
                            producto.Imagen = item.Imagen;

                            result.Objects.Add( producto );
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
        public static ML.Result GetById(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from productoLINQ in context.Productos
                                 where productoLINQ.IdProducto == producto.IdProducto
                                 join proveedorLINQ in context.Proveedors on productoLINQ.IdProveedor equals proveedorLINQ.IdProveedor
                                 join departamentoLINQ in context.Departamentos on productoLINQ.IdDepartamento equals departamentoLINQ.IdDepartamento
                                 join areaLINQ in context.Areas on departamentoLINQ.IdArea equals areaLINQ.IdArea
                                 select new
                                 {
                                     IdProducto = productoLINQ.IdProducto,
                                     Nombre = productoLINQ.Nombre,
                                     Precio = productoLINQ.PrecioUnitorio,
                                     Stock = productoLINQ.Stock,
                                     Proovedor = proveedorLINQ.IdProveedor,
                                     Area = departamentoLINQ.IdArea,
                                     Departamento = departamentoLINQ.IdDepartamento,
                                     Descripcion = productoLINQ.Descripcion,
                                     Imagen = productoLINQ.Imagen
                                 });
  
                    result.Objects = new List<object>();
                    foreach (var item in query)
                    {

                        producto.IdProducto = item.IdProducto;
                        producto.Nombre = item.Nombre;
                        producto.PrecioUnitario = item.Precio;
                        producto.Stock = item.Stock;
                        producto.Proveedor = new ML.Proveedor();                        
                        producto.Proveedor.IdProveedor = item.Proovedor;
                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = item.Departamento;
                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = item.Area.Value;
                        producto.Descripcion = item.Descripcion;
                        producto.Imagen = item.Imagen;

                        result.Object = producto;
                    }

                        result.Correct = true;
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
        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from productoLINQ in context.Productos
                                    where productoLINQ.IdProducto == producto.IdProducto                                   
                                    select productoLINQ).First();

                    context.Productos.Remove(query);
                    int RowsAffected = context.SaveChanges();


                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "El usuario no pudo ser borrado correctamente";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }

            return result;
        }
    }

}

