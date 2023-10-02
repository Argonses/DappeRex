# DappeRex

DappeRex is a CRUD web application made in ASP.NET Core API with Dapper. It allows users to create, read, update and delete records from an inventory database using a RESTful API.

## Features

- ASP.NET Core 7.0
- Dapper ORM
- SQL Server database
- Swagger UI

## Installation

To run this project, you need to have the following prerequisites:

- .NET 7.0 SDK
- SQL Server Express LocalDB
- Visual Studio 2019 or VS Code

Clone this repository and open the solution file `DappeRex.sln` in Visual Studio. Alternatively, you can use the `dotnet` CLI commands to build and run the project.

Before running the project, you need to create the database and tables using the scripts provided in the `Database` folder. You can use SQL Server Management Studio or any other tool to execute the scripts.

You also need to update the connection string in the `appsettings.json` file according to your database settings.

## Usage

You can use Swagger UI to test the API endpoints. The Swagger UI is available at `https://localhost:5001/swagger/index.html` when the project is running.

The API supports the following operations:

- GET `/api/Inventory` - Get all items
- GET `/api/Inventory/{id}` - Get an item by id
- POST `/api/Inventory` - Create a new item
- PUT `/api/Inventory/{id}` - Update an existing item
- DELETE `/api/Inventory/{id}` - Delete an item by id

The item model has the following properties:

- Id (int)
- Name (string)
- Description (string)
- Price (float)
- StockQuantity (int)
- Category (string)
- AddedDate (DateTime)

## License

This project is licensed under the MIT License.
