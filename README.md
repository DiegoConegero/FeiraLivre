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

### Pr�-requisitos e depend�ncias
- .Net 6 ou superior
- Visual Studio 2022 ou superior
- MondoDb 6.0.4 2008R2Plus SSL ou superior

### Instru��es para configurar o banco de dados
Para configurar o banco de dados � necess�rio seguir os seguintes passos\
1- Instalar o MongoDb 6.0.4 2008R2Plus SSL ou superior
2- Executar o projeto FeiraLivre.AtualizarBanco pelo Visual Studio

### Instru��es para rodar a aplica��o local
Para executar a aplica��o � necess�rio seguir os seguintes passos:\
1- Abrir o terminal do git-bash\
2- Fazer o clone do projeto e mudar para a branch develop\
	*'git clone ssh://https://github.com/DiegoConegero/FeiraLivre.git'*\
	*'git checkout develop'*\
3- Abra o projeto no Visual Studio 2022 Community ou Enterprise(Para cobertura dos testes)\
4- Atrav�s do Terminal do Visual Studio ou PowerShell execute os seguintes comandos para carregar as depend�ncias:\
	*'dotnet restore'*\
	*'dotnet build'*\
5- Execute a aplica��o pelo Visual Studio ou pelo comando abaixo\
	*'dotnet run -c release'*\

### Instru��es para visualizar a cobertura do c�digo
1- Atrav�s do Terminal 'Developer PowerShell' execute os seguintes comandos\
	*'dotnet tool install --global dotnet-reportgenerator-globaltool'*\
	*'dotnet tool install --global dotnet-coverage'*\
	*'dotnet coverage collect dotnet test --output .\CoberturaTestes\output_coverage --output-format cobertura'*\
	*'reportgenerator -reports:.\CoberturaTestes\output_coverage.cobertura.xml -targetdir:".\CoberturaTestes\CoberturaReport" -reporttypes:Html -assemblyfilters:+FeiraLivre.Application'*\
	*'reportgenerator -reports:.\CoberturaTestes\output_coverage.cobertura.xml -targetdir:".\CoberturaTestes\CoberturaReport" -reporttypes:Html -assemblyfilters:+FeiraLivre.Core'*\
2- O resultado de cobertura ser� gerado na pasta \CoberturaTestes\CoberturaReport, dentro da estrutura da solu��o\

### Informa��es adicionais
Para a documenta��o da Api, acesse o Swagger no endpoint abaixo:\
**Localhost:**\
https://localhost:5000/swagger/index.html \

**Log:**\
O arquivo de log ser� gerado na pasta **LogTrace** dentro da estrutura do projeto
