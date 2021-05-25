# albelli Assignment

## Architecture & Design

The project uses .NET Core and Sql Server. It consists of:
* UI layer `Albelli_Assignment`
* Business Logic layer `Albelli_Assignment.BusinessLogic`
* Data Access layer `Albelli_Assignment.DataAccess`.

Business Logic and Data Access are loosely coupled by using interfaces (`Albelli_Assignment.BusinessLogic.Interfaces` and `Albelli_Assignment.DataAccess.Interfaces`)
with dependencies injected in the UI layer.

Entities are in a separate library `Albelli_Assignment.Entities` so that they can be shared amoung several libraries, as well as Business logic models in `Albelli_Assignment.BusinessLogic.Models`
which contains input and output models for the Business Logic layer.

## Usage

Initially, the Data Access layer is short circuited, and the objects are saved in memory.
To use the database, change the connection string in `appsettings.json`, use `Update-Database` command on `Albelli_Assignment.DataAccess`,
then go to `Startup.cs` and switch the dependency from `DataAccessMocked` class to `DataAccess` class, then run.

## Bin Minimum Width

You will find a loosely coupled solution for the Bin Minimum Width problem, in the Business Logic layer.
The calculation is done on demand; it is not stored in the database, as this is better for data consistency.

## Unit Testing

You will find some test methods in the `Tests` folder for Business Logic layer as a unit. It will use the mocked version of the Data Access to isolate the Business Logic layer.
