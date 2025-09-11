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
dotnet test
```

---

## 🧩 Project Structure

- **OCM.Web.Admin** – Main ASP.NET Core Blazor Server admin interface
- **OCM.WebApi** – ASP.NET Core Web API project
- **OCM.Application** – Application layer with use cases, commands, and queries
- **OCM.Data** – Data access layer with repositories and entities
- **OCM.IoC** – Dependency injection configuration
- **tests/** – Unit and integration tests

---

## ✨ Features

### 🌍 World Management
- **List Worlds**: Paginated display with server-side data loading
- **Create World**: Full form with validation and duplicate prevention
- **Edit World**: Load and update existing worlds with API integration
- **Delete World**: Confirmation dialogs with soft delete support

### 🏠 Admin Interface
- **Modern UI**: Clean, responsive design with MudBlazor
- **Form Validation**: Required fields and IPv4 format validation
- **User Feedback**: Toast notifications and error handling
- **Navigation**: Intuitive routing between management sections

### 🔧 Technical Features
- **Clean Architecture**: CQRS with MediatR, Repository pattern
- **Data Integrity**: Soft deletes, unique constraints, type safety
- **API Integration**: Full CRUD operations with proper error handling

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
