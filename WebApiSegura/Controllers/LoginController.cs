using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest loginRequest)
        {
            if (loginRequest == null)
                return BadRequest();

            Usuario usuario = new Usuario();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(
                        @"SELECT * FROM Usuario
                    WHERE USU_IDENTIFICATION = @USU_IDENTIFICATION
                    AND USU_PASSWORD = @USU_PASSWORD", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@USU_IDENTIFICAION", loginRequest.Username);
                    sqlCommand.Parameters.AddWithValue("@USU_PASSWORD", loginRequest.Password);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        usuario.USU_CODIGO = sqlDataReader.GetInt32(0);
                        usuario.USU_IDENTIFICACION = sqlDataReader.GetString(1);
                        usuario.USU_NOMBRE = sqlDataReader.GetString(2);
                        usuario.USU_PASSWORD = sqlDataReader.GetString(3);
                        usuario.USU_EMAIL = sqlDataReader.GetString(4);
                        usuario.USU_ESTADO = sqlDataReader.GetString(5);
                        usuario.USU_FEC_NAC = sqlDataReader.GetDateTime(6);
                        usuario.USU_TELEFONO = sqlDataReader.GetString(7);

                        var token = TokenGenerator.GenerateTokenJwt(loginRequest.Username);
                        usuario.Token = token;
                    }
                    sqlConnection.Close();
                    if (!string.IsNullOrEmpty(usuario.Token))
                        return Ok(usuario);
                    else
                        return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
