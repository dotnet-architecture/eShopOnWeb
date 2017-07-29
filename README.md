# eShopOnWeb

Sample ASP.NET Core reference application, powered by Microsoft, demonstrating a monolithic application architecture and deployment model. This reference application is meant to support the [Architecting and Developing Modern Web Applications with ASP.NET Core and Azure eBook](https://aka.ms/webappebook)

The **eShopOnWeb** is related to the [eShopOnContainers](https://github.com/dotnet/eShopOnContainers) sample application which, in that case, focuses on a microservices/containers based application architecture. However, **eShopOnWeb** is much simpler in regards its current functionality and focuses on traditional Web Application Development with a single monolithic deployment, no microservices/containers related.

> ### DISCLAIMER
> **IMPORTANT:** The current state of this sample application is **ALPHA**, consider it a 0.1 foundational version. Therefore areas will change significantly while refactoring current code and implementing new features. **Feedback with improvements and pull requests from the community are highly appreciated and accepted.** Planned features for this sample include user registration, login, shopping cart management, and checkout.

## Topics (eBook TOC)

- Introduction
- Characteristics of Modern Web Applications
- Choosing Between Traditional Web Apps and SPAs
- Architectural Principles
- Common Web Application Architectures
- Common Client Side Technologies
- Developing ASP.NET Core MVC Apps
- Working with Data in ASP.NET Core Apps
- Testing ASP.NET Core MVC Apps
- Development Process for Azure-Hosted ASP.NET Core Apps
- Azure Hosting Recommendations for ASP.NET Core Web Apps

## Running the sample

After cloning or downloading the sample you will need to run its Entity Framework Core migrations before you will be able to run the app. To do so, open a command prompt in the Web folder and execute the following commands:

```
dotnet restore
dotnet ef database update --context Microsoft.eShopWeb.Infrastructure.CatalogContext
```

**NOTE** The application uses a separate DbContext for its authentication system, which is not yet complete.