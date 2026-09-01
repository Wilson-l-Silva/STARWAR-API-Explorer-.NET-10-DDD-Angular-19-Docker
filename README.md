# STARWAR-API-Explorer-.NET-10-DDD-Angular-19-Docker
Projeto didático: uma API em .NET 10 aplicando Domain-Driven Design (DDD) que consome a SWAPI (Star Wars API), e um front-end em Angular 19 que consome essa API. Tudo containerizado com Docker.

Objetivo: você digitar/criar cada arquivo no Visual Studio 2026, entendendo o porquê de cada camada e cada linha de código.


Visão geral da arquitetura
SwapiExplorer/
├── src/
│   ├── SwapiExplorer.Domain          -> Entidades, Value Objects, Interfaces (regras de negócio puras)
│   ├── SwapiExplorer.Application     -> Casos de uso, DTOs, Services (orquestra o domínio)
│   ├── SwapiExplorer.Infrastructure  -> Implementações concretas (HttpClient para a SWAPI, cache, etc.)
│   └── SwapiExplorer.Api             -> Controllers, Program.cs, Swagger, DI (porta de entrada HTTP)
├── frontend/
│   └── swapi-explorer-web            -> Aplicação Angular 19
├── docker-compose.yml
└── SwapiExplorer.sln



Por que DDD aqui, mesmo sendo "só" um consumidor de API externa? Na prática de mercado, times aplicam DDD mesmo em serviços que consomem APIs externas para isolar o domínio (o que é um "Planeta", uma "Nave", um "Personagem" para o seu sistema) da forma como o dado chega de fora (a SWAPI pode mudar formato, mudar de URL, cair, etc.). Assim:

Domain não sabe que existe HTTP, JSON ou SWAPI. Só conhece regras de negócio.
Infrastructure é o único lugar que sabe que os dados vêm de https://swapi.dev/api.
Se amanhã você trocar a SWAPI por um banco de dados próprio, só a Infrastructure muda.

Essa é a regra de ouro do DDD/Clean Architecture: as dependências apontam sempre para dentro (para o Domain).