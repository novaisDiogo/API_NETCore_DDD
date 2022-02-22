using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dtos.Municipio
{
    public class MunicipioDtoUpdate
    {
        [Required(ErrorMessage = "Id é campo um obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome de município é campo obrigatório.")]
        [StringLength(60, ErrorMessage = "Nome de município deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "O código do IBGE inválido.")]
        public int CodIBGE { get; set; }
        [Required(ErrorMessage = "Código de UF é campo obrigatório.")]
        public Guid UfId { get; set; }
    }
}
