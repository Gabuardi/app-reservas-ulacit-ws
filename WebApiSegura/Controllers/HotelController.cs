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
    [Authorize]
    [RoutePrefix("api/hotel")]
    public class HotelController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Hotel hotel = new Hotel();

            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString);
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM Hotel
                                                            WHERE HOT_CODIGO = @HOT_CODIGO", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@HOT_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        hotel.HOT_CODIGO = sqlDataReader.GetInt32(0);
                        hotel.HOT_NOMBRE = sqlDataReader.GetString(1);
                        hotel.HOT_EMAIL = sqlDataReader.GetString(2);
                        hotel.HOT_DIRECCION = sqlDataReader.GetString(3);
                        hotel.HOT_TELEFONO = sqlDataReader.GetString(4);
                        hotel.HOT_CATEGORIA = sqlDataReader.GetString(5);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(hotel);
        }
    }
}
