using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dtos.Cep
{
    public class CepDtoCreate
    {
        [Required(ErrorMessage = "CEP é obrigatório.")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "Logradouro é obrigatório.")]
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        [Required(ErrorMessage = "Municipio é obrigatório.")]
        public Guid MunicipioId { get; set; }
    }
}
