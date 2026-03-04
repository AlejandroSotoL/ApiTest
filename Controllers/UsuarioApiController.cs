using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioApiController : ControllerBase
    {
        private readonly ILogger<UsuarioApiController> _logger;

        public UsuarioApiController(ILogger<UsuarioApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            
            // if (!ModelState.IsValid)
            // {
            //     var errors = ModelState.Values
            //         .SelectMany(v => v.Errors)
            //         .Select(e => e.ErrorMessage)
            //         .ToList();
            //     return BadRequest(new { success = false, errors });
            // }
            using (SqlConnection conn = new SqlConnection("Server=DESKTOP-P657RHG\\SQL2016;Database=ApiTest;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                string query = $"SELECT * FROM Usuarios WHERE Username = '{dto.Username}' AND Password = '{dto.Password}'";
                // // string query = "SELECT * FROM Usuarios WHERE Username = @user AND Password = @pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                // cmd.Parameters.AddWithValue("@user", dto.Username);
                // cmd.Parameters.AddWithValue("@pass", dto.Password);

                object result = null;
                try
                {
                    conn.Open();
                    result = cmd.ExecuteScalar();
                }
                catch
                {
                }

                if (result != null)
                    return Ok(new { success = true, message = "Login exitoso" });


                return BadRequest(new
                {
                    success = false,
                    message = $"Usuario {dto.Username} o contraseña {dto.Password} invalidos"
                });
            }
        }

        [HttpPost("Vulnerabilidad")]
        public IActionResult Vulnerabilidad([FromBody] LoginDto login)
        {
            // Solo para mostrar en la demo
            Console.WriteLine($"Usuario: {login.Username}, Contraseña: {login.Password}");
            return Ok(new { message = "Datos recibidos (demo)" });
        }

    }
}