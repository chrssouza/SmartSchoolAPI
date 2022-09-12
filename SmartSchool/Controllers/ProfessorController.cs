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
        
        public readonly IRepository _repo;
        public ProfessorController(IRepository repo)
        {            
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Todos()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        // api/professor
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }
            return Ok(professor);
        }

        // api/professor
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

        // api/professor
        [HttpPut("{id}")]
        public IActionResult AlterarPorId(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id, false);
            if (prof == null)
            {
                return BadRequest("Professor não encontrado");
            }
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id, false);
            if (prof == null)
            {
                return BadRequest("Professor não encontrado");
            }
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }

            return BadRequest("Professor não atualizado");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }
            _repo.Remove(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor deletado");
            }

            return BadRequest("Professor não deletado");
           
        }
    }
}
