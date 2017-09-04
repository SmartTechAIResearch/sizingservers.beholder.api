# 2017 Sizing Servers Lab
# University College of West-Flanders, Department GKG
echo sizingservers.beholder.api DEBUG build script
echo ----------
rm -rf Build/Debug
cd sizingservers.beholder.api
dotnet restore
dotnet publish -c Debug
# If you do not specify the framework following command will not work.
dotnet ef --framework netcoreapp2.0 --configuration Debug database update
cd ..
mv -f Build/Debug/netcoreapp2.0/publish/* Build/Debug/netcoreapp2.0
rmdir Build/Debug/netcoreapp2.0/publish
mv Build/Debug/netcoreapp2.0/* Build/Debug
rmdir Build/Debug/netcoreapp2.0
