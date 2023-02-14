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

### Pré-requisitos e dependências
- .Net 6 ou superior
- Visual Studio 2022 ou superior
- MondoDb 6.0.4 2008R2Plus SSL ou superior

### Instruções para configurar o banco de dados
Para configurar o banco de dados é necessário seguir os seguintes passos\
1- Instalar o MongoDb 6.0.4 2008R2Plus SSL ou superior
2- Executar o projeto FeiraLivre.AtualizarBanco pelo Visual Studio

### Instruções para rodar a aplicação local
Para executar a aplicação é necessário seguir os seguintes passos:\
1- Abrir o terminal do git-bash\
2- Fazer o clone do projeto e mudar para a branch develop\
	*'git clone ssh://https://github.com/DiegoConegero/FeiraLivre.git'*\
	*'git checkout develop'*\
3- Abra o projeto no Visual Studio 2022 Community ou Enterprise(Para cobertura dos testes)\
4- Através do Terminal do Visual Studio ou PowerShell execute os seguintes comandos para carregar as dependências:\
	*'dotnet restore'*\
	*'dotnet build'*\
5- Execute a aplicação pelo Visual Studio ou pelo comando abaixo\
	*'dotnet run -c release'*\

### Instruções para visualizar a cobertura do código
1- Através do Terminal 'Developer PowerShell' execute os seguintes comandos\
	*'dotnet tool install --global dotnet-reportgenerator-globaltool'*\
	*'dotnet tool install --global dotnet-coverage'*\
	*'dotnet coverage collect dotnet test --output .\CoberturaTestes\output_coverage --output-format cobertura'*\
	*'reportgenerator -reports:.\CoberturaTestes\output_coverage.cobertura.xml -targetdir:".\CoberturaTestes\CoberturaReport" -reporttypes:Html -assemblyfilters:+FeiraLivre.Application'*\
	*'reportgenerator -reports:.\CoberturaTestes\output_coverage.cobertura.xml -targetdir:".\CoberturaTestes\CoberturaReport" -reporttypes:Html -assemblyfilters:+FeiraLivre.Core'*\
2- O resultado de cobertura será gerado na pasta \CoberturaTestes\CoberturaReport, dentro da estrutura da solução\

### Informações adicionais
Para a documentação da Api, acesse o Swagger no endpoint abaixo:\
**Localhost:**\
https://localhost:5000/swagger/index.html \

**Log:**\
O arquivo de log será gerado na pasta **LogTrace** dentro da estrutura do projeto
