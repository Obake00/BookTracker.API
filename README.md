Ola! Bem vindo a aplicação BookTracker,
Projeto iniciado para proposta de teste pratico!

Padrão do projeto realizado em camadas, onde tive menos dificuldade de aplicação, embora algumas dificuldades fossem algumas partes de codigo que tive que utilizar de I.A para resolver, principalmente na ligação do banco de dados com a API proposta
onde tive que utilizar de varios testes para rodar a aplicação.
projeto rodando em Docker como pedido

Instruçoes basicas para uso da aplicação:
- Clone do repositorio para uso local em maquina
- ( https://github.com/Obake00/BookTracker.API.git )
- Abra um terminal na pasta raiz do projeto na propria maquina
- Execute o comando "docker compose up", faz subir todo o banco e a aplicação
- Use o link para acesso ao Swagger ( http://localhost:8080/swagger/index.html )
- Teste da API proposta no proprio swagger
- Principais campos de uso na API:
- "POST /api/auth/register" -> Registra o usuario na aplicação
- "POST /api/auth/login" -> Loga usuario via autorização de token 
- "GET /api/books/search" -> Faz busca de livros por genero na biblioteca da API
- "POST /api/books/import" -> Envia/guarda livros no banco de dados local via estrutura JSON
- "GET /api/books" -> Busca pelo livro no banco de dados
- "POST /api/books" -> Adiciona em especial um livro ao banco
- "GET /api/books/{id}" -> Busca livros por ID
- "PUT /api/books/{id}" -> Altera info sobre o livro por ID
- "DELETE /api/books/{id}" -> Deleta um livro da base de dados do banco via ID
- "PATCH /api/books/{id}/status" -> Altera o status de leitura do livro 1 - Quero, 2 - Lendo, 3 - Ja li via ID
