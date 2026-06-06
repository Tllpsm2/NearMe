# NearMe

Aplicação em **Blazor Server** para exploração do **Azure Maps**, com foco em geolocalização, mapas interativos e construção de um localizador de lojas.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![Azure Maps](https://img.shields.io/badge/Azure%20Maps-0078D4?logo=microsoftazure&logoColor=white)](https://azure.microsoft.com/en-us/products/azure-maps)
[![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)](#status)
[![License](https://img.shields.io/badge/license-MIT-green)](./LICENSE)

> [!IMPORTANT]
> Credenciais e segredos devem ser armazenados exclusivamente em **User Secrets**. Nenhuma informação sensível deve ser commitada em arquivos versionados.

---

## Índice

- [Visão geral](#visão-geral)
- [Requisitos](#requisitos)
- [Status](#status)
- [Funcionalidades](#funcionalidades)
- [Roadmap](#roadmap)
- [Tecnologias](#tecnologias)
- [Configuração local](#configuração-local)
- [Execução](#execução)
- [Licença](#licença)

---

## Visão geral

O NearMe é uma aplicação em fase de prova de conceito. O objetivo atual é consolidar a infraestrutura de mapas, autenticação e geolocalização antes da implementação completa do fluxo de busca de lojas por proximidade.

O projeto serve como plataforma de integração entre **Blazor Server**, **Azure Maps** e **Microsoft Entra ID**, concebida para evoluir em um localizador de lojas completo.

---

## Requisitos

- [.NET SDK 10.0.201+](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (para execução do SQL Server localmente)
- [Conta Azure](https://azure.microsoft.com/free/) com acesso ao Azure Maps
- Registro de aplicação no Microsoft Entra ID

---

## Status

**Fase atual:** Prova de conceito / base funcional.

Os componentes técnicos principais descritos em [Funcionalidades](#funcionalidades) já estão implementados. O próximo marco é a busca de lojas por proximidade com ordenação por distância (ver [Roadmap](#roadmap)).

---

## Funcionalidades

- Aplicação Blazor Server em .NET 10
- Integração com Azure Maps via `AzureMapsControl.Components`
- Autenticação com Microsoft Entra ID
- Páginas de teste para mapas estáticos e interativos
- Geolocalização do navegador via `Blazor.Geolocation`
- Persistência de lojas com Entity Framework Core e SQL Server
- Suporte a dados geoespaciais com NetTopologySuite
- Interface de gerenciamento de lojas com Syncfusion Blazor
- Geocoding de endereços via Azure Maps

---

## Roadmap

- [ ] Busca de lojas por proximidade com ordenação por distância
- [ ] Integração entre lista de resultados e mapa interativo
- [ ] Cache de geolocalização em memória por sessão
- [ ] Definir e implementar estratégia de deploy

---

## Tecnologias

**Core**

- [.NET 10](https://dotnet.microsoft.com/) · [Blazor Server](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)

**Mapas e autenticação**

- [Azure Maps](https://azure.microsoft.com/en-us/products/azure-maps) · `AzureMapsControl.Components`
- [Microsoft Entra ID](https://www.microsoft.com/security/business/identity-access/microsoft-entra-id) · [Azure Identity](https://learn.microsoft.com/dotnet/api/overview/azure/identity-readme) · [Microsoft.Identity.Client](https://learn.microsoft.com/dotnet/api/microsoft.identity.client)

**Dados e geolocalização**

- [Blazor.Geolocation](https://github.com/AspNetMonsters/Blazor.Geolocation)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [NetTopologySuite](https://github.com/NetTopologySuite/NetTopologySuite)

**UI e banco de dados**

- [Syncfusion Blazor](https://www.syncfusion.com/blazor-components)
- SQL Server via Docker

**Recursos Azure utilizados**

- Azure Maps — renderização de mapas, busca e interatividade
- Azure Budget — monitoramento de consumo
- Action Group / Runbook — automação vinculada a alertas de orçamento

---

## Configuração local

> Os passos abaixo pressupõem que o recurso Azure Maps foi criado e configurado para autenticação via Microsoft Entra ID.

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/nearme.git
cd nearme
```

### 2. Inicie o container do banco de dados

```bash
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=SuaSenhaForte!" \
  -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

### 3. Configure os User Secrets

```bash
cd src/NearMe
dotnet user-secrets init

dotnet user-secrets set "AzureMaps:ClientId"       "<client-id>"
dotnet user-secrets set "AzureMaps:AadAppId"        "<app-id>"
dotnet user-secrets set "AzureMaps:AadTenantId"     "<tenant-id>"
dotnet user-secrets set "AzureMaps:AadClientSecret" "<client-secret>"
```

---

## Execução

```bash
dotnet restore
dotnet run --project src/NearMe/NearMe.csproj
```

A aplicação estará disponível em `https://localhost:5001` ou na porta indicada no console.

---

## Licença

Este projeto é distribuído sob a [Licença MIT](./LICENSE).
