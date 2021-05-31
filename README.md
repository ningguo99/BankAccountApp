# Bank Account API's

## Set up
Install Git and dotnet CLI if you haven't. Clone the repository and start the application by running the following commands:
```
git clone https://github.com/ningguo99/BankAccountApp.git
```
```
cd BankAccountApp/BankAccountApi
```
```
dotnet dev-certs https --trust
```
```
dotnet run
```
Navigate to [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html) to check all the APIs.

## Overview
This project creates the following API:
| API                                   | Description                | Request body      | Response body          | HTTP Header           | Query Params |
| ------------------------------------- |:--------------------------:| :----------------:|:----------------------:|:---------------------:|:------------:|
| `GET /api/Users`                      | Get all users              | None              | Array of AppUser       | Username              | None         |
| `POST /api/Users`                     | Add a new user             | AppUser           | ReturnedUserDto        | Username              | None         |
| `GET /api/Users/{id}/total-balance`   | Get a user's total balance | None              | total balance (double) | UserId                | None         |
| `PUT /api/Users/{id}/update-address`  | Update a user's address    | UpdateAddressDto  | ReturnedUserDto        | UserId                | None         |
| `POST /api/BankAccounts`              | Create a bank account      | AddBankAccountDto | ReturnedBankAccountDto | UserId                | None         |
| `PUT /api/BankAccounts/{id}/deposit`  | Deposit to an account      | None              | ReturnedBankAccountDto | UserId                | amount       |
| `PUT /api/BankAccounts/{id}/withdraw` | Withdraw from an account   | None              | ReturnedBankAccountDto | UserId                | amount       |
| `GET /api/BankAccounts/{id}/balance`  | Get an account's balance   | None              | balance (double)       | UserId                | None         |

## Assumptions & Explanations
* Since user authentication is out of scope, an HTTP Header "UserId" will be included in the relevant APIs e.g. in `/api/BankAccounts/{id}/deposit` to check if the user has the permission to deposit to a specific bank account.
* Any unhandled exceptions are catched in [`BankAccountApi/Middleware/ExceptionMiddleware.cs`](BankAccountApi/Middleware/ExceptionMiddleware.cs). All error logs can be found under `BankAccountApi/logs`.
* Address validation is performed in [`BankAccountApi/Helpers/AuStateAttribute.cs`](BankAccountApi/Helpers/AuStateAttribute.cs) and [`BankAccountApi/Helpers/AddressHelper.cs`](BankAccountApi/Helpers/AddressHelper.cs).