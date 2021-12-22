Rem Clean the solution
dotnet clean --configuration Release

Rem Build the solution
dotnet build --configuration Release

Rem Run Test for eBroker.Repository.Test
dotnet test eBroker.Repository.Test/eBroker.Repository.Test.csproj --configuration Release --no-build /p:CollectCoverage=true  /p:CoverletOutput=../codeCoverageReport/

Rem Run Test for eBroker.Service.Test
dotnet test eBroker.Service.Test/eBroker.Service.Test.csproj --configuration Release --no-build /p:CollectCoverage=true  /p:CoverletOutput=../codeCoverageReport/ /p:MergeWith="../codeCoverageReport/coverage.json"

Rem Run Test for eBroker.Test
dotnet test eBroker.Test/eBroker.Test.csproj --configuration Release --no-build /p:CollectCoverage=true  /p:CoverletOutput=../codeCoverageReport/ /p:MergeWith="../codeCoverageReport/coverage.json" /p:CoverletOutputFormat="cobertura"

Rem Generate Report
reportgenerator -reports:".\codeCoverageReport\coverage.cobertura.xml" -targetdir:".\codeCoverageReport\coverageReport" -reporttypes:Html

Rem Open Report
start ./codeCoverageReport/coverageReport/index.html