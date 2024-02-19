# Take Notes App

Introducing my note-taking app, seamlessly blending Angular 17 on the frontend with .NET 8 on the backend. Enjoy a modern interface and efficient note management, combining the power of the latest technologies for a seamless user experience.

You can see a production version of the app on:

[https://take-notes-app-4c867.web.app](https://take-notes-app-4c867.web.app/)

---

# Backend

Builded on .NET 8, has been deployed and hosted in Azure, can be consumed from:

[https://az-notes-api.azurewebsites.net](https://az-notes-api.azurewebsites.net/)

As mandatory, the API connects to a Sql Server Data Base that are also hosting in Azure and comunicate by using Entity Framework, the ORM per excellence of .NET.

The swagger UI was left enabled for the purpose of documenting and testing the backend from the web, you can see it from:

[https://az-notes-api.azurewebsites.net](https://az-notes-api.azurewebsites.net/)/swagger/index.html

As request, all the packages used are find on the backend\Notes.API\Notes.API.csproj file:

```powershell
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UserSecretsId>7abde481-291b-42f9-883b-295a5c8d892e</UserSecretsId>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Notes.Data\Notes.Data.csproj" />
    <ProjectReference Include="..\Notes.Services\Notes.Services.csproj" />
  </ItemGroup>

</Project>
```

If you prefer to run the application locally:

1. Go to the Notes.API directory:
    
    ```bash
    cd .\backend\Notes.API\
    ```
    
2. Build and run the API:
    
    ```bash
    dotnet restore
    dotnet run
    ```
    

In case you would like to consume the API from another source like postman, make sure to add to the headers the next line:

```json
{
	Ocp-Apim-Subscription-Key: '772a213e02b64195abfccdda18af9789'
};
```


---

# Frontend

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 17.1.0.

Made on Angular 17 which provides now an Server-Side Renderind (SSR) and of course a Single Page Web Application (SPA). 

As provided, the app can be tested via: [https://take-notes-app-4c867.web.app](https://take-notes-app-4c867.web.app/)

Hosted on Firebase.

All the packages and dependencies needed are found on the frontend\ng-notes-app\package.json file:

```json
{
  "name": "ng-notes-app",
  "version": "0.0.0",
  "scripts": {
    "ng": "ng",
    "start": "ng serve",
    "build": "ng build",
    "watch": "ng build --watch --configuration development",
    "test": "ng test",
    "serve:ssr:ng-notes-app": "node dist/ng-notes-app/server/server.mjs"
  },
  "private": true,
  "dependencies": {
    "@angular/animations": "^17.1.0",
    "@angular/common": "^17.1.0",
    "@angular/compiler": "^17.1.0",
    "@angular/core": "^17.1.0",
    "@angular/forms": "^17.1.0",
    "@angular/platform-browser": "^17.1.0",
    "@angular/platform-browser-dynamic": "^17.1.0",
    "@angular/platform-server": "^17.1.0",
    "@angular/router": "^17.1.0",
    "@angular/ssr": "^17.1.0",
    "bootstrap": "^5.3.2",
    "bootstrap-icons": "^1.11.3",
    "express": "^4.18.2",
    "rxjs": "~7.8.0",
    "tslib": "^2.3.0",
    "zone.js": "~0.14.3"
  },
  "devDependencies": {
    "@angular-devkit/build-angular": "^17.1.0",
    "@angular/cli": "^17.1.0",
    "@angular/compiler-cli": "^17.1.0",
    "@types/express": "^4.17.17",
    "@types/jasmine": "~5.1.0",
    "@types/node": "^18.18.0",
    "jasmine-core": "~5.1.0",
    "karma": "~6.4.0",
    "karma-chrome-launcher": "~3.2.0",
    "karma-coverage": "~2.2.0",
    "karma-jasmine": "~5.1.0",
    "karma-jasmine-html-reporter": "~2.1.0",
    "prettier": "^3.2.4",
    "typescript": "~5.3.2"
  }
}
```

To run the front locally (already connected to the hosted backend):

1. Navigate to the `frontend\\ng-notes-app\` directory where contains the `package.json` file.
2. Install all the dependencies, recomended using yarn run  `yarn` or if want to use npm run `npm install`.
3. Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.
4. Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

Or for Linux just run the bash `[run.sh](http://run.sh)` located on the root directory.

Enjoy!