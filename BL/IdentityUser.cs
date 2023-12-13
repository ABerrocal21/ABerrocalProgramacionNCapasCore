using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class IdentityUser
    {
        public static ML.Result GetAll(Guid idRole)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from user in context.AspNetUsers
                                 /*here user.Id != idRole.ToString() && user.Id ==*/
                                 select new
                                 {
                                     Id = user.Id,
                                     UserName = user.UserName

                                 }).ToList();

                    if (query.Count > 0 )
                    {
                        result.Objects = new List<object>();
                        foreach ( var item in query)
                        {
                            ML.UserIdentity user = new ML.UserIdentity();
                            user.IdUsuario = item.Id;
                            user.UserName = item.UserName;
                            result.Objects.Add(user);
                        }
                        result.Correct = true;
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

        public static ML.Result Asignar(ML.UserIdentity user)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddAspNetUserRoles '{user.IdUsuario}', '{user.Rol.RoleId}'");

                    if (query != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al hacer la asignacion";
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

