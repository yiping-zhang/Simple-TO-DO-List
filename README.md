# Simple TO DO List - Johns Lyng Group

A simple TO DO list application that allows users to see their TODO list, add items to it, and delete items from it.

As of Nov 2024, to-do list is persisted in memory. No database is used. 

## Backend

| Framework    | Language |
| -------- 	   | -------  |
| .NET 8       | C#       |

### How to run
1. Before running the application, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet)
- [Visual Studio](https://visualstudio.microsoft.com/) (or another preferred C# IDE)

2. Navigate to `ToDoList.BackEnd` directory and restore required NuGet packages by running `dotnet restore`

3. To run the application locally, navigate to `ToDoList.Endpoint` directory and run `dotnet run`. The application will start running on `https://localhost:5001`. Swagger UI can be accessed in your browser at `https://localhost:5001/swagger/index.html`

## Frontend

| Framework    | Language   |
| -------- 	   | -------    |
| Angular 18   | TypeScript |

### How to run
1. Before running the application, ensure you have the following installed on your machine:

- [Node.js](https://nodejs.org/) (v12.x or higher)
- [npm](https://www.npmjs.com/) (comes with Node.js)
- [Angular CLI](https://angular.io/cli) (optional, can be installed globally)

2. Navigate to `ToDoList.UI` directory and install project dependencies by running `npm install`

3. To start the development server and serve the Angular application, run `ng serve`. 
By default, the application will run at `http://localhost:4200/`