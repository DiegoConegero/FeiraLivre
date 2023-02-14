
namespace FeiraLivre.Api.ViewModels
{
    /// <summary>
    /// View de feira livre
    /// </summary>
    public class FeiraLivreViewModel
    {
        /// <summary>
        /// Id da feira
        /// </summary>
        public string Id { get; set; }

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