# Shop

**Shop** free and open-source eCommerce solution project which written 
with clean architecture and best practices

## Technologies
* ASP.NET Core 6
* Entity Framework Core 5
* FluentValidation
* XUnit, FluentAssertions, Moq & Respawn
* AutoMapper [ Soon ]
* Docker [ Soon ]

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 6.x ](https://dotnet.microsoft.com/download/dotnet-core)
* EF Core 5.x 

### Installing
Follow these steps to get your development environment set up:
1. Clone the repository
2. At the root directory, restore required packages by running:
```csharp
dotnet restore
```
3. Next, build the solution by running:
```csharp
dotnet build
```
4. Next, within the AspnetRun.Web directory, launch the back end by running:
```csharp
dotnet run
```
5. Launch http://localhost:5001/ in your browser to view the Web UI.



## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebUI

This layer is a single page application based on Angular 10 and ASP.NET Core 5. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.


## Resources
* [Clean Architecture-jasontaylor](https://github.com/jasontaylordev/CleanArchitecture)
* [Clean Architecture - Unclue bob ](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
* [nopCommerce](https://github.com/nopSolutions/nopCommerce)
