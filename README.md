# 🚢 Tally Management System API

A production-grade port operations API built with **ASP.NET Core 10**, designed to digitize the manual tally process at shipping ports. Built by a working tallyman at the Port of Djen Djen, Algeria.

> **Live API:** https://tally-app-production.up.railway.app

---

## 📋 Overview

The Tally Management System replaces paper-based port tally workflows with a digital, real-time API. It tracks vessel arrivals, merchandise counting, truck sessions, pauses, client assignments, and operational observations — everything a port tallyman records during a shift.

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────┐
│                   API Layer                  │
│         Controllers + Middleware             │
├─────────────────────────────────────────────┤
│               Application Layer             │
│     CQRS + MediatR + FluentValidation       │
│      Pipeline Behaviors (Cache/Validate)    │
├─────────────────────────────────────────────┤
│                Domain Layer                  │
│        Entities + Interfaces + Enums        │
├─────────────────────────────────────────────┤
│             Infrastructure Layer            │
│   EF Core + Redis + Hangfire + Cloudinary   │
└─────────────────────────────────────────────┘
```

**Patterns used:**
- Clean Architecture
- CQRS + Mediator (MediatR)
- Repository Pattern
- Pipeline Behaviors (Validation + Cache Invalidation)
- Write-Behind Caching

---

## ⚙️ Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core 10 |
| ORM | Entity Framework Core + PostgreSQL |
| Caching | FusionCache + Redis |
| Auth | JWT Bearer + ASP.NET Identity |
| Background Jobs | Hangfire |
| Real-time | SignalR |
| File Upload | Cloudinary |
| Logging | Serilog |
| Validation | FluentValidation |
| Email | Resend |
| Container | Docker / Podman |
| Deployment | Railway |

---

## 🚀 Features

### Authentication & Users
- JWT authentication with refresh token rotation
- Email verification with OTP
- Password reset flow
- User profiles with avatar upload (Cloudinary)
- Role-based authorization (`Chef`, `Tallyman`)

### Port Operations
- **Ships** — vessel registry with IMO number
- **TallySheets** — shift-based tally sessions (morning/afternoon/night) per zone
- **TallySheet Trucks** — track truck sessions with start/end times
- **Pauses** — record truck or ship pauses with reasons and duration
- **Merchandise** — cargo types and units (bags, packages, pieces, tons)
- **TallySheet Merchandise** — real-time quantity counting with write-behind caching
- **Clients** — cargo owners with Bill of Lading
- **Cars** — vehicle tracking with VIN numbers
- **Observations** — operational notes tied to tally sheet, client, or truck

### Performance & Reliability
- FusionCache with tag-based invalidation across all read endpoints
- Write-behind caching for high-frequency quantity updates (Redis → DB via Hangfire every 2 minutes)
- `AsNoTracking` on all read queries
- Response compression (Brotli + Gzip)
- Rate limiting
- Health checks

---

## 📡 API Endpoints

### Auth
```
POST   /api/auth/register
POST   /api/auth/login
POST   /api/auth/logout
POST   /api/tokens/refresh
POST   /api/emails/verify
POST   /api/emails/forget-password
POST   /api/emails/reset-password
```

### Ships
```
GET    /api/ships?pageNumber=1&pageSize=10&name=&sortBy=name&isDescending=false
GET    /api/ships/{id}
POST   /api/ships
PUT    /api/ships/{id}
DELETE /api/ships/{id}
```

### TallySheets
```
GET    /api/tallysheets?shipName=&shift=morning&zone=zone_a&date=
GET    /api/tallysheets/{id}
POST   /api/tallysheets
PUT    /api/tallysheets/{id}
DELETE /api/tallysheets/{id}
```

### TallySheet Trucks
```
GET    /api/tallysheettrucks/{tallySheetId}
POST   /api/tallysheettrucks
PATCH  /api/tallysheettrucks/{id}/end
```

### Pauses
```
GET    /api/pauses/tallysheet/{tallySheetId}
GET    /api/pauses/truck/{tallySheetTruckId}
GET    /api/pauses/{id}
POST   /api/pauses
PATCH  /api/pauses/{id}/end
DELETE /api/pauses/{id}
```

### Merchandise
```
GET    /api/merchandises?pageNumber=1&pageSize=10&name=&type=
GET    /api/merchandises/{id}
POST   /api/merchandises
PUT    /api/merchandises/{id}
DELETE /api/merchandises/{id}
```

### TallySheet Merchandise
```
GET    /api/tallysheetmerchandise/{tallySheetId}
GET    /api/tallysheetmerchandise/{tallySheetId}/{merchandiseId}
POST   /api/tallysheetmerchandise
PATCH  /api/tallysheetmerchandise/{tallySheetId}/{merchandiseId}/quantity
DELETE /api/tallysheetmerchandise/{tallySheetId}/{merchandiseId}
```

### Clients
```
GET    /api/clients?pageNumber=1&pageSize=10&name=
GET    /api/clients/{id}
POST   /api/clients
PUT    /api/clients/{id}
DELETE /api/clients/{id}
```

### Trucks
```
GET    /api/trucks?pageNumber=1&pageSize=10
GET    /api/trucks/{id}
POST   /api/trucks
PUT    /api/trucks/{id}
DELETE /api/trucks/{id}
```

### Observations
```
GET    /api/observations/tallysheet/{tallySheetId}
GET    /api/observations/{id}
POST   /api/observations
PUT    /api/observations/{id}
DELETE /api/observations/{id}
```

### Uploads
```
POST   /api/uploads/profile-picture
DELETE /api/uploads/{id}
```

---

## 🐳 Running Locally

### Prerequisites
- Docker or Podman
- .NET 10 SDK

### 1. Clone the repository
```bash
git clone https://github.com/yourusername/tally-api.git
cd tally-api
```

### 2. Create `.env` file
```env
ConnectionStrings__Default=Host=db;Port=5432;Database=Tally;Username=postgres;Password=postgres
JWT__Issuer=http://localhost:5198
JWT__Audience=http://localhost:5198
JWT__Key=your-secret-key-at-least-256-bits-long
JWT__AccessTokenExpirationMinutes=15
JWT__RefreshTokenExpirationDays=7
Smtp__Host=mailhog
Smtp__Port=1025
Smtp__EnableSsl=false
Smtp__UserName=dev
Smtp__Password=dev
Smtp__From=noreply@tally.com
Smtp__FromName=Tally System
RedisConnectionString=redis:6379
CloudinarySettings__CloudName=your_cloud_name
CloudinarySettings__ApiKey=your_api_key
CloudinarySettings__ApiSecret=your_api_secret
ASPNETCORE_ENVIRONMENT=Development
```

### 3. Run with Docker Compose
```bash
docker compose up --build
```

### 4. Access the API
- **Scalar UI:** http://localhost:5198/docs
- **Hangfire Dashboard:** http://localhost:5198/hangfire
- **MailHog UI:** http://localhost:8025

---

## 🏛️ Project Structure

```
├── API/                    # Controllers, Middleware, Program.cs
├── Application/            # Commands, Queries, Handlers, DTOs, Validators
│   ├── Commands/
│   ├── Queries/
│   ├── Handlers/
│   ├── Dtos/
│   ├── Validators/
│   ├── Mappers/
│   └── Helpers/            # Pipeline Behaviors
├── Domain/                 # Entities, Interfaces, Enums
│   ├── Entities/
│   ├── Contracts/
│   ├── Enums/
│   └── Helpers/
└── Infrastructure/         # Repositories, DbContext, Jobs, Services
    ├── Data/
    ├── Repositories/
    ├── Jobs/
    └── Services/
