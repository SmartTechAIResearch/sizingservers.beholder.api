REM 2017 Sizing Servers Lab
REM University College of West-Flanders, Department GKG
echo sizingservers.beholder.api DEBUG build script
echo ----------
rmdir /S /Q Build\Debug
cd sizingservers.beholder.api
dotnet restore
dotnet publish -c Debug
REM If you do not specify the framework following command will not work.
dotnet ef --framework netcoreapp2.0 --configuration Debug database update
cd ..
copy /Y Build\Debug\netcoreapp2.0\publish\* Build\Debug\netcoreapp2.0
rmdir /S /Q Build\Debug\netcoreapp2.0\publish
copy /Y Build\Debug\netcoreapp2.0\* Build\Debug
rmdir /S /Q Build\Debug\netcoreapp2.0\
