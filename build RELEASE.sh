# 2017 Sizing Servers Lab
# University College of West-Flanders, Department GKG
echo sizingservers.beholder.api build script
echo ----------
rm -rf Build/Release
cd sizingservers.beholder.api
dotnet restore
dotnet publish -c Release
# If you do not specify the framework following command will not work.
dotnet ef --framework netcoreapp1.1 --configuration Release database update
cd ..
mv -f Build/Release/netcoreapp1.1/publish/* Build/Release/netcoreapp1.1
rmdir Build/Release/netcoreapp1.1/publish
mv Build/Release/netcoreapp1.1/* Build/Release
rmdir Build/Release/netcoreapp1.1
