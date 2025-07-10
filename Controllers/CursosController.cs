using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CursosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
    {
        return await _context.Cursos.Include(c => c.Docente).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetCurso(int id)
    {
        var curso = await _context.Cursos.Include(c => c.Docente).FirstOrDefaultAsync(c => c.Id == id);
        if (curso == null) return NotFound();
        return curso;
    }

    [HttpGet("ciclo/{ciclo}")]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorCiclo(int ciclo)
    {
        return await _context.Cursos.Where(c => c.Ciclo == ciclo).Include(c => c.Docente).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> PostCurso(CursoCreateDto dto)
    {
        // Opcional: Validar que el docente exista
        var docente = await _context.Docentes.FindAsync(dto.IdDocente);
        if (docente == null)
            return BadRequest("El docente especificado no existe.");

        var curso = new Curso
        {
            Nombre = dto.Nombre,
            Creditos = dto.Creditos,
            HorasSemanal = dto.HorasSemanal,
            Ciclo = dto.Ciclo,
            IdDocente = dto.IdDocente
        };

        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCurso(int id, CursoUpdateDto dto)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
            return NotFound();

        // Validar que el docente exista
        var docente = await _context.Docentes.FindAsync(dto.IdDocente);
        if (docente == null)
            return BadRequest("El docente especificado no existe.");

        curso.Nombre = dto.Nombre;
        curso.Creditos = dto.Creditos;
        curso.HorasSemanal = dto.HorasSemanal;
        curso.Ciclo = dto.Ciclo;
        curso.IdDocente = dto.IdDocente;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurso(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null) return NotFound();
        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}