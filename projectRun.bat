Rem Clean the solution
dotnet clean --configuration Release

Rem Build the solution
dotnet build  --configuration Release

Rem Run Test Case of Solution
dotnet test --configuration Release

Rem Run the solution
dotnet run --project eBroker/eBroker.csproj  --configuration Release