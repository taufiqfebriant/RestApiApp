# Simple C# REST API

A simple REST API built with ASP.NET Core 8.

**Live Demo:** http://34.143.132.27:5000

Test the API:
```bash
curl http://34.143.132.27:5000/api/health
```

## Prerequisites

- .NET 8 SDK

## How to Run Locally

```bash
dotnet restore
dotnet run
```

The application will start at `http://localhost:5017`

## How to Open Swagger API Docs

Navigate to: `http://localhost:5017/swagger`

> Note: Swagger is only available in Development mode.

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

## Deployment Guide (Ubuntu / Linux)

This guide explains how to deploy the RestApiApp on an Ubuntu-based Linux server (e.g., Google Cloud VM).

---

### Prerequisites

- Ubuntu 22.04 / 24.04 LTS
- .NET 8 SDK installed
- Git installed

Install dependencies:

```bash
sudo apt update
sudo apt install -y git dotnet-sdk-8.0
```

---

### Clone Repository

```bash
git clone https://github.com/taufiqfebriant/RestApiApp.git
cd RestApiApp
```

---

### Publish Application

Create deployment directory:

```bash
sudo mkdir -p /opt/restapi
sudo chown -R $USER:$USER /opt/restapi
```

Publish the app:

```bash
dotnet publish RestApiApp.csproj -c Release -o /opt/restapi
```

---

### Configure systemd Service

Create a new service file:

```bash
sudo nano /etc/systemd/system/restapi.service
```

Paste the following:

```ini
[Unit]
Description=REST API App
After=network.target

[Service]
WorkingDirectory=/opt/restapi
ExecStart=/usr/bin/dotnet /opt/restapi/RestApiApp.dll
Restart=always
RestartSec=10
SyslogIdentifier=restapi
User=ubuntu
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:5000

[Install]
WantedBy=multi-user.target
```

Note: Replace `User=ubuntu` with your actual username if different.

---

### Start Service

```bash
sudo systemctl daemon-reload
sudo systemctl enable restapi
sudo systemctl start restapi
```

---

### Check Service Status

```bash
sudo systemctl status restapi
```

Expected result:

```
Active: active (running)
```

---

### View Logs

```bash
journalctl -u restapi -f
```

---

### Access Application

The application is deployed at: **http://34.143.132.27:5000**

Local access:

```
http://localhost:5000/api/health
```

External access:

```
http://<YOUR_EXTERNAL_IP>:5000/api/health
```

---

### Open Firewall (Optional)

If using Ubuntu firewall:

```bash
sudo ufw allow 5000
sudo ufw reload
```

If using Google Cloud:

- Go to VPC Network → Firewall
- Allow TCP:5000

---

### Test API

```bash
curl http://34.143.132.27:5000/api/health
```

---

### Summary

- Published using .NET 8
- Deployed to /opt/restapi
- Managed via systemd
- Accessible via port 5000
