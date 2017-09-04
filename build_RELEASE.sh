# 2017 Sizing Servers Lab
# University College of West-Flanders, Department GKG
echo sizingservers.beholder.api build script
echo ----------
rm -rf Build/Release
cd sizingservers.beholder.api
dotnet restore
dotnet publish -c Release
# If you do not specify the framework following command will not work.
dotnet ef --framework netcoreapp2.0 --configuration Release database update
cd ..
mv -f Build/Release/netcoreapp2.0/publish/* Build/Release/netcoreapp2.0
rmdir Build/Release/netcoreapp2.0/publish
mv Build/Release/netcoreapp2.0/* Build/Release
rmdir Build/Release/netcoreapp2.0
