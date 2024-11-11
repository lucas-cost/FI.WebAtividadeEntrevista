using FI.AtividadeEntrevista.DML;
using System.ComponentModel.DataAnnotations;

namespace FI.WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiário
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// CPF do Beneficiário
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Digite um CPF válido no formato XXX.XXX.XXX-XX")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome do Beneficiário
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// ID do Cliente associado
        /// </summary>
        [Required]
        public long ClienteId { get; set; }

        /// <summary>
        /// Action (Register, Update, Remove)
        /// </summary>
        public string Action { get; set; }
    }
}
