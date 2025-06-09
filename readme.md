# StockNotifier

A real-time stock alert notification system built with ASP.NET Core (.NET 9), SignalR, and Hangfire.

## Features

- Real-time stock alerts using SignalR
- Scheduled background jobs with Hangfire
- Caching for performance optimization
- Modular architecture with repository and service layers
- .NET 9 and C# 13 support

## Projects

- **StockNotifier.Infrastructure**: Infrastructure services, SignalR Hubs, background jobs, and data access.
- **StockNotifier.Domain**: Core domain entities and enums.
- **StockNotifier.Application**: Application logic, repositories, and cache interfaces.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server or another supported database (if using persistent storage)
- Redis (optional, for distributed caching)
- Node.js (optional, for front-end SignalR clients)

### Setup

1. **Clone the repository:**

2. **Configure appsettings:**
	- Update `appsettings.json` with your database, cache, and SignalR settings.
	- Create database "stocknotifier" in your SQL Server instance.

3. **Restore and build:**

4. **Run database migrations (if applicable):**

5. **Run the application:**

6. **Access Hangfire Dashboard:**
   - Navigate to `/hangfire` in your browser (if enabled).

## Usage

- The system will periodically check stock values and send real-time alerts to connected clients when thresholds are crossed.
- Clients can subscribe to SignalR events to receive notifications.

## Technologies Used

- ASP.NET Core (.NET 9)
- SignalR
- Hangfire
- Dapper
- MemoryCache
- C# 13