# teste_drugovich
Gerenciamento de Clientes

Usei Migration para modelar e criar a base de dados, usei o sql server.
Basta executar os comandos do migration para criar o banco de dados para você, é necessário ter o sql server instalado.

No princípio eu ia utilizar docker, mas teria que instalar o sql server no docker, então optei por não utilizar o docker e rodar local mesmo, utilizando meu banco local.

Dentro do projeto tem um arquivo chamado Gerenciador de Clientes - Tests.postman_collection.json, é uma collection do postman para fazer os testes da API.

Tem um endpoint para criar o gerente, e outro para autenticar passando o email e senha do gerente.
Após autenticar a collection já salva o token para utilizar o restante da API.

Espero que gostem.