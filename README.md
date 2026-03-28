# Simple C# REST API

A simple REST API built with ASP.NET Core 8.

## Prerequisites

- .NET 8 SDK
- Linux (WSL Ubuntu for Windows, native Linux/OSX for Mac)
- Terminal

## How to Run Locally

```bash
dotnet restore
dotnet run
```

The application will start at `http://localhost:5017`

## How to Open Swagger API Docs

Navigate to: `http://localhost:5017/swagger`

> Note: Swagger is only available in Development mode.

## How to Deploy on Linux

Build and run as a self-contained application:

```bash
# Build self-contained for Linux x64
dotnet publish -c Release -r linux-x64 --self-contained

# Run the application
./bin/Release/net8.0/linux-x64/RestApiApp
```

To bind to a different port (e.g., port 80):

```bash
ASPNETCORE_URLS=http://0.0.0.0:80 ./bin/Release/net8.0/linux-x64/RestApiApp
```

To run in background:

```bash
nohup ./bin/Release/net8.0/linux-x64/RestApiApp &
```

## API Endpoints

- `GET /api/health` - Health check
- `GET /api/users` - Users endpoints

For all endpoints, use Swagger UI at `/swagger`.

## Project Structure

```
Controllers/   - API endpoints
Services/      - Business logic
Middleware/    - Exception handling
```


