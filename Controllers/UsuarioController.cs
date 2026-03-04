using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.Data.SqlClient;
using api.Dto;

namespace api.Controllers;

[Route("api/[controller]")]
public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/Usuario")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("Login")]
    public IActionResult Login([FromForm] LoginDto dto)
    {
        using (SqlConnection conn = new SqlConnection("Server=DESKTOP-VJ2RO9L\\SQL2016;Database=ApiTest;Trusted_Connection=True;TrustServerCertificate=True;"))
        {
            // string query = $"SELECT * FROM Usuarios WHERE Username = '{dto.Username}' AND Password = '{dto.Password}'";
            // // string query = "SELECT * FROM Usuarios WHERE Username = @user AND Password = @pass";
            // SqlCommand cmd = new SqlCommand(query, conn);
            // // cmd.Parameters.AddWithValue("@user", dto.Username);
            // // cmd.Parameters.AddWithValue("@pass", dto.Password);

            // conn.Open();
            // var result = cmd.ExecuteScalar();

            // if (result != null)
            //     return Ok(new { success = true, message = "Login exitoso" });

            // return Unauthorized(new { success = false, message = "Credenciales inválidas" });

            return Ok(new
            {
                success = false,
                message = $"Usuario {dto.Username} inválido"
            });
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
