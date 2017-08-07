# eShopOnWeb

Sample ASP.NET Core reference application, powered by Microsoft, demonstrating a single-process (monolithic) application architecture and deployment model. This reference application is meant to support the [Architecting Modern Web Applications with ASP.NET Core and Azure eBook](https://aka.ms/webappebook)

The **eShopOnWeb** sample is related to the [eShopOnContainers](https://github.com/dotnet/eShopOnContainers) sample application which, in that case, focuses on a microservices/containers-based application architecture. However, **eShopOnWeb** is much simpler in regards to its current functionality and focuses on traditional Web Application Development with a single deployment.

The goal for this sample is to demonstrate some of the principles and patterns described in the [eBook](https://aka.ms/webappebook). It is not meant to be an eCommerce reference application, and as such it does not implement many features that would be obvious and/or essential to a real eCommerce application.

> ### DISCLAIMER
> **IMPORTANT:** The current state of this sample application is **BETA**, consider it a 0.5 version, open to community feedback and contributions. Some areas will continue to evolve and change while refactoring continues. **Feedback with improvements and pull requests from the community are highly appreciated and accepted.**

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

After cloning or downloading the sample you should be able to run it using an In Memory database immediately.

If you wish to use the sample with a persistent database, you will need to run its Entity Framework Core migrations before you will be able to run the app. To do so, open a command prompt in the Web folder and execute the following commands:

```
dotnet restore
dotnet ef database update --context Microsoft.eShopWeb.Infrastructure.CatalogContext
```

**NOTE** The application uses a separate DbContext for its authentication system, which is not yet complete.