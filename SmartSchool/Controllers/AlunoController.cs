using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.Models;
using System.Linq;

namespace SmartSchool.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        // api/aluno
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null)
            {
                return BadRequest("O Aluno não foi encontrado");
            }
            return Ok(aluno);

        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não encontrado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alu = _repo.GetAlunoById(id);
            if (alu == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _repo.Remove(alu);
            if (_repo.SaveChanges())
            {
                return Ok(alu);
            }
            return BadRequest("Aluno não deletado");
        }


    }
}
