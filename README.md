# OpenCoreMMO-Web

A modular, scalable C# web solution for managing and visualizing data from the [OpenCoreMMO server](https://github.com/OpenCoreMMO/OpenCoreMMO), built with clean architecture principles and modern UI components.

---

## 🚀 Technologies

- **.NET 9.0** – Modern, high-performance backend
- **MudBlazor** – Elegant, material-inspired UI for Blazor
- **xUnit** & **FluentAssertions** – Robust unit and integration testing
- **Microsoft.AspNetCore.Mvc.Testing** – API test harness

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [JetBrains Rider](https://www.jetbrains.com/rider/) or Visual Studio 2022+

### Clone & Run

```sh
git clone https://github.com/your-org/OpenCoreMMO-Web.git
cd OpenCoreMMO-Web/src
dotnet run --project "OCM.Web.Admin"
```

### Running Tests

You can run all tests from your IDE or via terminal:

```sh
dotnet test NeoServer.WebApi.Tests/NeoServer.WebApi.Tests.csproj
```

---

## 🧩 Project Structure

- **NeoServer.WebApi** – Main ASP.NET Core Web API project
- **NeoServer.WebApi.Tests** – API endpoint and integration tests
- **NeoServer.Core** – Core business logic and domain models
- **NeoServer.Infrastructure** – Data access and external integrations
- **NeoServer.Application** – Application layer for use cases and orchestration
- **NeoServer.Shared** – Shared utilities and abstractions

---

## ✨ Features

- Todo..

---

## 📚 Documentation & Resources

- [OpenCoreMMO Server](https://github.com/OpenCoreMMO/OpenCoreMMO) – Main server repository
- [MudBlazor Documentation](https://mudblazor.com/)
- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core/)

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Open a pull request

We welcome contributions of all kinds!

---

## 📝 License

This project is licensed under the MIT License.

---

## 💬 Support

For questions or support, please [open an issue](https://github.com/your-org/OpenCoreMMO-Web/issues) or join the project discussions.
