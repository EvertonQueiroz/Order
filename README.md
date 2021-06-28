Aplicação destinada a registrar pedidos e registrar sua aprovaçã ou reprovação.

# Pre-requisito
- .NET 5

# Arquitetura
O sistema foi construído em 3 camadas, implementando o padrão arquitetural CQRS.

 1. **API** - Camada responsável por fazer a interface de interação com o usuário. Implementada em ASP.NET 5.
 2. **Domain** - Lógicas de negócio e entidades.
 3. **Data Access** - Implementação dos repositórios com Entity Framework Core 5.


# Execução
Acesse a pasta do projeto e execute o seguinte comando: 
```
dotnet run --project Order.API\Order.API.csproj
```

# Execução dos testes
Acesse a pasta do projeto e execute o seguinte comando: 
```
dotnet test 
```
