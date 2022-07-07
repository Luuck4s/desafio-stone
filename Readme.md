<h1  align="center">
<img  alt="Desafio Stone"  title="#Desafio Stone"  src=".github/images/logo-stone.png"  width="250px" />
</h1>

<h4  align="center">
	💚 Desafio Stone
</h4>

<p  align="center">
<a  href="#telescope-projeto">🔭 Projeto</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp
<a  href="#computer-tecnologias"> 💻 Tecnologias</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
<a  href="#-getting-started">🤠 Getting Started </a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
<a  href="#sparkles-requisitos">:sparkles: Requisitos </a>
</p>

[![Build API](https://github.com/Luuck4s/desafio-stone/actions/workflows/pipelines-github-actions.yml/badge.svg)](https://github.com/Luuck4s/desafio-stone/actions/workflows/pipelines-github-actions.yml)


## :telescope: Projeto
 
 
Nesse desafio deve-se criar um serviço para cadastramento do Censo demográfico de uma região, onde uma pessoa deverá ser cadastrada informando os dados de Nome, sobrenome, cor, filiação e filhos, escolaridade.

Esse serviço deve permitir alguns tipos de buscas para traçar dados estatísticos como:
- Percentual de Pessoas com mesmo nome de uma determinada região.
- Arvore genealógica de um indivíduo até o nível solicitado.
- Concatenação de filtros para extração de dados. Ex: Quero o número de indivíduos Negros, com
formação superior e com Nome João.


## :computer: Tecnologias

  

**:satellite: Backend**

 
- .NET 6.0
- MongoDB
- SignalR


**:computer: Web**
 
- Angular


## 🤠 Getting Started

Você precisa clonar o repositório e pode fazer isso digitando em seu terminal `$ git clone https://github.com/Luuck4s/desafio-stone.git`.
Será necessário ter instalado:
- Docker
- Docker Compose

Para iniciar o projeto execute:
```
$ git clone https://github.com/Luuck4s/desafio-stone
$ cd desafio-stone
$ docker-compose up -d
```

Para acessar o projeto:

### Api Swagger
```http://localhost:5000/swagger/```

### Web App
```http://localhost:9000/```


## :sparkles: Requisitos 

 - [x]  Utilizar .NET Core em sua última versão para o desenvolvimento;
 - [x] Todo ambiente deve ser containerizado (Docker) e preferencialmente executado com todos
serviços dependentes em um único comando;
 - [x] Para o armazenamento das informações utilizar qualqluer banco NoSQL (Elasticsearch, MongoDB,
RavenDB, etc), da sua preferência;
 - [x] Testes são muito importantes. Crie alguns casos de testes unitários (com uso de Mock) e de
integração;
 - [x] Ter um repositório organizado, documentação e fácil de utilizar, um ou poucos comandos para
levantar todo ambiente e suas dependências;
 - [x] O repositório deve ser públlico e ter integração com qualquer serviço de CI (seja Azure DevOps,
AppVeyor, CircleCI, etc);

