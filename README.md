# 🚀 ASP.NET Core API Base Project

A clean and scalable base project for building APIs with .NET 8.

> **Note:** This project is currently in the early stages of development. Contributions and feedback are highly appreciated to help improve its stability and features.

---

## 🏗️ Technologies & Architectures

### Architecture

- **Clean Architecture**

### Framework

- **.NET 8.0**

### Database

- **PostgreSQL**

### Libraries & Patterns

- **MediatR:** In-process messaging for CQRS
- **FluentValidation:** Strong-typed validation rules

### Components

- **Custom Middleware** for Global Exception Handling

---

## 🛠️ Prerequisites

| Component       | Requirement                        |
| --------------- | ---------------------------------- |
| 💻 **IDE**      | Visual Studio 2022 (version 17.8+) |
| 📦 **SDK**      | .NET 8 SDK                         |
| 🗄️ **Database** | PostgreSQL instance                |

---

## 📋 Version History

### V1: 31/12/2025 - Database Connection

#### 🔧 Setup Instructions

1. **Create Configuration File**

   - 📄 Create file: `appsettings.Development.json` in `Net_8.0_Projects` directory
   - ⚙️ The project setting for PostgreSQL

2. **Add Connection String**

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=YourDBSchemaName;Username=postgres;Password=YourPassword"
     }
   }
   ```

3. **Run Database Migration**
   - 🎯 Open **Package Manager Console**: `Tools` → `NuGet Package Manager` → `Package Manager Console`
   - 📂 Choose **Default project**: `Infrastructure`
   - ⌨️ Type: `Update-Database` and press Enter
