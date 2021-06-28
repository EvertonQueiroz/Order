Aplicação destinada :
- Registrar pedidos;
- Registrar sua aprovação ou reprovação.

# Pré-requisito
- .NET 5

# Arquitetura
O sistema foi construído em 3 camadas, implementando o padrão arquitetural CQRS.

 1. **API** - Camada responsável por fazer a interface de interação com o usuário. Implementada em ASP.NET 5.
 2. **Domain** - Camada responsável por implementar a lógica de negócio.
 3. **Data Access** - Camada responsável pela persistência do dado, através da implementação dos repositórios estabelecidos pela camada de domínio. Para o projeto foi utilizado o ORM: Entity Framework Core 5.


# Execução
Acesse a pasta do projeto e execute o seguinte comando: 
```
dotnet run --project .\Order.API\Order.API.csproj
```
No navegador acesse o endereço:
```
https://localhost:5001/swagger/index.html
```


# Execução dos testes
Acesse a pasta do projeto e execute o seguinte comando: 
```
dotnet test 
```
