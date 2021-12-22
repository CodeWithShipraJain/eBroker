# eBroker
**Unit Test Assignment**

_The solution is developed on .netcore 3.1_
_Test cases were not developed for startup.cs and main.cs_

>**Inital set up to be done to generate the coverage report** 
- Run following file in the root directory: `projectSetup.bat`
- Open visual studio go to Extenions(Menu) --> Manage Extension; then search "Run Coverlet Report" and install it. (** You might need to close all visual studio instances to install it.)
- Once install; open visual studio go to Tools(Menu) --> Options and search for "Run Coverlet Report" and update "Integration Type" to "MSBuild"

**  _First of all, run all of you tests for the first time: this helps to initialize correctly all the references to the test projects._

> Run the code
- Run following file in the root directory: `projectRun.bat`
- Run following api: (Currently we have following equity id: 1,2,3,4,5,6,7)
  - To add fund value: POST https://localhost:44371/api/fund/add/{amount}
  - To buy equity:     POST https://localhost:44371/api/trader/equity/{equityId}/buy/{quantity}
  - To sell equity:    POST https://localhost:44371/api/trader/equity/{equityId}/sell/{quantity}


> Generate Report
- Run following file in the root directory: `projectReportGeneration.bat`

>To view code coverage report in pdf do following:
- Open codeCoverageReport/Summary - Coverage Report.pdf
