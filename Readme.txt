**Author:** Karthik B (Senior .NET Backend Engineer)  
**Email:** bgurukarthik@gmail.com  
**Portfolio:** https://github.com/KarthikB24

# London Stock Exchange – Trading API (MVP)

A minimal trading backend that exposes APIs to authenticate brokers, retrieve stock master data, submit trades, and query the latest stock prices.

The solution follows **Clean Architecture**, **CQRS**, **MediatR**, **Fluent Validation**, **Serilog logging**, **JWT authentication**, and **SQL Server persistence**.

## Features
- Broker authentication using JWT
- Retrieve latest price for a ticker
- Retrieve multiple prices in batch
- Retrieve all registered stocks
- Submit trades & maintain price summary
- In-memory or persistent storage supported
- Input validation via FluentValidation
- Global exception middleware for consistent responses

## Tech Stack

| **Layer** | **Technologies** |
|-----------|------------------|
| **API** | ASP.NET Core 10.0, JWT, Swagger, Versioning |
| **Application** | MediatR, CQRS, FluentValidation, AutoMapper |
| **Infrastructure** | EF Core, Dapper, SQL Server |
| **Testing** | **xUnit, Moq, FluentAssertions** |
| **Cross-Cutting** | Serilog Logging, Rate Limiting, Global Error Handling |


## Running the Solution
1. Update connection string in `appsettings.json`
2. Run DB migrations or execute schema scripts
3. Start API project
4. Navigate to Swagger UI

## API Documentation

### Authentication
| **Method** | **Endpoint** | **Description** |
|------------|--------------|-----------------|
| POST | `/api/v1/auth/BrokerLogin` | Authenticate broker, return JWT |

### Stocks
| **Method** | **Endpoint** | **Description** | **Auth** |
|------------|--------------|-----------------|----------|
| GET | `/api/v1/stocks/{ticker}` | Latest price of a ticker | No |
| POST | `/api/v1/stocks/prices` | Batch price lookup | No |
| GET | `/api/v1/stocks` | List all active stocks | Yes |

### Trades
| **Method** | **Endpoint** | **Description** | **Auth** |
|------------|--------------|-----------------|----------|
| POST | `/api/v1/trades/buy` | Submit BUY trade | Yes |
| POST | `/api/v1/trades/sell` | Submit SELL trade | Yes |
| GET | `/api/v1/trades` | Paginated trade list | Yes |


## 🧪 Testing Strategy

| **Type** | **Coverage** | **Tools** |
|----------|--------------|-----------|
| **Unit Tests** | Handlers, Validators, Services | xUnit + Moq |

