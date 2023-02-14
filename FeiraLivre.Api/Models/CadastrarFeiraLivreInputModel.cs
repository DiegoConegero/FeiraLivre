
namespace FeiraLivre.Api.Models
{
    /// <summary>
    /// Input de cadastro de feira
    /// </summary>
    public class CadastrarFeiraLivreInputModel
    {
        /// <summary>
        /// Nome da feira
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Bairro da feira
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        /// Região da feira
        /// </summary>
        public string Regiao { get; set; }

        /// <summary>
        /// Distrito da feira
        /// </summary>
        public string Distrito { get; set; }
    }
}