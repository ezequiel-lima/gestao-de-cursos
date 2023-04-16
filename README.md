# API de cursos integrada com o ChatGPT

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/ezequiel-lima/gestao-de-cursos/blob/master/LICENSE.txt)

Este é um projeto em C#, consiste em uma API de cursos integrada com o ChatGPT. A API utiliza o framework ASP.NET Core e o banco de dados Microsoft SQL Server.

O projeto possui endpoints necessários para as operações CRUD de um curso: listagem de todos os cursos, listagem de um curso por ID, listagem de cursos por categoria, listagem de um curso por nome, criação de um novo curso e deletar um curso existente. Além disso, o controlador também possui um endpoint para atualizar os dados de um curso.

A API também possui endpoints para manipulação de categorias, permitindo listar todas as categorias, buscar uma categoria por ID ou nome, criar uma nova categoria, atualizar os dados de uma categoria existente e atualizar o estado de ativo em uma categoria.

O projeto utiliza a biblioteca OpenAI para gerar uma descrição do curso a partir do seu nome, que é usada na criação de um novo curso. 

Além das funcionalidades de CRUD, o projeto também inclui testes das viewmodels de curso e categoria. Esses testes garantem a integridade e a qualidade do código, permitindo a validação das entradas e saídas das viewmodels, bem como a verificação de possíveis erros ou comportamentos inesperados.

## Demonstração 

Endpoints

![Endpoints](https://user-images.githubusercontent.com/81567476/230727216-006a069f-ddbf-45c6-bf96-875c0b89abb3.png)

Demonstração da integração do OpenAI para criação de um curso

https://user-images.githubusercontent.com/81567476/230728748-a8bfd5e7-5e87-4e5f-a976-915bc2023ab6.mp4

Testes

![Tests](https://user-images.githubusercontent.com/81567476/232260613-84bd6d65-ee44-453c-8ce9-0233109913cc.png)

## Como executar o projeto
Para executar o projeto, siga as seguintes etapas:

1. Clone este repositório em sua máquina local usando o comando git clone `https://github.com/ezequiel-lima/gestao-de-cursos.git`
2. Abra o projeto no Visual Studio ou em outra IDE de sua preferência.
3. Configure a string de conexão do banco de dados no arquivo `appsettings.json`.
4. No Console do Gerenciador de Pacotes, execute o comando `Update-Database` para criar o banco de dados e suas tabelas.
5. Compile o projeto e execute a aplicação.
6. Use o Swagger ou outra ferramenta similar para testar os endpoints da API.

## Conclusão
Em resumo, a API de cursos integrada com o ChatGPT é uma solução eficiente para gestão de cursos, oferecendo operações CRUD para cursos e categorias, com a vantagem de descrições automáticas geradas pela integração com a API da OpenAI. Além disso, há saídas em formato JSON padronizadas.
