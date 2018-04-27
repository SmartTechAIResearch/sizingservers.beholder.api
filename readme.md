# sizingservers.beholder.api
    2017 Sizing Servers Lab  
    University College of West-Flanders, Department GKG


![flow](readme_img/flow.png)

This project is part of a computer hardware inventorization solution, together with sizingservers.beholder.agent and sizingservers.beholder.frontend.

Agents are installed on the computers you want to inventorize. These communicate with the REST API which stores hardware info. The front-end app visualizes that info.

## Languages, libraries, tools and technologies used
The API is a **C# ASP.NET Core Web Application** (**dotnet core 2.0**: target framework netcoreapp2.0) contained within a **Visual Studio 2017**
solution.  
Furthermore, the **Entity framework** is used to store system information agents send in a **SQLite3** database: beholder.db.

## Overview of the API
Communication happens over HTTP, using following structured url:

    http://< insert ip / fqdn here >:< port: default 5000 >/api/< call >?< params >

The ApiController class handles all communication and following calls are available:

* GET Ping  
  Returns "pong" if the api is reachable.
  
  Example: *GET http://localhost:5000/api/ping*
  
* GET List, params apiKey  
  Returns all stored system informations.
  
  More on the API key later.
  
  Example: *GET http://localhost:5000/api/list?apiKey=...*
  
* POST Report, params apiKey, body systemInformation  
  To store a new system information in the database or replace an existing one using the hostname.
  
  System information has a model you can find in the solution. It should be serialized as JSON in the body of the call.
  
  Example: *POST http://localhost:5000/api/report?apiKey=...*
  
* DELETE Remove, params apiKey hostname  
  Removes the system information having the given hostname, if any.
  
* PUT CleanOlderThan, params days apiKey  
  Cleans up old system informations so the database represents reality.

  Example: *PUT http://localhost:5000/api/cleanolderthan?days=1&apiKey=...*
   
* PUT Clear, params apiKey  
  Clear all system informations in the database.

  Example: *PUT http://localhost:5000/api/clear?apiKey=..*.
  
  
Furthermore, check the XML documentation in the Build / comments in the code.

## Build
You need the SDK (<https://www.microsoft.com/net/download/core#/sdk>) to build the source and the .Net framework SDK (<https://www.microsoft.com/en-us/download/details.aspx?id=55168>) if building on Windows.

You need to be connected to the Internet for restoring NuGet packages.

Execute *build DEBUG.cmd*, *build RELEASE.cmd* or the *sh*'s for when building on Linux:

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
    
If you do a Release build you can specify if an API key is required for communication from agents to the API. For a Debug build, communication always works without a key.

## Configure

### appsettings.json
    {
      "Logging": {
        "IncludeScopes": false,
        "LogLevel": {
          "Default": "Warning"
        }
      },
      "Authorization" :  false
    }
    
Set Authorization to enable or disable the requirement of an API key for communication from agents to the API.

Only for a Release build, this setting is used. For a Debug build, communication always works without a key.

P.S.: This security measure is a bit silly and should be replaced by something better if you need it. If not, leave it as is.

### beholder.db
beholder.db contains two usable tables:

![db](readme_img/db.png)

In APIKeys you can add keys manually, using SQLiteStudio. A key can be anything, for instance a SHA-512.  
SystemInformations is populated automatically by the use of the API. The timestamp is set when SystemInformation is added to / updated in the database.


### hosting.json
    {
      "server.urls": "http://0.0.0.0:5000;http://::1:5000"
    }

Here you can define the used endpoints when running standalone: a ASP.Net Core Web App is just a console application containing a http server (Kestrel).

If you want HTTPS, get a certificate from e.g. [letsencrypt.org](letsencrypt.org) and configure a real web server like IIS, Nginx, Apache as a reverse proxy.

(SSL certificates can only be hard-coded or loaded from an external file via a work-around for Kestrel (build-in http server in an ASP.Net core app) at the moment.)

### HTTPS reverse proxy Nginx (Ubuntu 16.04)

Use following commands.

    sudo apt-get install nginx
    sudo nano /etc/nginx/sites-available/aspnetcore
    
Add following content to the newly created config

    server {
        listen 443 ssl;    
        ssl_certificate PATH_TO_CERTIFICATE/CERTIFICATE_NAME.pem;
        ssl_certificate_key PATH_TO_PRIVATE_KEY/PRIVATE_KEY.pem;

        location / {
            proxy_pass http://localhost:5000;
        }
    }

Execute following commands.

    cd /etc/nginx/sites-enabled
    sudo ln -s /etc/nginx/sites-available/aspnetcore
    sudo nginx -s reload

### HTTPS reverse proxy Apache (Ubuntu 16.04)

Use following commands.

    sudo apt-get install apache2
    sudo a2enmod proxy_http
    sudo a2enmod proxy_connect
    sudo nano /etc/apache2/sites-available/aspnetcore
    
Add following content to the newly created config
    
    <VirtualHost *:443>
        ServerAdmin info@sizingservers
        ErrorLog ${APACHE_LOG_DIR}/error.log
        CustomLog ${APACHE_LOG_DIR}/access.log combined
        SSlEngine On
        SSLCertificateFile    PATH_TO_CERTIFICATE/CERTIFICATE_NAME.pem
        SSLCertificateKeyFile PATH_TO_PRIVATE_KEY/PRIVATE_KEY.pem
        ProxyPreserveHost On
        ProxyPass / http://localhost:5000/
        ProxyPassReverse / http://localhost:5000/
        ServerName localhost
    </VirtualHost>

Execute following commands.

    cd /etc/apache2/sites-enabled
    sudo ln -s /etc/apache2/sites-available/aspnetcore
    sudo apache2 reload

## Run
You need the .NET core runtime (<https://www.microsoft.com/net/download/core#/runtime>) to run the build: 2.0 at the time of writing.

You need the .NET framework on Windows, but you have that by default.

Execute run.cmd or run.sh or host it on a web server: <https://docs.microsoft.com/en-us/aspnet/core/publishing/>

Alternatively you can run it as a service / daemon. Use NSSM for Windows or the start-as-daemon script / screen for Linux.

To check if it works you can use Postman for instance.