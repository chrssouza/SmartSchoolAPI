using System;

namespace SmartSchool.V1.Dtos
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }        
        public string Telefone { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;        
        public bool Ativo { get; set; } = true;
    }
}
