using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.Models;
using System.Linq;

namespace SmartSchool.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;
        public readonly IRepository _repo;
        public ProfessorController(SmartContext context, IRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Todos()
        {
            return Ok(_context.Professores);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }
            return Ok(professor);
        }

        [HttpGet("nome")]
        public IActionResult BuscarPorNome(int id, string nome)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(nome));
            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("professor não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult AlterarPorId(int id, Professor professor)
        {
            var profe = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (profe == null)
            {
                return BadRequest("Professor não encontrado");
            }
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var profe = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (profe == null)
            {
                return BadRequest("Professor não encontrado");
            }
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }
            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}
