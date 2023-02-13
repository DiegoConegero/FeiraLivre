using FeiraLivre.Core.Constants;
using FluentValidation;

namespace FeiraLivre.Core.Entities.Validators
{
    public class FeiraLivreEntityValidator : AbstractValidator<FeiraLivreEntity>
    {
        public FeiraLivreEntityValidator()
        {
            RuleFor(r => r.Nome)
                .NotEmpty().WithMessage(MensagensErroConstant.NomeObrigatorio)
                .Length(3, 100).WithMessage(MensagensErroConstant.TamanhoNome);

            RuleFor(r => r.Bairro)
                .NotEmpty().WithMessage(MensagensErroConstant.BairroObrigatorio)
                .Length(3, 100).WithMessage(MensagensErroConstant.TamanhoBairro);

            RuleFor(r => r.Regiao)
                .NotEmpty().WithMessage(MensagensErroConstant.RegiaoObrigatorio)
                .Length(3, 100).WithMessage(MensagensErroConstant.TamanhoRegiao);

            RuleFor(r => r.Distrito)
                .NotEmpty().WithMessage(MensagensErroConstant.DistritoObrigatorio)
                .Length(3, 100).WithMessage(MensagensErroConstant.TamanhoDistrito);
        }
    }
}