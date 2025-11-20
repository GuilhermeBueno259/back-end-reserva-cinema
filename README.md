# back-end-reserva-cinema

## Requisitos para rodar a aplicação
 - SQL Server 2022
 - SDK .NET 9.0
 - Git 2.44.0

## Como rodar a aplicação
 - Abra um novo terminal
 - Rode dentro do novo terminal o comando ``` https://github.com/GuilhermeBueno259/back-end-reserva-cinema.git ```
 - Após isso rode o comando ``` cd back-end-reserva-cinema ``` para entrar no diretório da aplicação
 - Certifique-se de que o SQL Server esteja rodando, e acesse o servidor pela ferramenta de sua escolha
 - Após acessar o servidor do banco de dados, crie um novo banco chamado ReservaCinema
 - Agora retorne para o terminal onde está o código do projeto e rode os seguintes comandos ``` dotnet add package Microsoft.EntityFrameworkCore ```, ``` dotnet add package Microsoft.EntityFrameworkCore.SqlServer ``` e ``` Microsoft.EntityFrameworkCore.Tools ```, estes comandos são responsáveis por instalar os pacotes necessários para o funcionamento do ORM EntityFrameworkCore
 - Então rode o comando ``` dotnet ef database update ```, esse comando é responsável por criar as tabelas dentro do banco de dados
 - Agora rode o comando ``` dotnet run ```, esse comando é o responsável por fazer a nossa aplicação rodar

Obs.: Dentro do arquivo ReservaCinema.http existe o modelo de cada uma das requisições da API