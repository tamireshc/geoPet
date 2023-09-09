# GeoPet - Projeto Final Aceleração em C# .NET

O geoPet é uma API para geolocalização de pets.<br>
O objetivo deste projeto é proporcionar uma forma para que os tutores de pets possam armazenar as informções dos seus animalzinhos e 
buscar pelas suas últimas localizações.<br>
O Api também disponibiliza rotas que geram um QR Code com as informações da pessoa cuidadora e dos pets, caso o animal esteja perdido.<br>

## :mag: Tecnologias utilizadas
- Construção da API - [ASP.NET ](https://dotnet.microsoft.com/pt-br/apps/aspnet)<br>
- Banco de dados [SQL Server ](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) <br>
 - Autenticação - [JWT](https://jwt.io/) <br> 
 - Testes - [Fluent Assertions](https://fluentassertions.com/) e [xUnit.net](https://xunit.net/) <br> 

## 📋 Execute o projeto em sua máquina

Clone o repositório:

```
git clone git@github.com:tamireshc/geoPet.git
cd src/triytter
dotnet restore
dotnet run
```
## 🕵 Diagrama UML da API <br>
![Geo_Pets drawio](https://github.com/tamireshc/geoPet/assets/65035109/e4ee8a88-5390-454b-a76e-5ff9899cdcd4)

## 🧪 Executando os testes

Entre na pasta dos testes ```cd src/geoPet.Test``` e rode o comando:

```
dotnet test
```

### Testes de cobertura:<br>
Na pasta dos testes ```cd src/GeoPet.Test``` rode o comando:
```
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings 
```
Para ver os resultados da cobertura no formato HTML,instale o reportgenerator-globaltool com o seguinte comando:
```
dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.8.6
```
E rode o seguinte comando na pasta criada pelo Code Coverage para armazenar os resultados:
```
reportgenerator "-reports:coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
```
Então visualize os resultados do arquivo ```index.html``` no navegador

## :dart: Cobertura dos testes
*em contrução
O testes deste projeto contemplaram uma cobertura de ??? da linhas.<br>
As linhas não cobertas tratam de linhas de configurações.


## 🔎 Documentação da API





