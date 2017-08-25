# eShopOnWeb

Sample ASP.NET Core reference application, powered by Microsoft, demonstrating a single-process (monolithic) application architecture and deployment model. This reference application is meant to support the [Architecting Modern Web Applications with ASP.NET Core and Azure eBook](https://aka.ms/webappebook)

The **eShopOnWeb** sample is related to the [eShopOnContainers](https://github.com/dotnet/eShopOnContainers) sample application which, in that case, focuses on a microservices/containers-based application architecture. However, **eShopOnWeb** is much simpler in regards to its current functionality and focuses on traditional Web Application Development with a single deployment.

The goal for this sample is to demonstrate some of the principles and patterns described in the [eBook](https://aka.ms/webappebook). It is not meant to be an eCommerce reference application, and as such it does not implement many features that would be obvious and/or essential to a real eCommerce application.

> ### DISCLAIMER
> **IMPORTANT:** The current state of this sample application is 1.0. It remains open to community feedback and contributions. Work on updating the sample to ASP.NET Core 2.0 and EF Core 2.0 is taking place in its own branch. **Feedback with improvements and pull requests from the community are highly appreciated will be accepted if possible.**

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

If you wish to use the sample with a persistent database, you will need to run its Entity Framework Core migrations before you will be able to run the app, and update `ConfigureServices` method in `Startup.cs`.

### Configuring the sample to use SQL Server

1. Update `Startup.cs`'s `ConfigureServices` method as follows:

```
public void ConfigureServices(IServiceCollection services)
{
    // Requires LocalDB which can be installed with SQL Server Express 2016
    // https://www.microsoft.com/en-us/download/details.aspx?id=54284
    services.AddDbContext<CatalogContext>(c =>
    {
        try
        {
            //c.UseInMemoryDatabase("Catalog");
            c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection"));
            c.ConfigureWarnings(wb =>
            {
                //By default, in this application, we don't want to have client evaluations
                wb.Log(RelationalEventId.QueryClientEvaluationWarning);
            });
        }
        catch (System.Exception ex )
        {
            var message = ex.Message;
        }                
    });

    // Add Identity DbContext
    services.AddDbContext<AppIdentityDbContext>(options =>
        //options.UseInMemoryDatabase("Identity"));
        options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));


// leave the rest of the method as-is
```
1. Ensure your connection strings in `appsettings.json` point to a local SQL Server instance.

2. Open a command prompt in the Web folder and execute the following commands:

```
dotnet restore
dotnet ef database update -c catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
dotnet ef database update -c appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
```

These commands will create two separate databases, one for the store's catalog data and shopping cart information, and one for the app's user credentials and identity data.

3. Run the application.
The first time you run the application, it will seed both databases with data such that you should see products in the store, and you should be able to log in using the demouser@microsoft.com account.

Note: If you need to create migrations, you can use these commands:
```
-- create migration (from Web folder CLI)
dotnet ef migrations add InitialModel --context catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations

dotnet ef migrations add InitialIdentityModel --context appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Identity/Migrations
```
