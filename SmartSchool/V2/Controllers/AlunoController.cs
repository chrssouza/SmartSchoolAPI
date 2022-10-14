using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.V1.Dtos;
using SmartSchool.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.V2.Controllers
{
    /// <summary>
    /// Versão 2 da minha controller de Alunos
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Método responsável para retornar todos os meus alunos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var alunos = _repo.GetAllAlunos(true);         
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }
       

        /// <summary>
        /// Método responsável por retornar apenas um aluno por meio do código Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // api/aluno
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null)
            {
                return BadRequest("O Aluno não foi encontrado");
            }

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult AlterarPorId(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não encontrado");
        }
        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }

            _repo.Remove(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }
            return BadRequest("Aluno não deletado");
        }


    }
}
