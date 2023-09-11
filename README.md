# GeoPet 

O GeoPet é uma API para geolocalização de pets.<br>
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
cd src/geoPet
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
<details>
<summary><strong>:boy:: Owers </strong></summary><br/>

 - Login de tutor

```
  POST /Login
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` |   nome do tutor. |
| `password` | `string` |   password de acesso. |

:white_check_mark: STATUS 200 OK<br>
:key: Retorna um TOKEN de autenticação

- Cadastro de tutor

```
  POST /Ower
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` |   nome do tutor. |
| `email` | `string` |   email do tutor. |
| `cep` | `string` |   cep do endereço do tutor do tutor. |
| `password` | `string` |   password de acesso. |

** Antes da inserção o cep é validado por meio da API [ViA CEP ](https://viacep.com.br/) <br>
** O email deve ser único.<br>
** A senha é salva em formato HASH.<br>
*Para mais detalhes ver o tópico dos casos de falha<br>

:white_check_mark: STATUS 200 OK

 - Atualizar tutor

 ```
  PUT /Ower/:id
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` |   nome do tutor. |
| `email` | `string` |   email do tutor. |
| `cep` | `string` |   cep do endereço do tutor do tutor. |
| `password` | `string` |   password de acesso. |

** Antes da inserção o cep é validado por meio da API [ViA CEP ](https://viacep.com.br/) <br>
** O email deve ser único.<br>
** A senha é salva em formato HASH.<br>
*Para mais detalhes ver o tópico dos casos de falha<br>


:white_check_mark: STATUS 200 OK

- Obter um tutor por seu id

```
  GET /Ower/:id
```
  Corpo da resposta: <br/>
  
  ```json
	{
{
	"owerId": 1,
	"name": "Yuri",
	"email": "yuri@gmail.com",
	"cep": "37356260",
	"pets": [
		{
			"petId": 5,
			"name": "JUJUBA",
			"age": 2,
			"size": 1,
			"breed": "VIRA-LATA",
			"owerId": 1,
		}
	]
}
	}
  ```

:white_check_mark: STATUS 200 OK

- Obter a listagem de todos os tutores

```
  GET /Ower
```

  Corpo da resposta: <br/>
  
  ```json
[
	{
		"owerId": 2,
		"name": "Maria",
		"email": "maria@gmail.com",
		"cep": "358376190"
	},
	{
		"owerId": 6,
		"name": "Alex Green",
		"email": "alex@gmail.com",
		"cep": "31567490"
	}
]
  ```
:white_check_mark: STATUS 200 OK

- Deletar um tutor por seu id

```
  DELETE /Ower/:id
```

  Corpo da resposta: <br/>
  
:white_check_mark: STATUS 200 OK
</details>

<details>
<summary><strong>:dog: :cat: Pet </strong></summary><br/>

- Cadastro de Pet

```
  POST /Pet
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` |   nome do Pet. |
| `age` | `integer` |   idade do tutor. |
| `size` | `Size` |   porte do pet. |
| `breed` | `string` |  raça do pet. |
| `owerId` | `int` |  id do tudor do pet. |


** Antes da inserção  é validado se o tutor do pet existe na base de dados e se o size é de um dos tipos: "SMALL", "MEDIUM" ou "LARGE".<br>
*Para mais detalhes ver o tópico dos casos de falha<br>

:white_check_mark: STATUS 200 OK

 - Atualizar Pet

 ```
  PUT /PET/:id
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `name` | `string` |   nome do Pet. |
| `age` | `integer` |   idade do tutor. |
| `size` | `Size` |   porte do pet. |
| `breed` | `string` |  raça do pet. |
| `owerId` | `int` |  id do tudor do pet. |


** Antes da inserção  é validado se o tutor do pet existe na base de dados e se o size é de um dos tipos: "SMALL", "MEDIUM" ou "LARGE".<br>
*Para mais detalhes ver o tópico dos casos de falha<br>


:white_check_mark: STATUS 200 OK

- Obter um Pet por seu id

```
  GET /Ower/:id
```
  Corpo da resposta: <br/>
  
  ```json
	{
  	"petId": 2,
  	"name": "Bilu",
  	"age": 2,
  	"size": 1,
  	"breed": "string",
  	"owerId": 2,
  	"positions": [
  		{
  			"positionId": 1002,
  			"latitude": "-19.9235803",
  			"longitude": "-43.9811087",
  			"dateTime": "2023-09-06T22:25:40.477",
  			"petId": 2
		}
	]
}
  ```

:white_check_mark: STATUS 200 OK

- Obter a listagem de todos os pets

```
  GET /Pet
```

  Corpo da resposta: <br/>
  
  ```json
[
 	{
 		"petId": 1,
 		"name": "Damiao",
 		"age": 2,
 		"size": 1,
 		"breed": "string",
 		"owerId": 4
 	},
 	{
 		"petId": 2,
 		"name": "Clarinho",
 		"age": 2,
 		"size": 1,
 		"breed": "string",
 		"owerId": 4
 	}
]
  ```
:white_check_mark: STATUS 200 OK

- Deletar um Pet por seu id

```
  DELETE /Pet/:id
```

 Corpo da resposta: <br/>
  
:white_check_mark: STATUS 200 OK
</details>

<details>
<summary><strong>:round_pushpin: Position </strong></summary><br/>

- Cadastro de uma posição de um Pet

```
  POST /Position
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `latitude` | `string` |   latitude da posição do Pet. |
| `longitude` | `string` |   latitude da posição do Pet. |
| `petId` | `int` |  id do pet. |


** Antes da inserção  é validado se o pet existe na base de dados.<br>
*Para mais detalhes ver o tópico dos casos de falha<br>

:white_check_mark: STATUS 200 OK

 - Atualizar a Posição de um Pet

 ```
  PUT /Position/:id
```
| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `latitude` | `string` |   latitude da posição do Pet. |
| `longitude` | `string` |   latitude da posição do Pet. |
| `dateTime` | `string` |   data em que o pet foi visto nesta posição. |
| `petId` | `int` |  id do pet. |

** Antes da inserção  é validado se o pet existe na base de dados.<br>
*Para mais detalhes ver o tópico dos casos de falha<br>

:white_check_mark: STATUS 200 OK

- Obter a última posição de Pet por seu id

```
  GET //Position/Pet/:id
```
  Corpo da resposta: <br/>
  
  ```json
{
	"positionId": 12,
	"latitude": "15,23456",
	"longitude": "30,67890",
	"dateTime": "2023-09-08T09:09:50.5213263",
	"petId": 4
}
  ```

:white_check_mark: STATUS 200 OK

- Obter a listagem de todas as posições cadastradas

```
  GET /Position
```

  Corpo da resposta: <br/>
  
  ```json
[
	{
		"positionId": 3,
		"latitude": "15,23456",
		"longitude": "30,67890",
		"dateTime": "2023-09-06T22:25:40.477",
		"petId": 2
	},
	{
		"positionId": 4,
		"latitude": "15,23456",
		"longitude": "30,67890",
		"dateTime": "2023-09-06T22:25:40.477",
		"petId": 2
	}
]

  ```
:white_check_mark: STATUS 200 OK

- Obter uma posição por seu id

```
  GET /Position/:id
```

  Corpo da resposta: <br/>
  
  ```json
	{
		"positionId": 3,
		"latitude": "15,23456",
		"longitude": "30,67890",
		"dateTime": "2023-09-06T22:25:40.477",
		"petId": 2
	}
  ```
:white_check_mark: STATUS 200 OK

- Deletar um Position por seu id

```
  DELETE /Position/:id
```

 Corpo da resposta: <br/>
  
:white_check_mark: STATUS 200 OK
</details>

<details>
<summary><strong>:checkered_flag: QR Code </strong></summary><br/>

</details>

## :smiley: Extra 
*em construção
![Captura de tela 2023-09-08 195040](https://github.com/tamireshc/geoPet/assets/65035109/d3d82bd9-7d28-4b86-a36e-aa8cd01c1454)


