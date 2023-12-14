using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetbyArea(int IdArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from departamentoLINQ in context.Departamentos
                                 where departamentoLINQ.IdArea == IdArea
                                 select new
                                 {
                                     IdDepartamento = departamentoLINQ.IdDepartamento,
                                     Nombre = departamentoLINQ.Nombre

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = item.IdDepartamento;
                            departamento.Nombre = item.Nombre;

                            result.Objects.Add(departamento);
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
        public static Result Add(ML.Departamento departamento)
        {
            Result result = new Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    DL.Departamento query = new DL.Departamento();

                    //productoDL.IdProducto = producto.IdProducto;
                    query.Nombre = departamento.Nombre;
                    query.IdArea = departamento.Area.IdArea;


                    context.Departamentos.Add(query);
                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected >= 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrio un error al insertar el registro";
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
        public static Result Update(ML.Departamento departamento)
        {
            Result result = new Result();
            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from departamentoLINQ in context.Departamentos
                                 where departamentoLINQ.IdDepartamento == departamento.IdDepartamento
                                 select departamentoLINQ).SingleOrDefault();

                    if (query != null)
                    {
                        query.Nombre = departamento.Nombre;
                        query.IdArea = departamento.Area.IdArea;
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
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from departamentoLINQ in context.Departamentos
                                 join areaLINQ in context.Areas on departamentoLINQ.IdArea equals areaLINQ.IdArea
                                 select new
                                 {
                                     IdDepartamento = departamentoLINQ.IdDepartamento,
                                     Nombre = departamentoLINQ.Nombre,
                                     IdArea = departamentoLINQ.IdArea,
                                     Area = areaLINQ.Nombre

                                 }).ToList();

                    result.Objects = new List<object>();
                        
                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = item.IdDepartamento;
                            departamento.Nombre = item.Nombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = item.IdArea.Value;
                            departamento.Area.Nombre = item.Area;

                            result.Objects.Add(departamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static Result GetById(ML.Departamento departamento)
        {
            Result result = new Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from departamentoLINQ in context.Departamentos
                                 join areaLINQ in context.Areas on departamentoLINQ.IdArea equals areaLINQ.IdArea
                                 where departamentoLINQ.IdDepartamento == departamento.IdDepartamento
                                 select new
                                 {
                                     IdDepartamento = departamentoLINQ.IdDepartamento,
                                     Nombre = departamentoLINQ.Nombre,
                                     IdArea = departamentoLINQ.IdArea,
                                     Area = areaLINQ.Nombre

                                 });

                    result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            departamento.IdDepartamento = item.IdDepartamento;
                            departamento.Nombre = item.Nombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = item.IdArea.Value;
                            departamento.Area.Nombre = item.Area;

                            result.Object = departamento;
                        }

                        result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "No se encontraron registros" + ex.Message;
            }
            return result;
        }
        public static Result Delete(ML.Departamento departamento)
        {
            Result result = new Result();
            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from departamentoLINQ in context.Departamentos
                                 where departamentoLINQ.IdDepartamento == departamento.IdDepartamento
                                 select departamentoLINQ).First();

                    context.Departamentos.Remove(query);
                    context.SaveChanges();

                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontro el producto" + departamento.IdDepartamento;
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
