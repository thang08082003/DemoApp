# DemoApp

## Overview
DemoApp is a comprehensive employee management system built with .NET Core. This solution consists of three main projects that follow a modular architecture to ensure scalability and maintainability.

## Features
- Employee management (Create, Read, Update, Delete)
- Employee profile picture management
- User authentication and authorization
- Role-based access control
- Company management
- CSV file import/export

## Project Structure
The solution contains three projects:

### 1. DemoShared
A class library containing shared models, constants, and utilities used across the application.

**Key components:**
- **Constants**: Application settings, navigation paths, and common messages
- **Models**: 
  - Data Transfer Objects (DTOs)
  - Database entities
  - Request/response models
  - Parameter models for API calls

### 2. DemoWeb
A Blazor WebAssembly front-end application that provides the user interface.

**Key components:**
- **Layout**: Application layouts including main and login layouts
- **Pages**: 
  - Account management (Login)
  - Employee information and management
  - Home page
- **Services**: 
  - API repositories for back-end communication
  - Redirection services
  - Configuration services
- **Components**:
  - Modal dialogs for employee operations
  - Confirmation dialogs

### 3. DemoWebApi
A RESTful API that handles business logic and data access.

**Key components:**
- **Controllers**: API endpoints for various entities (Account, Employee, Company, etc.)
- **Services**: Business logic implementation
- **Data**: Entity Framework Core context and migrations
- **Repository**: Data access implementations

## Technologies
- **.NET Core** - Cross-platform framework
- **Blazor WebAssembly** - Client-side web framework
- **Entity Framework Core** - ORM for database operations
- **RESTful API** - Backend service architecture
- **C#** - Primary programming language

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (or compatible database)
- Visual Studio 2022 or VS Code

### Installation
1. Clone the repository
```
git clone https://github.com/thang08082003/DemoApp.git
```

2. Navigate to the solution directory
```
cd DemoApp
```

3. Restore NuGet packages
```
dotnet restore
```

4. Update the database connection string in `DemoWebApi/appsettings.json`

5. Apply database migrations
```
cd DemoWebApi
dotnet ef database update
```

### Running the Application
1. Start the API project
```
cd DemoWebApi
dotnet run
```

2. In a separate terminal, start the Web project
```
cd DemoWeb
dotnet run
```

3. Open your browser and navigate to the URL shown in the terminal (typically https://localhost:5001)



## Contributors
- [thang08082003](https://github.com/thang08082003)
