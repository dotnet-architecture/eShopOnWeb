# Project Structure

```
├───Assets
├───Build
├───Documentation
├───Samples
├───Source
├───Tests
└───Tools

```
## Directories
* Assets - Images, Logos etc.
* Build - Build files yaml, cake, scripts, etc.
* Documentation - Documentation for your solution
* Samples (optional) - Sample projects
* Source - Main projects (the product code)
* Tests - Test projects
* Tools - Other applications scripts or tools used

## Files at root of the solution
* `<YourProject>`.sln - Your projects solution file
* .editorconfig - [coding conventions](https://docs.microsoft.com/en-us/visualstudio/ide/editorconfig-code-style-settings-reference?view=vs-2019)
* [.gitignore](https://git-scm.com/docs/gitignore) - started with the VisualStudio one from GitHub. Update as needed.
* [CodeMaid.config](http://www.codemaid.net/documentation/)
* [Directory.Build.props](https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2019#directorybuildprops-and-directorybuildtargets) Set the versions of the packages used across the solution here.
* [global.json](https://docs.microsoft.com/en-us/dotnet/core/tools/global-json?tabs=netcore3x)- (optional) specify required SDK
* [Nuget.config](https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file) - specify NuGet sources
* README.md
* Run.ps1 - (optional) PowerShell script to Run the application
* TestCafe.ps1 - Script to execute the TestCafe tests
* UNLICENSE.md


# Source directory

The client and server applications have parallel structures. 
Both utilize the MediatR pipeline and the command pattern of "Object in Object out."

## Folder Structure
```
├───Api
│   └───Features
│       ├───Base
│       └───WeatherForecast
│           └───GetList
├───Client
│   ├───Components
│   ├───Features
│   │   ├───Application
│   │   │   ├───Actions
│   │   │   │   ├───ResetStore
│   │   │   │   └───ToggleMenu
│   │   │   └───Components
│   │   ├───Base
│   │   │   └───Components
│   │   ├───ClientLoader
│   │   ├───Counter
│   │   │   ├───Actions
│   │   │   │   ├───IncrementCount
│   │   │   │   └───ThrowException
│   │   │   ├───Components
│   │   │   └───Notification
│   │   ├───EventStream
│   │   │   ├───Actions
│   │   │   │   └───AddEvent
│   │   │   ├───Components
│   │   │   └───Pipeline
│   │   └───WeatherForecast
│   │       └───Actions
│   │           └───Fetch
│   ├───Pages
│   │   └───Authentication
│   ├───Pipeline
│   │   ├───NotificationPostProcessor
│   │   └───NotificationPreProcessor
│   └───wwwroot
│       ├───css
│       └───images
└───Server
    ├───Data
    ├───Entities
    │   └───X_Aggregate
    ├───Features
    │   ├───Base
    │   └───WeatherForecast
    │       └───Get
    ├───Infrastructure
    ├───Pages
    └───Services
```

# Projects

Although we have three projects because of how the resulting dlls will be deployed, it is best to think of this as one solution.  We use namespaces that cross all projects.

* Api - The set of classes that  are shared between the Server and the Client projects that define the interface (Request, Response, DTOs, Common Validation).
* Client - The user presentation layer.
* Server - Implements the Endpoints of API

# Directories

There are three projects Client, Server, and Api some of the folders can be used in any of the three.  In the description we will indicate which project uses these folders (C, S, A)

```
├─Components
├─Configuration
├─Data
├─Entities
├─Features
├─Infrastructure
├─Pages
├─Pipeline
├─Services
└─wwwroot
```
All folders are optional if nothing will be in them feel free to delete.

* **Components** (C): For shared components used in more than one Feature or Page.
* **Configuration** (C|S|A): Contains Strongly typed classes representing configuration.
* **Data** (S): Data access layer (DbContext Migrations, Repository etc...)
* **Entities** (S): Core Business Logic
* **Features** (C|S|A): Organized by State/Aggregate they act upon. See below for the structure of a `Feature`.
* **Infrastructure** (C|S|A):
* **Pages** (C|S): Pages are normal components that specify a Route.
* **Pipeline** (C|S): Custom middle-ware (Behaviors) added to the mediator pipeline.
* **wwwroot** (C): static items used by the client. css, js, images etc.

## Features
Each Folder is named after the State/Aggregate to which it relates. Example *Features/Counter*

In this Folder you can have 
 * **Actions** (C): Contains Action and the ActionHandler Grouped in Folder by Action. 
   For Example:
      ```
      ├───Actions
      │   └───IncrementCount
      │           IncrementCounterAction.cs
      │           IncrementCounterHandler.cs
      ```
 * **Components**: 
   Components that only depend on this State.
   If other states are required then the component should be moved up the directory to a 
   `Components` folder that is a parent of all the dependent states.
 * **Notification**: Notification Handlers ( On the server they may be called Domain Events) 
 * **Pipeline** (C|S):  Custom middle-ware (Behaviors) added to the mediator pipeline exclusively for this feature
 * **State** (C): The definition of the State object and any of its required classes.
 * **Features** (C|S): Contained child Features. 