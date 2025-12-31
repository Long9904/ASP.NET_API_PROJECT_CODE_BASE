ASP.NET Core API Base Project

A clean and scalable base project for building APIs with .NET 8.

Note: This project is currently in the early stages of development. Contributions and feedback are highly appreciated to help improve its stability and features.

---

Technologies & Architectures
Architecture: Clean Architecture

Framework: .NET 8.0

Database: PostgreSQL

Libraries & Patterns: - MediatR: In-process messaging for CQRS.

FluentValidation: Strong-typed validation rules.

Components: Custom Middleware for Global Exception Handling.

---

🛠 Prerequisites
IDE: Visual Studio 2022 (version 17.8+)

SDK: .NET 8 SDK

Database: PostgreSQL instance

---

V1: 31/12/2025: DB Connection

- To use database, create file: appsettings.Development.json in Net_8.0_Projects
- The project setting for postgresql

- Add connection string:
  "ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=YourDBSchemaName;Username=postgres;Password=YourPassword"
  }

- Open Pakage Manager Console: Tools - Nuget Pakage Manager
- Choose Default project: Infrastructure
- Type: Update-Database and Enter
