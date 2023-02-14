# FEIRAS LIVRES DE SÃO PAULO

## Descrição
Esta API é responsável por gerenciar o cadastro de feiras livres de São Paulo

### Principais Funcionalidades
Possui cinco endpoints para manutenção de feiras, responsáveis por:
- Cadastrar nova feira
- Listar feiras por Distrito
- Listar todas as feiras de São Paulo
- Alterar feira
- Deletar feira

### Instruções para rodar localmente
Para executar a aplicação é necessário seguir os seguintes passos:\
1- Abrir o terminal do git-bash\
2- Fazer o clone do projeto e mudar para a branch develop\
'git clone ssh:// nao sei mais'\
'git checkout develop'\
3- Abra o projeto no Visual Studio 2022 Enterprise\
4- Através do Terminal 'Developer PowerShell' execute os seguintes comandos\
'dotnet restore' para carregar as dependências do projeto\
'dotnet build' para compilar a aplicação\
'dotnet run -c release' para rodar a aplicação\

### Instruções para visualizar a cobertura do código
1- Através do Terminal 'Developer PowerShell' execute os seguintes comandos\
'dotnet tool install --global dotnet-reportgenerator-globaltool'\
'dotnet tool install --global dotnet-coverage'\
'dotnet coverage collect dotnet test --output .\CoberturaTestes\output_coverage --output-format cobertura'\
'reportgenerator -reports:.\CoberturaTestes\output_coverage.cobertura.xml -targetdir:".\CoberturaTestes\CoberturaReport" -reporttypes:Html -assemblyfilters:+FeiraLivre.Application'\
'reportgenerator -reports:.\CoberturaTestes\output_coverage.cobertura.xml -targetdir:".\CoberturaTestes\CoberturaReport" -reporttypes:Html -assemblyfilters:+FeiraLivre.Core'\
2- O resultado de cobertura será gerado na pasta \CoberturaTestes\CoberturaReport, dentro da estrutura da solução\




