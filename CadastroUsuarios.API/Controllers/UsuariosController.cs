using CadastroUsuarios.API.Data;
using CadastroUsuarios.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroUsuarios.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase {
    private readonly AppDbContext _context;

    // O construtor recebe o AppDbContext que configuramos no Program.cs
    public UsuariosController(AppDbContext context) {
        _context = context;
    }

    // 1. Endpoint para CADASTRAR um usuário (POST: api/usuarios)
    [HttpPost]
    public async Task<IActionResult> Cadastrar(Usuario usuario) {
        // Evita que dois usuários usem o mesmo e-mail
        var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email);
        if (emailExiste) {
            return BadRequest("Este e-mail já está cadastrado.");
        }

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync(); // Salva fisicamente no PostgreSQL

        return Ok(usuario);
    }

    // 2. Endpoint para LISTAR todos os usuários (GET: api/usuarios)
    [HttpGet]
    public async Task<IActionResult> Listar() {
        var usuarios = await _context.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    // Mantive o seu método antigo caso ainda queira testar a rota simples (GET: api/usuarios/teste)
    [HttpGet("teste")]
    public IActionResult Testar() {
        return Ok("API de cadastro de usuários funcionando!");
    }
}
