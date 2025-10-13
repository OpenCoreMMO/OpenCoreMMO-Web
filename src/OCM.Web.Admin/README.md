# OpenCoreMMO Web Admin

A modern web administration interface for the OpenCoreMMO game server, built with ASP.NET Core Blazor Server and MudBlazor.

## Architecture Overview

This project follows Clean Architecture principles with a layered approach to ensure maintainability, testability, and separation of concerns.

### Project Structure

```
src/OCM.Web.Admin/
├── Components/           # Reusable Blazor components
│   ├── Layout/          # Main layout components (NavMenu, MainLayout)
│   └── Pages/           # Page components (Error, etc.)
├── Features/            # Feature-based organization
│   ├── Account/         # Account management features
│   ├── Player/          # Player management features
│   ├── Guild/           # Guild management features
│   ├── World/           # World management features
│   ├── IpBans/          # IP ban management features
│   └── BugReports/      # Bug report management features
├── Controllers/         # ASP.NET Core controllers for authentication
├── Data/               # Data access layer (Identity, etc.)
├── Services/           # Application services
└── wwwroot/            # Static assets (images, CSS, JS)
```

### Architectural Layers

#### 1. Presentation Layer (OCM.Web.Admin)
- **Framework**: ASP.NET Core Blazor Server
- **UI Library**: MudBlazor (Material Design components)
- **Authentication**: ASP.NET Core Identity with custom authentication scheme
- **State Management**: Blazor component state with dependency injection

**Key Components:**
- **Pages**: Feature-specific pages organized under `/Features/`
- **Components**: Reusable UI components under `/Components/`
- **Layout**: Main layout with navigation and theming
- **Authentication**: Login/logout handled via controllers and claims-based identity

#### 2. Application Layer (OCM.Application)
- **Pattern**: CQRS (Command Query Responsibility Segregation)
- **Mediator**: MediatR for request/response handling
- **Validation**: FluentValidation for request validation
- **Responses**: Structured response objects with success/error handling

**Key Patterns:**
- **Commands**: Handle write operations (Create, Update, Delete)
- **Queries**: Handle read operations (Get, List, Search)
- **Requests**: Input models for commands and queries
- **Validators**: Input validation rules
- **Response Models**: Structured output data

#### 3. Infrastructure Layer (OCM.Data)
- **ORM**: Entity Framework Core
- **Database**: PostgreSQL/SQLite support
- **Repositories**: Data access patterns
- **Entities**: Domain models mapped to database tables
- **Configurations**: Entity Framework configurations

**Key Components:**
- **Context**: `NeoContext` - Main database context
- **Repositories**: Generic and specific data access classes
- **Entities**: Database table representations
- **Migrations**: Database schema management

#### 4. Dependency Injection (OCM.IoC)
- **Container**: Microsoft.Extensions.DependencyInjection
- **Registration**: Module-based service registration
- **Scopes**: Singleton, Scoped, and Transient services

### Key Design Patterns

#### CQRS (Command Query Responsibility Segregation)
- **Commands**: Handle state-changing operations
  - `ChangePasswordCommand` - Updates user password
  - `CreateAccountCommand` - Creates new accounts
  - `BanAccountCommand` - Bans user accounts
- **Queries**: Handle data retrieval operations
  - `GetAccountsRequest` - Retrieves account list with filtering
  - `GetPlayersRequest` - Retrieves player data

#### Repository Pattern
- **Generic Repository**: `BaseRepository<T>` for common CRUD operations
- **Specific Repositories**: Domain-specific data access (e.g., `AccountRepository`, `PlayerRepository`)
- **Interface Segregation**: Repository interfaces for dependency injection

#### Mediator Pattern
- **MediatR**: Decouples request senders from handlers
- **Request/Response**: Typed request-response pairs
- **Pipeline Behaviors**: Cross-cutting concerns (logging, validation, etc.)

### Authentication & Authorization

#### Authentication Flow
1. **Login**: User submits credentials via login form
2. **Validation**: `LoginCommand` validates credentials against database
3. **Claims**: Successful login creates claims-based identity
4. **Cookies**: Authentication persisted via encrypted cookies
5. **Session**: Claims available throughout user session

#### Authorization
- **Role-Based**: Users assigned roles (Administrator, Player, etc.)
- **Claims-Based**: Permissions checked via user claims
- **UI-Level**: Components conditionally rendered based on user roles

### Data Flow Example

#### Password Change Flow:
1. **UI** (`Profile.razor`): User fills password change form
2. **Validation**: Client-side validation via MudBlazor forms
3. **Command**: `ChangePasswordCommand` created with user input
4. **Mediator**: Command sent via MediatR
5. **Handler**: `ChangePasswordCommandHandler` processes request
6. **Repository**: `AccountRepository` updates database
7. **Response**: Success/error response returned to UI
8. **Feedback**: User notified of result via snackbar/alert

### Development Guidelines

#### Naming Conventions
- **Commands**: `ActionEntityCommand` (e.g., `ChangePasswordCommand`)
- **Queries**: `GetEntityRequest` (e.g., `GetAccountsRequest`)
- **Validators**: `EntityValidator` (e.g., `ChangePasswordValidator`)
- **Repositories**: `EntityRepository` (e.g., `AccountRepository`)

#### File Organization
- **Features**: Group related components, commands, and queries together
- **Shared Components**: Common UI elements in `/Components/`
- **Services**: Business logic in `/Services/`
- **Models**: Data transfer objects in feature folders

#### Error Handling
- **Validation Errors**: Handled at request level with FluentValidation
- **Business Errors**: Custom error messages via `OutputResponse`
- **System Errors**: Global exception handling middleware
- **UI Feedback**: User-friendly error messages via MudBlazor alerts

### Technologies Used

- **Backend**: ASP.NET Core 8.0, C# 12
- **Frontend**: Blazor Server, MudBlazor
- **Database**: Entity Framework Core, PostgreSQL/SQLite
- **Architecture**: Clean Architecture, CQRS, Repository Pattern
- **Testing**: xUnit, Moq (in separate test projects)
- **Build**: MSBuild, dotnet CLI

### Getting Started

1. **Prerequisites**: .NET 8.0 SDK, PostgreSQL/SQLite database
2. **Configuration**: Update `appsettings.json` with database connection
3. **Database**: Run migrations to create schema
4. **Run**: `dotnet run` from the project directory
5. **Access**: Navigate to `https://localhost:5001` (default HTTPS port)

### API Endpoints

- **Authentication**: `/Account/Login`, `/Account/Logout`
- **Admin UI**: `/` (Blazor Server app)
- **Features**: `/accounts`, `/players`, `/guilds`, `/worlds`, `/ip-bans`, `/bug-reports`, `/profile`

This architecture ensures scalability, maintainability, and testability while providing a rich user experience for game server administration.