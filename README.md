# FEIRAS LIVRES DE S�O PAULO

## Descri��o
Esta API � respons�vel por gerenciar o cadastro de feiras livres de S�o Paulo

### Principais Funcionalidades
Possui cinco endpoints para manuten��o de feiras, respons�veis por:
- Cadastrar nova feira
- Listar feiras por Distrito
- Listar todas as feiras de S�o Paulo
- Alterar feira
- Deletar feira

### Instru��es para rodar localmente
Para executar a aplica��o � necess�rio seguir os seguintes passos:\
1- Abrir o terminal do git-bash\
2- Fazer o clone do projeto e mudar para a branch develop\
'git clone ssh:// nao sei mais'\
'git checkout develop'\
3- Abra o projeto no Visual Studio 2022 Enterprise\
4- Atrav�s do Terminal 'Developer PowerShell' execute os seguintes comandos\
'dotnet restore' para carregar as depend�ncias do projeto\
'dotnet build' para compilar a aplica��o\
'dotnet run -c release' para rodar a aplica��o\

### Instru��es para visualizar a cobertura do c�digo
1- Atrav�s do Terminal 'Developer PowerShell' execute os seguintes comandos\
'dotnet tool install --global dotnet-reportgenerator-globaltool'\
'dotnet tool install --global dotnet-coverage'\
'dotnet coverage collect dotnet test --output .\CoberturaTestes\output_coverage --output-format cobertura'\
'reportgenerator -reports:.\CoberturaTestes\output_coverage.cobertura.xml -targetdir:".\CoberturaTestes\CoberturaReport" -reporttypes:Html -assemblyfilters:+FeiraLivre.Application'\
'reportgenerator -reports:.\CoberturaTestes\output_coverage.cobertura.xml -targetdir:".\CoberturaTestes\CoberturaReport" -reporttypes:Html -assemblyfilters:+FeiraLivre.Core'\
2- O resultado de cobertura ser� gerado na pasta \CoberturaTestes\CoberturaReport, dentro da estrutura da solu��o\




