FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChatBot.Api/ChatBot.Api.csproj", "ChatBot.Api/"]
RUN dotnet restore "ChatBot.Api/ChatBot.Api.csproj"
COPY . .
WORKDIR "/src/ChatBot.Api"
RUN dotnet build "ChatBot.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatBot.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatBot.Api.dll"]
