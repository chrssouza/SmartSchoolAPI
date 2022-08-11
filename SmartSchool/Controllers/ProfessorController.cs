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
        public ProfessorController(SmartContext context)
        {
            _context = context;
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
                return BadRequest("Id não encontrado");
            }
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult AlterarPorId(int id, Professor professor)
        {
            var profe = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (profe == null)
            {
                return BadRequest("Id não encontrado");
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
                return BadRequest("Id não encontrado");
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
                return BadRequest("Id não encontrado");
            }
            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}
