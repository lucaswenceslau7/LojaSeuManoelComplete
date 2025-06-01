# Usa a imagem oficial do .NET 8 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código e compila a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Usa a imagem runtime do .NET 8 para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expõe a porta da aplicação
EXPOSE 5000

# Define o comando de entrada
ENTRYPOINT ["dotnet", "LojaSeuManoel.WebApi.dll"]
