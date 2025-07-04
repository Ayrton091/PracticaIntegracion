using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DocentesController : ControllerBase
{
    private readonly AppDbContext _context;

    public DocentesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/docentes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Docente>>> GetDocentes()
    {
        return await _context.Docentes.ToListAsync();
    }

    // POST: api/docentes
    [HttpPost]
    public async Task<ActionResult<Docente>> PostDocente(DocenteCreateDto dto)
    {
        var docente = new Docente
        {
            Apellidos = dto.Apellidos,
            Nombres = dto.Nombres,
            Profesion = dto.Profesion,
            FechaNacimiento = dto.FechaNacimiento,
            Correo = dto.Correo
        };

        _context.Docentes.Add(docente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDocentes), new { id = docente.Id }, docente);
    }
}