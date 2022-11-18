# Default to current directory
$FOLDER_PATH = [string](Get-Location)

# Before setting up your database, make sure of two things:
# 1. Ensure your connection strings in appsettings.json point to a local SQL Server instance.
# 2. Ensure the tool EF was already installed.
dotnet tool update --global dotnet-ef
dotnet ef database update -c catalogcontext -p $FOLDER_PATH/src/Infrastructure/Infrastructure.csproj -s $FOLDER_PATH/src/Web/Web.csproj
dotnet ef database update -c appidentitydbcontext  -p $FOLDER_PATH/src/Infrastructure/Infrastructure.csproj -s $FOLDER_PATH/src/Web/Web.csproj