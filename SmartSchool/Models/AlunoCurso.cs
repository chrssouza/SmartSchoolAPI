using System;

namespace SmartSchool.Models
{
    public class AlunoCurso
    {
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }        
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;

        public AlunoCurso()
        {
        }

        public AlunoCurso(int alunoId, int cursoId)
        {
            AlunoId = alunoId;            
            CursoId = cursoId;            
        }
    }
}
