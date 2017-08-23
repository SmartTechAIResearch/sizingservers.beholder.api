REM 2017 Sizing Servers Lab
REM University College of West-Flanders, Department GKG
echo sizingservers.beholder.api DEBUG build script
echo ----------
rmdir /S /Q Build\Debug
cd sizingservers.beholder.api
dotnet restore
dotnet publish -c Debug
REM If you do not specify the framework following command will not work.
dotnet ef --framework netcoreapp1.1 --configuration Debug database update
cd ..
copy /Y Build\Debug\netcoreapp1.1\publish\* Build\Debug\netcoreapp1.1
rmdir /S /Q Build\Debug\netcoreapp1.1\publish
copy /Y Build\Debug\netcoreapp1.1\* Build\Debug
rmdir /S /Q Build\Debug\netcoreapp1.1\
