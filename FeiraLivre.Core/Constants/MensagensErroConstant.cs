using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeiraLivre.Core.Constants
{
    public class MensagensErroConstant
    {
        public const string FeiraExistente = "Feira já cadastrada para o bairro.";

        public const string IdentificadorObrigatorio = "Identificador é obrigatório.";

        public const string TamanhoIdentificador = "Identificador deve ter 24 caracteres.";

        public const string NomeObrigatorio = "Nome é obrigatório.";

        public const string TamanhoNome = "Nome deve ter entre 3 e 100 caracteres.";

        public const string BairroObrigatorio = "Bairro é obrigatório.";

        public const string TamanhoBairro= "Bairro deve ter entre 3 e 100 caracteres.";

        public const string RegiaoObrigatorio = "Regiao é obrigatório.";

        public const string TamanhoRegiao = "Região deve ter entre 3 e 100 caracteres.";

        public const string DistritoObrigatorio = "Distrito é obrigatório.";

        public const string TamanhoDistrito= "Distrito deve ter entre 3 e 100 caracteres.";

        public const string CodigoNaoEncontrado = "Código não encontrado.";

        public const string NaoEncontrada = "Feira não encontrada.";

        public const string ListaNaoEncontrada = "Nenhuma feira encontrada.";

        public const string ListaNaoEncontradaDistrito = "Nenhuma feira encontrada para o distrito.";

        public const string NenhumaEncontrada = "Nenhuma feira encontrada.";
    }
}