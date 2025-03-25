# Farm2Fork - ASP.NET Core Web API

Farm2Fork is an ASP.NET Core Web API project that provides endpoints for managing products from farms, utilizing PostgreSQL for data storage and Swagger for API documentation.

## Features

- **Product Management**: Add and retrieve product details.
- **Swagger Integration**: API documentation for easy interaction with the API.
- **PostgreSQL**: Database integration for persistent storage.

## Prerequisites

Before you begin, ensure you have the following installed:

- [Visual Studio Code](https://code.visualstudio.com/)
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet)
- [PostgreSQL](https://www.postgresql.org/download/)
- [PostgreSQL extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ckolkman.vscode-postgres) (Optional, for direct database access)

## Setup Instructions

### 1. Clone the repository

Clone this repository to your local machine:

```bash
git clone https://github.com/mdsaniulbasirsaz/Farm2Fork.git
cd Farm2Fork
```
### 2. Install dependencies
Run the following command to restore the project dependencies:
```bash
dotnet restore
```

### 3. Set up PostgreSQL
Ensure that you have PostgreSQL installed and running.
Create a new database in PostgreSQL (e.g., Farm2Fork).
Update the connection string in appsettings.json:
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=Farm2Fork;Username=your-username;Password=your-password"
  }
}
```
### 4. Apply Migrations
Run the following commands to create the database schema:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
### 5. Run the application
To run the application, execute the following command:
```bash
dotnet run
```
The API will be accessible at https://localhost:5001 or http://localhost:5000 depending on your settings.

### 6.Access Swagger
Once the application is running, you can access the Swagger UI by navigating to the following URL in your browser:
```bash
https://localhost:5001/swagger
```
