# 🎮 GamesWebApi

Este projeto tem como objetivo praticar conceitos do ASP.NET Core, Entity Framework Core, arquitetura de código limpa e introdução a APIs REST.  

Foi implementado um CRUD completo (Create, Read, Update e Delete) com o tema de jogos.

Nele, é possível cadastrar:
- 🎮 Jogos
- 🏭 Produtoras
- 🏷 Gêneros

Com esses dados, é possível relacionar:
- Um **jogo a uma produtora** (relação 1:N)
- Um **jogo a vários gêneros** (relação N:N)

A funcionalidade final permite ao usuário **consultar jogos semelhantes** ao que ele escolheu, com base nos gêneros em comum.

## 📦 Funcionalidades

- Cadastro, edição, exclusão e listagem de jogos, produtoras e gêneros
- Relacionamentos entre entidades (1:N e N:N)
- Consulta de jogos semelhantes por gêneros em comum
- Documentação via Swagger

## 🛠 Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server Express
- Swagger (Swashbuckle)

## 📬 Endpoints Principais

|Verbo  |Rota                             |  Descrição                                                       |
|-------|---------------------------------|------------------------------------------------------------------|
|GET    |/api/Jogo/ListarJogosSemelhantes|Recebe como parâmentro o id de um jogo e devolve jogos semelhantes, utilizando como filtro os gêneros em comum.                                                                                            |
|GET    |/api/Jogo/ListarJogos            |Lista todos os jogos cadastrados.                                 |
|POST   |/api/Jogo/CadastrarJogo          |Cria um novo jogo.                                                |
|PUT    |/api/Jogo/EditarJogo             |Edita o jogo, incluindo a produtora e os gêneros cadastrados nele.|
|DELETE |/api/Jogo/ExcluirJogo            |Deleta o jogo                                                     |
|POST   |/api/Produtora/CadastrarProdutora|Cadastra uma produtora                                            |
|POST   |/api/Genero/CadastrarGenero      |Cadastra um novo gênero                                           |

## 📌 Observações

- Este projeto tem fins educativos e pode servir de base para estudos e portfólio.
- Sinta-se à vontade para contribuir, sugerir melhorias ou relatar problemas!

## 🧑‍💻 Autor

- Matheus Rodrigues — [GitHub](https://github.com/MatheusPRodrigues)