```

---

## 🔒 Security

- JWT Bearer authentication on all endpoints
- Refresh token rotation — old tokens revoked on refresh
- Role-based authorization
- Rate limiting on all endpoints
- Input validation via FluentValidation pipeline
- Secrets managed via environment variables — never committed to source control

---

## 💡 Architecture Highlights

### MediatR Pipeline Behaviors

Every command passes through two pipeline behaviors automatically:

1. **ValidationBehaviour** — runs FluentValidation before the handler executes
2. **CacheInvalidationBehavior** — invalidates Redis cache after write operations

```
Request → ValidationBehaviour → CacheInvalidationBehavior → Handler → Response
```

### Write-Behind Caching for Quantity Updates

Merchandise quantities change frequently during tally sessions. To avoid hammering the database on every update:

1. `PATCH /quantity` writes to Redis instantly
2. Hangfire job runs every 2 minutes
3. Job reads all pending updates from Redis
4. Bulk updates PostgreSQL in one pass
5. Clears Redis queue

This pattern absorbs high-frequency writes without degrading database performance.

---

## 🌍 Deployment

Deployed on **Railway** with:
- PostgreSQL managed database
- Redis managed instance
- Docker container deployment from GitHub

---

## 👤 Author

**Walid Aguib** — Tallyman at Port of Djen Djen, Algeria. Self-taught .NET developer.

- Built this system to digitize the exact workflow I perform manually every shift
- Domain expertise + software skills = a rare combination in maritime tech

---

## 📄 License

MIT
