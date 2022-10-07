using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.Dtos;
using SmartSchool.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProfessorController : ControllerBase
    {
        
        public readonly IRepository _repo;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repo, IMapper mapper)
        {            
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Todos()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(result));
        }

        [HttpGet("getRegister")]
        public IActionResult getRegister()
        {
            return Ok(new ProfessorRegistrarDto());
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
            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professor);
        }

        // api/professor
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("professor não encontrado");
        }

        // api/professor
        [HttpPut("{id}")]
        public IActionResult AlterarPorId(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }
            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não encontrado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado");
            }
            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não encontrado");

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
